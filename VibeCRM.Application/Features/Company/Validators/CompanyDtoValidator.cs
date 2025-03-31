using FluentValidation;
using VibeCRM.Shared.DTOs.Company;

namespace VibeCRM.Application.Features.Company.Validators
{
    /// <summary>
    /// Validator for the CompanyDto.
    /// Defines validation rules for company data transfer objects.
    /// </summary>
    public class CompanyDtoValidator : AbstractValidator<CompanyDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public CompanyDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Company name is required.")
                .MaximumLength(200).WithMessage("Company name must not exceed 200 characters.");

            RuleFor(c => c.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(c => c.AccountTypeId)
                .NotEmpty().WithMessage("Account type is required.");

            RuleFor(c => c.AccountStatusId)
                .NotEmpty().WithMessage("Account status is required.");

            RuleFor(c => c.Website)
                .MaximumLength(255).WithMessage("Website URL must not exceed 255 characters.")
                .Matches(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$")
                .When(c => !string.IsNullOrEmpty(c.Website))
                .WithMessage("Website must be a valid URL.");
        }
    }
}