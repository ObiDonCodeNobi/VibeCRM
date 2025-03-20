using FluentValidation;

namespace VibeCRM.Application.Features.AccountStatus.Commands.UpdateAccountStatus
{
    /// <summary>
    /// Validator for the UpdateAccountStatusCommand.
    /// Defines validation rules for account status update commands.
    /// </summary>
    public class UpdateAccountStatusCommandValidator : AbstractValidator<UpdateAccountStatusCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAccountStatusCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public UpdateAccountStatusCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Account status ID is required.");

            RuleFor(c => c.Status)
                .NotEmpty().WithMessage("Status name is required.")
                .MaximumLength(100).WithMessage("Status name must not exceed 100 characters.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(c => c.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}