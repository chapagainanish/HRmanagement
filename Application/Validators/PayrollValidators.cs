using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CreatePayrollDtoValidator : AbstractValidator<CreatePayrollDto>
    {
        public CreatePayrollDtoValidator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0).WithMessage("Employee ID must be greater than 0.");

            RuleFor(x => x.SalaryMonth)
                .NotEmpty().WithMessage("Salary month is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Salary month cannot be in the future.");

            RuleFor(x => x.BasicSalary)
                .GreaterThan(0).WithMessage("Basic salary must be greater than 0.");

            RuleFor(x => x.Allowance)
                .GreaterThanOrEqualTo(0).WithMessage("Allowance cannot be negative.");

            RuleFor(x => x.Deduction)
                .GreaterThanOrEqualTo(0).WithMessage("Deduction cannot be negative.");
        }
    }

    public class UpdatePayrollDtoValidator : AbstractValidator<UpdatePayrollDto>
    {
        public UpdatePayrollDtoValidator()
        {
            RuleFor(x => x.BasicSalary)
                .GreaterThan(0).WithMessage("Basic salary must be greater than 0.")
                .When(x => x.BasicSalary.HasValue);

            RuleFor(x => x.Allowance)
                .GreaterThanOrEqualTo(0).WithMessage("Allowance cannot be negative.")
                .When(x => x.Allowance.HasValue);

            RuleFor(x => x.Deduction)
                .GreaterThanOrEqualTo(0).WithMessage("Deduction cannot be negative.")
                .When(x => x.Deduction.HasValue);
        }
    }
}
