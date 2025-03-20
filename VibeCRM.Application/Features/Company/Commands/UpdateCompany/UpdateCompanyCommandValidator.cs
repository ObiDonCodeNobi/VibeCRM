using FluentValidation;

namespace VibeCRM.Application.Features.Company.Commands.UpdateCompany
{
    /// <summary>
    /// Validator for the UpdateCompanyCommand.
    /// Defines validation rules for company update commands.
    /// </summary>
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCompanyCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public UpdateCompanyCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Company ID is required.");

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

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}