using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var result = new List<UserDto>();
            foreach (var user in users)
            {
                result.Add(MapToDto(user));
            }
            return result;
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            return MapToDto(user);
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            return MapToDto(user);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = dto.Password,
                Role = dto.Role,
                IsActive = dto.IsActive
            };

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(user);
        }

            public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(dto.Email))
            {
                user.Email = dto.Email;
            }
            if (!string.IsNullOrEmpty(dto.FullName))
            {
                user.FullName = dto.FullName;
            }
            if (!string.IsNullOrEmpty(dto.Password))
            {
                user.PasswordHash = dto.Password;
            }
            if (!string.IsNullOrEmpty(dto.Role))
            {
                user.Role = dto.Role;
            }
            if (dto.IsActive.HasValue)
            {
                user.IsActive = dto.IsActive.Value;
            }

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            _userRepository.Remove(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _userRepository.IsEmailTakenAsync(email);
        }

        private UserDto MapToDto(User entity)
        {
            return new UserDto
            {
                UserId = entity.UserId,
                FullName = entity.FullName,
                Email = entity.Email,
                Role = entity.Role,
                IsActive = entity.IsActive
            };
        }
    }
}