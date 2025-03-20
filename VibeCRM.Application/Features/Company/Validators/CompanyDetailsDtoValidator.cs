using FluentValidation;
using VibeCRM.Application.Features.Company.DTOs;

namespace VibeCRM.Application.Features.Company.Validators
{
    /// <summary>
    /// Validator for the CompanyDetailsDto.
    /// Defines validation rules for detailed company data transfer objects.
    /// </summary>
    public class CompanyDetailsDtoValidator : AbstractValidator<CompanyDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyDetailsDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public CompanyDetailsDtoValidator()
        {
            Include(new CompanyDtoValidator());

            RuleFor(c => c.AccountTypeName)
                .NotEmpty().WithMessage("Account type name is required.")
                .MaximumLength(100).WithMessage("Account type name must not exceed 100 characters.");

            RuleFor(c => c.AccountStatusName)
                .NotEmpty().WithMessage("Account status name is required.")
                .MaximumLength(100).WithMessage("Account status name must not exceed 100 characters.");

            RuleFor(c => c.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required.");

            RuleFor(c => c.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");

            RuleFor(c => c.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.");
        }
    }
}