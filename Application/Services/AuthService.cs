using Application.DTOs;
using Application.Interfaces;
using Application.Settings;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _jwtSettings;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<RefreshTokenDto> _refreshTokenValidator;

        public AuthService(
            IUnitOfWork unitOfWork,
            IJwtService jwtService,
            IOptions<JwtSettings> jwtSettings,
            IValidator<LoginDto> loginValidator,
            IValidator<RegisterDto> registerValidator,
            IValidator<RefreshTokenDto> refreshTokenValidator)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _jwtSettings = jwtSettings.Value;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
            _refreshTokenValidator = refreshTokenValidator;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var validationResult = await _loginValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
            if (user == null || !user.IsActive)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return null;
            }

            return await GenerateAuthResponseAsync(user);
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var validationResult = await _registerValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (await _unitOfWork.Users.IsEmailTakenAsync(dto.Email))
            {
                throw new InvalidOperationException("Email is already registered.");
            }

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return await GenerateAuthResponseAsync(user);
        }

        public async Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenDto dto)
        {
            var validationResult = await _refreshTokenValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var refreshToken = await _unitOfWork.RefreshToken.GetByTokenAsync(dto.RefreshToken);

            if (refreshToken == null || !refreshToken.IsActive)
            {
                return null;
            }

            var user = refreshToken.User;
            if (!user.IsActive)
            {
                return null;
            }

            // Revoke current refresh token (rotation)
            refreshToken.RevokedAt = DateTime.UtcNow;

            // Generate new tokens
            var newAccessToken = _jwtService.GenerateAccessToken(user);
            var newRefreshToken = await CreateRefreshTokenAsync(user.UserId);

            // Link old token to new one
            refreshToken.ReplacedByToken = newRefreshToken.Token;

            await _unitOfWork.SaveChangesAsync();

            return new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token,
                AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                RefreshTokenExpiresAt = newRefreshToken.ExpiresAt
            };
        }

        public async Task<bool> RevokeTokenAsync(RefreshTokenDto dto)
        {
            var refreshToken = await _unitOfWork.RefreshToken.GetByTokenAsync(dto.RefreshToken);

            if (refreshToken == null || !refreshToken.IsActive)
            {
                return false;
            }

            refreshToken.RevokedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RevokeAllUserTokensAsync(int userId)
        {
            await _unitOfWork.RefreshToken.RevokeAllUserTokensAsync(userId);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private async Task<AuthResponseDto> GenerateAuthResponseAsync(User user)
        {
            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = await CreateRefreshTokenAsync(user.UserId);

            await _unitOfWork.SaveChangesAsync();

            return new AuthResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                RefreshTokenExpiresAt = refreshToken.ExpiresAt
            };
        }

        private async Task<RefreshToken> CreateRefreshTokenAsync(int userId)
        {
            var refreshToken = new RefreshToken
            {
                Token = _jwtService.GenerateRefreshToken(),
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.RefreshToken.AddAsync(refreshToken);

            return refreshToken;
        }
    }
}