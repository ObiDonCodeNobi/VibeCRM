using MediatR;
using VibeCRM.Application.Features.PaymentLineItem.DTOs;

namespace VibeCRM.Application.Features.PaymentLineItem.Commands.UpdatePaymentLineItem
{
    /// <summary>
    /// Command for updating an existing payment line item.
    /// </summary>
    /// <remarks>
    /// This command encapsulates all the data needed to update an existing payment line item.
    /// </remarks>
    public class UpdatePaymentLineItemCommand : IRequest<PaymentLineItemDetailsDto>
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

        /// <summary>
        /// Gets or sets additional notes or comments about the payment line item.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the user who modified the payment line item.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}