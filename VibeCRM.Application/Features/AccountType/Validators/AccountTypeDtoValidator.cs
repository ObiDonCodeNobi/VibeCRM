using FluentValidation;
using VibeCRM.Shared.DTOs.AccountType;

namespace VibeCRM.Application.Features.AccountType.Validators
{
    /// <summary>
    /// Validator for the AccountTypeDto class.
    /// Defines validation rules for account type data.
    /// </summary>
    public class AccountTypeDtoValidator : AbstractValidator<AccountTypeDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountTypeDtoValidator"/> class.
        /// Configures validation rules for AccountTypeDto properties.
        /// </summary>
        public AccountTypeDtoValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(100).WithMessage("Type cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}