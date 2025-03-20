using FluentValidation;

namespace VibeCRM.Application.Features.PaymentLineItem.Commands.DeletePaymentLineItem
{
    /// <summary>
    /// Validator for the DeletePaymentLineItemCommand class.
    /// </summary>
    /// <remarks>
    /// Defines validation rules for soft-deleting an existing payment line item.
    /// </remarks>
    public class DeletePaymentLineItemCommandValidator : AbstractValidator<DeletePaymentLineItemCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePaymentLineItemCommandValidator"/> class.
        /// </summary>
        public DeletePaymentLineItemCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Payment line item ID is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required.")
                .MaximumLength(100).WithMessage("Modified by cannot exceed 100 characters.");
        }
    }
}