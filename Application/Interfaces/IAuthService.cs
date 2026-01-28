using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenDto dto);
        Task<bool> RevokeTokenAsync(RefreshTokenDto dto);
        Task<bool> RevokeAllUserTokensAsync(int userId);
    }
}