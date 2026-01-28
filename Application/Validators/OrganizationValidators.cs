using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CreateOrganizationDtoValidator : AbstractValidator<CreateOrganizationDto>
    {
        public CreateOrganizationDtoValidator()
        {
            RuleFor(x => x.OrganizationName)
                .NotEmpty().WithMessage("Organization name is required.")
                .MaximumLength(200).WithMessage("Organization name cannot exceed 200 characters.");

            RuleFor(x => x.Address)
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.ContactEmail)
                .EmailAddress().WithMessage("Invalid email format.")
                .When(x => !string.IsNullOrEmpty(x.ContactEmail));

            RuleFor(x => x.ContactPhone)
                .Matches(@"^\+?[\d\s\-()]+$").WithMessage("Invalid phone number format.")
                .When(x => !string.IsNullOrEmpty(x.ContactPhone));
        }
    }

    public class UpdateOrganizationDtoValidator : AbstractValidator<UpdateOrganizationDto>
    {
        public UpdateOrganizationDtoValidator()
        {
            RuleFor(x => x.OrganizationName)
                .MaximumLength(200).WithMessage("Organization name cannot exceed 200 characters.")
                .When(x => !string.IsNullOrEmpty(x.OrganizationName));

            RuleFor(x => x.Address)
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.ContactEmail)
                .EmailAddress().WithMessage("Invalid email format.")
                .When(x => !string.IsNullOrEmpty(x.ContactEmail));

            RuleFor(x => x.ContactPhone)
                .Matches(@"^\+?[\d\s\-()]+$").WithMessage("Invalid phone number format.")
                .When(x => !string.IsNullOrEmpty(x.ContactPhone));
        }
    }
}
