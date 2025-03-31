using FluentValidation;
using VibeCRM.Shared.DTOs.AccountStatus;

namespace VibeCRM.Application.Features.AccountStatus.Validators
{
    /// <summary>
    /// Validator for the AccountStatusDetailsDto.
    /// Defines validation rules for detailed account status data transfer objects.
    /// </summary>
    public class AccountStatusDetailsDtoValidator : AbstractValidator<AccountStatusDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountStatusDetailsDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public AccountStatusDetailsDtoValidator()
        {
            // Include validation rules from the base AccountStatusDto validator
            Include(new AccountStatusDtoValidator());

            // Additional validation rules specific to AccountStatusDetailsDto
            RuleFor(dto => dto.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required.");

            RuleFor(dto => dto.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(dto => dto.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");

            RuleFor(dto => dto.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.")
                .GreaterThanOrEqualTo(dto => dto.CreatedDate)
                .WithMessage("Modified date must be after or equal to the created date.");

            RuleFor(dto => dto.CompanyCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Company count must be a non-negative number.");
        }
    }
}