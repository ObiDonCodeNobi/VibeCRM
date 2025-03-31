using FluentValidation;
using VibeCRM.Shared.DTOs.AccountType;

namespace VibeCRM.Application.Features.AccountType.Validators
{
    /// <summary>
    /// Validator for the AccountTypeListDto class.
    /// Defines validation rules for account type list data.
    /// </summary>
    public class AccountTypeListDtoValidator : AbstractValidator<AccountTypeListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountTypeListDtoValidator"/> class.
        /// Configures validation rules for AccountTypeListDto properties.
        /// </summary>
        public AccountTypeListDtoValidator()
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
        }
    }
}