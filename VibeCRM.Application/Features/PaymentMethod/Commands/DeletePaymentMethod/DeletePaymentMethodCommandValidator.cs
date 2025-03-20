using FluentValidation;
using VibeCRM.Application.Features.PaymentMethod.Commands.DeletePaymentMethod;

namespace VibeCRM.Application.Features.PaymentMethod.Commands.DeletePaymentMethod
{
    /// <summary>
    /// Validator for the DeletePaymentMethodCommand
    /// </summary>
    public class DeletePaymentMethodCommandValidator : AbstractValidator<DeletePaymentMethodCommand>
    {
        /// <summary>
        /// Initializes a new instance of the DeletePaymentMethodCommandValidator class
        /// </summary>
        public DeletePaymentMethodCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Payment method ID is required");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required");
        }
    }
}
