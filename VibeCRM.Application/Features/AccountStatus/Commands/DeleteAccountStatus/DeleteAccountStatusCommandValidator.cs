using FluentValidation;

namespace VibeCRM.Application.Features.AccountStatus.Commands.DeleteAccountStatus
{
    /// <summary>
    /// Validator for the DeleteAccountStatusCommand.
    /// Defines validation rules for account status deletion commands.
    /// </summary>
    public class DeleteAccountStatusCommandValidator : AbstractValidator<DeleteAccountStatusCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAccountStatusCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public DeleteAccountStatusCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Account status ID is required.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}