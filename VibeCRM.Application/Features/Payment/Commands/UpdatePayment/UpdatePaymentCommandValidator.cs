using FluentValidation;

namespace VibeCRM.Application.Features.Payment.Commands.UpdatePayment
{
    /// <summary>
    /// Validator for the UpdatePaymentCommand.
    /// Defines validation rules for updating an existing payment.
    /// </summary>
    public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePaymentCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public UpdatePaymentCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Payment ID is required.");

            RuleFor(p => p.PaymentTypeId)
                .NotEmpty().WithMessage("Payment type is required.");

            RuleFor(p => p.PaymentStatusId)
                .NotEmpty().WithMessage("Payment status is required.");

            RuleFor(p => p.PaymentDate)
                .NotEmpty().WithMessage("Payment date is required.");

            RuleFor(p => p.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(p => p.ReferenceNumber)
                .NotEmpty().WithMessage("Reference number is required.")
                .MaximumLength(100).WithMessage("Reference number must not exceed 100 characters.");

            RuleFor(p => p.Notes)
                .MaximumLength(1000).WithMessage("Notes must not exceed 1000 characters.");

            RuleFor(p => p.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}