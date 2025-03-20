using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a payment made by a customer in the system.
    /// </summary>
    public class Payment : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Payment"/> class.
        /// </summary>
        public Payment()
        {
            PaymentLineItems = new List<PaymentLineItem>();
            Id = Guid.NewGuid();
            PaymentDate = DateTime.UtcNow;
            ReferenceNumber = string.Empty;
        }

        /// <summary>
        /// Gets or sets the payment identifier that directly maps to the PaymentId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid PaymentId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the invoice that this payment is associated with, if applicable.
        /// </summary>
        public Guid? InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the payment type identifier indicating how the payment was made (e.g., credit card, cash, check).
        /// </summary>
        public Guid PaymentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the payment method type identifier (e.g., Visa, Mastercard, PayPal).
        /// </summary>
        public Guid? PaymentMethodTypeId { get; set; }

        /// <summary>
        /// Gets or sets the status identifier of the payment.
        /// </summary>
        public Guid PaymentStatusId { get; set; }

        /// <summary>
        /// Gets or sets the date when the payment was made.
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the payment.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the external reference number for the payment, such as a check number or transaction ID.
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets additional notes or comments about the payment.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets a collection of line items associated with this payment.
        /// </summary>
        /// <remarks>
        /// Payment line items allow a single payment to be applied to multiple invoices or charges.
        /// </remarks>
        public ICollection<PaymentLineItem> PaymentLineItems { get; set; }
    }
}