using System;
using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MinimumLength(4).WithMessage("Full name cannot be less than 4 characters.")
                .MaximumLength(200).WithMessage("Full name cannot exceed 200 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.EmployeeCode)
                .MaximumLength(50).WithMessage("Employee code cannot exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.EmployeeCode));

            RuleFor(x => x.OrganizationId)
                .GreaterThan(0).WithMessage("Organization ID must be greater than 0.")
                .When(x => x.OrganizationId.HasValue);

            RuleFor(x => x.HireDate)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Hire date cannot be in the future.")
                .When(x => x.HireDate.HasValue);

            RuleFor(x => x.Salary)
                .GreaterThanOrEqualTo(0).WithMessage("Salary cannot be negative.")
                .When(x => x.Salary.HasValue);

            RuleFor(x => x.Position)
                .MaximumLength(100).WithMessage("Position cannot exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Position));
        }
    }

    public class UpdateEmployeeDtoValidator : AbstractValidator<UpdateEmployeeDto>
    {
        public UpdateEmployeeDtoValidator()
        {
            RuleFor(x => x.FullName)
                .MaximumLength(200).WithMessage("Full name cannot exceed 200 characters.")
                .When(x => !string.IsNullOrEmpty(x.FullName));

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.EmployeeCode)
                .MaximumLength(50).WithMessage("Employee code cannot exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.EmployeeCode));

            RuleFor(x => x.OrganizationId)
                .GreaterThan(0).WithMessage("Organization ID must be greater than 0.")
                .When(x => x.OrganizationId.HasValue);

            RuleFor(x => x.HireDate)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Hire date cannot be in the future.")
                .When(x => x.HireDate.HasValue);

            RuleFor(x => x.Salary)
                .GreaterThanOrEqualTo(0).WithMessage("Salary cannot be negative.")
                .When(x => x.Salary.HasValue);

            RuleFor(x => x.Position)
                .MaximumLength(100).WithMessage("Position cannot exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Position));
        }
    }
}
