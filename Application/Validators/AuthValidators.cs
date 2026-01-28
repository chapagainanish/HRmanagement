using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }

    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        private static readonly string[] ValidRoles = ["Admin", "User", "Manager"];

        public RegisterDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(200).WithMessage("Full name cannot exceed 200 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.");

            RuleFor(x => x.Role)
                .Must(role => ValidRoles.Contains(role))
                .WithMessage($"Role must be one of: {string.Join(", ", ValidRoles)}.");
        }
    }

    public class RefreshTokenDtoValidator : AbstractValidator<RefreshTokenDto>
    {
        public RefreshTokenDtoValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required.");
        }
    }
}