namespace VibeCRM.Application.Features.PaymentLineItem.DTOs
{
    /// <summary>
    /// Base Data Transfer Object for payment line item information.
    /// </summary>
    /// <remarks>
    /// This DTO contains the essential properties for transferring payment line item data between layers.
    /// </remarks>
    public class PaymentLineItemDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the payment line item.
        /// </summary>
        public Guid Id { get; set; }

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
    }
}