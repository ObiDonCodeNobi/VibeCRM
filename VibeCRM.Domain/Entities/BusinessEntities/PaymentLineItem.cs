using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a line item within a payment in the system.
    /// </summary>
    /// <remarks>
    /// Payment line items allow a single payment to be applied to multiple invoices or charges.
    /// Each line item can reference a specific invoice, invoice line item, or other billable entity.
    /// </remarks>
    public class PaymentLineItem : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentLineItem"/> class.
        /// </summary>
        public PaymentLineItem()
        {
            Id = Guid.NewGuid();
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the payment line item.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid PaymentLineItemId
        {
            get => Id;
            set => Id = value;
        }

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
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets additional notes or comments about the payment line item.
        /// </summary>
        public string? Notes { get; set; }
    }
}