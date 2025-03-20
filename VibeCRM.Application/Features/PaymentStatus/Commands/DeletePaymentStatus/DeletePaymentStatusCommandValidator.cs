using FluentValidation;

namespace VibeCRM.Application.Features.PaymentStatus.Commands.DeletePaymentStatus
{
    /// <summary>
    /// Validator for the DeletePaymentStatusCommand.
    /// Defines validation rules for payment status deletion commands.
    /// </summary>
    public class DeletePaymentStatusCommandValidator : AbstractValidator<DeletePaymentStatusCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePaymentStatusCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public DeletePaymentStatusCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Payment status ID is required.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}
