using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        private static readonly string[] ValidRoles = ["Admin", "User", "Manager"];

        public CreateUserDtoValidator()
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
                .NotEmpty().WithMessage("Role is required.")
                .Must(role => ValidRoles.Contains(role))
                .WithMessage($"Role must be one of: {string.Join(", ", ValidRoles)}.");
        }
    }

    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        private static readonly string[] ValidRoles = ["Admin", "User", "Manager"];

        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.FullName)
                .MaximumLength(200).WithMessage("Full name cannot exceed 200 characters.")
                .When(x => !string.IsNullOrEmpty(x.FullName));

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
                .When(x => !string.IsNullOrEmpty(x.Password));

            RuleFor(x => x.Role)
                .Must(role => ValidRoles.Contains(role!))
                .WithMessage($"Role must be one of: {string.Join(", ", ValidRoles)}.")
                .When(x => !string.IsNullOrEmpty(x.Role));
        }
    }
}
