using FluentValidation;

namespace VibeCRM.Application.Features.Payment.Commands.DeletePayment
{
    /// <summary>
    /// Validator for the DeletePaymentCommand.
    /// Defines validation rules for soft-deleting a payment.
    /// </summary>
    public class DeletePaymentCommandValidator : AbstractValidator<DeletePaymentCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePaymentCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public DeletePaymentCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Payment ID is required.");

            RuleFor(p => p.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}