using FluentValidation;
using VibeCRM.Application.Features.Company.DTOs;

namespace VibeCRM.Application.Features.Company.Validators
{
    /// <summary>
    /// Validator for the CompanyListDto.
    /// Defines validation rules for company list data transfer objects.
    /// </summary>
    public class CompanyListDtoValidator : AbstractValidator<CompanyListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyListDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public CompanyListDtoValidator()
        {
            Include(new CompanyDtoValidator());

            RuleFor(c => c.AccountTypeName)
                .NotEmpty().WithMessage("Account type name is required.")
                .MaximumLength(100).WithMessage("Account type name must not exceed 100 characters.");

            RuleFor(c => c.AccountStatusName)
                .NotEmpty().WithMessage("Account status name is required.")
                .MaximumLength(100).WithMessage("Account status name must not exceed 100 characters.");

            RuleFor(c => c.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");
        }
    }
}