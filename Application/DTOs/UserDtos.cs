using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateUserDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Accept plaintext password and hash it server-side in the service
        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "User";

        public bool IsActive { get; set; } = true;
    }

    public class UpdateUserDto
    {
        public string? FullName { get; set; }
        [EmailAddress] public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public bool? IsActive { get; set; }
    }
}
