using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CreateTravelExpenseDtoValidator : AbstractValidator<CreateTravelExpenseDto>
    {
        private static readonly string[] ValidStatuses = ["Pending", "Approved", "Rejected"];

        public CreateTravelExpenseDtoValidator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0).WithMessage("Employee ID must be greater than 0.");

            RuleFor(x => x.TravelDate)
                .NotEmpty().WithMessage("Travel date is required.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.Purpose)
                .NotEmpty().WithMessage("Purpose is required.")
                .MaximumLength(500).WithMessage("Purpose cannot exceed 500 characters.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(status => ValidStatuses.Contains(status))
                .WithMessage($"Status must be one of: {string.Join(", ", ValidStatuses)}.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }

    public class UpdateTravelExpenseDtoValidator : AbstractValidator<UpdateTravelExpenseDto>
    {
        private static readonly string[] ValidStatuses = ["Pending", "Approved", "Rejected"];

        public UpdateTravelExpenseDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0.")
                .When(x => x.Amount.HasValue);

            RuleFor(x => x.Purpose)
                .MaximumLength(500).WithMessage("Purpose cannot exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Purpose));

            RuleFor(x => x.Status)
                .Must(status => ValidStatuses.Contains(status!))
                .WithMessage($"Status must be one of: {string.Join(", ", ValidStatuses)}.")
                .When(x => !string.IsNullOrEmpty(x.Status));

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
