using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CreateAttendenceDtoValidator : AbstractValidator<CreateAttendenceDto>
    {
        public CreateAttendenceDtoValidator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0).WithMessage("Employee ID must be greater than 0.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Date cannot be in the future.");

            RuleFor(x => x.CheckOut)
                .GreaterThan(x => x.CheckIn)
                .WithMessage("Check-out time must be after check-in time.")
                .When(x => x.CheckIn.HasValue && x.CheckOut.HasValue);
        }
    }

    public class UpdateAttendenceDtoValidator : AbstractValidator<UpdateAttendenceDto>
    {
        public UpdateAttendenceDtoValidator()
        {
            RuleFor(x => x.CheckOut)
                .GreaterThan(x => x.CheckIn)
                .WithMessage("Check-out time must be after check-in time.")
                .When(x => x.CheckIn.HasValue && x.CheckOut.HasValue);
        }
    }
}
