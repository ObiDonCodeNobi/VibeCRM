using FluentValidation;

namespace VibeCRM.Application.Features.Company.Commands.DeleteCompany
{
    /// <summary>
    /// Validator for the DeleteCompanyCommand.
    /// Defines validation rules for company deletion commands.
    /// </summary>
    public class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCompanyCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public DeleteCompanyCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Company ID is required.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}