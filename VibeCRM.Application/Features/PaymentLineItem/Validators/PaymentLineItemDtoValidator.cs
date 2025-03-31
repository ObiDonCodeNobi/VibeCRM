using FluentValidation;
using VibeCRM.Shared.DTOs.PaymentLineItem;

namespace VibeCRM.Application.Features.PaymentLineItem.Validators
{
    /// <summary>
    /// Validator for the PaymentLineItemDto class.
    /// </summary>
    /// <remarks>
    /// Defines validation rules for the base payment line item DTO.
    /// </remarks>
    public class PaymentLineItemDtoValidator : AbstractValidator<PaymentLineItemDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentLineItemDtoValidator"/> class.
        /// </summary>
        public PaymentLineItemDtoValidator()
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
        }
    }
}