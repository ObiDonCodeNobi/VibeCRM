using FluentValidation;

namespace VibeCRM.Application.Features.PaymentLineItem.Commands.CreatePaymentLineItem
{
    /// <summary>
    /// Validator for the CreatePaymentLineItemCommand class.
    /// </summary>
    /// <remarks>
    /// Defines validation rules for creating a new payment line item.
    /// </remarks>
    public class CreatePaymentLineItemCommandValidator : AbstractValidator<CreatePaymentLineItemCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePaymentLineItemCommandValidator"/> class.
        /// </summary>
        public CreatePaymentLineItemCommandValidator()
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

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by is required.")
                .MaximumLength(100).WithMessage("Created by cannot exceed 100 characters.");
        }
    }
}