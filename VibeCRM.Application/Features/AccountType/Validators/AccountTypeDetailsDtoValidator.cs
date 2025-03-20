using FluentValidation;
using VibeCRM.Application.Features.AccountType.DTOs;

namespace VibeCRM.Application.Features.AccountType.Validators
{
    /// <summary>
    /// Validator for the AccountTypeDetailsDto class.
    /// Defines validation rules for detailed account type data.
    /// </summary>
    public class AccountTypeDetailsDtoValidator : AbstractValidator<AccountTypeDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountTypeDetailsDtoValidator"/> class.
        /// Configures validation rules for AccountTypeDetailsDto properties.
        /// </summary>
        public AccountTypeDetailsDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(System.Guid.Empty).WithMessage("Account type ID is required.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(100).WithMessage("Type cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");

            RuleFor(x => x.CompanyCount)
                .GreaterThanOrEqualTo(0).WithMessage("Company count must be a non-negative number.");

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required.");
        }
    }
}