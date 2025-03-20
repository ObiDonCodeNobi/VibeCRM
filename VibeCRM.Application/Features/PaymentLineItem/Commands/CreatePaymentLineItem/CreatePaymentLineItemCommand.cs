using MediatR;
using VibeCRM.Application.Features.PaymentLineItem.DTOs;

namespace VibeCRM.Application.Features.PaymentLineItem.Commands.CreatePaymentLineItem
{
    /// <summary>
    /// Command for creating a new payment line item.
    /// </summary>
    /// <remarks>
    /// This command encapsulates all the data needed to create a new payment line item.
    /// </remarks>
    public class CreatePaymentLineItemCommand : IRequest<PaymentLineItemDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the payment line item.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the payment identifier that this line item belongs to.
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the invoice identifier that this payment line item is applied to, if applicable.
        /// </summary>
        public Guid? InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the invoice line item identifier that this payment is applied to, if applicable.
        /// </summary>
        public Guid? InvoiceLineItemId { get; set; }

        /// <summary>
        /// Gets or sets the amount applied to this specific line item.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets a description of what this payment line item represents.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets additional notes or comments about the payment line item.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the user who created the payment line item.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
    }
}