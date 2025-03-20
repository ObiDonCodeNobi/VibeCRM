namespace VibeCRM.Application.Features.Payment.DTOs
{
    /// <summary>
    /// Data Transfer Object for basic payment information.
    /// This DTO contains the core properties of a payment entity for use in API responses and client applications.
    /// </summary>
    public class PaymentDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the payment.
        /// </summary>
        public Guid Id { get; set; }

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
        public string ReferenceNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets additional notes or comments about the payment.
        /// </summary>
        public string Notes { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the payment is active.
        /// </summary>
        public bool Active { get; set; }
    }
}