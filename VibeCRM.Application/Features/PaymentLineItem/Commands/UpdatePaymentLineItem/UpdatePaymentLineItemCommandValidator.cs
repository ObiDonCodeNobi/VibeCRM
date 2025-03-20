using FluentValidation;

namespace VibeCRM.Application.Features.PaymentLineItem.Commands.UpdatePaymentLineItem
{
    /// <summary>
    /// Validator for the UpdatePaymentLineItemCommand class.
    /// </summary>
    /// <remarks>
    /// Defines validation rules for updating an existing payment line item.
    /// </remarks>
    public class UpdatePaymentLineItemCommandValidator : AbstractValidator<UpdatePaymentLineItemCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePaymentLineItemCommandValidator"/> class.
        /// </summary>
        public UpdatePaymentLineItemCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Payment line item ID is required.");

            RuleFor(x => x.PaymentId)
                .NotEmpty().WithMessage("Payment ID is required.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            // Either InvoiceId or InvoiceLineItemId should be provided
            RuleFor(x => new { x.InvoiceId, x.InvoiceLineItemId })
                .Must(x => x.InvoiceId.HasValue || x.InvoiceLineItemId.HasValue)
                .WithMessage("Either Invoice ID or Invoice Line Item ID must be provided.");

            RuleFor(x => x.Notes)
                .MaximumLength(2000).WithMessage("Notes cannot exceed 2000 characters.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required.")
                .MaximumLength(100).WithMessage("Modified by cannot exceed 100 characters.");
        }
    }
}