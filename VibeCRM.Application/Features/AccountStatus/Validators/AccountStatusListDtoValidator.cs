using FluentValidation;
using VibeCRM.Application.Features.AccountStatus.DTOs;

namespace VibeCRM.Application.Features.AccountStatus.Validators
{
    /// <summary>
    /// Validator for the AccountStatusListDto.
    /// Defines validation rules for account status list data transfer objects.
    /// </summary>
    public class AccountStatusListDtoValidator : AbstractValidator<AccountStatusListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountStatusListDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public AccountStatusListDtoValidator()
        {
            // Include validation rules from the base AccountStatusDto validator
            Include(new AccountStatusDtoValidator());

            // Additional validation rules specific to AccountStatusListDto
            RuleFor(dto => dto.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(dto => dto.CompanyCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Company count must be a non-negative number.");
        }
    }
}