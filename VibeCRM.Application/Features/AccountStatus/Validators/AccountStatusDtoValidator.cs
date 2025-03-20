using FluentValidation;
using VibeCRM.Application.Features.AccountStatus.DTOs;

namespace VibeCRM.Application.Features.AccountStatus.Validators
{
    /// <summary>
    /// Validator for the AccountStatusDto.
    /// Defines validation rules for account status data transfer objects.
    /// </summary>
    public class AccountStatusDtoValidator : AbstractValidator<AccountStatusDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountStatusDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public AccountStatusDtoValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage("Account status ID is required.");

            RuleFor(dto => dto.Status)
                .NotEmpty().WithMessage("Status name is required.")
                .MaximumLength(100).WithMessage("Status name must not exceed 100 characters.");

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(dto => dto.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}