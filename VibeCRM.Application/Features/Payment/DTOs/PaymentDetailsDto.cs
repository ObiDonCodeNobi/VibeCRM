namespace VibeCRM.Application.Features.Payment.DTOs
{
    /// <summary>
    /// Data Transfer Object for detailed payment information.
    /// This DTO extends the base PaymentDto with additional properties for detailed views.
    /// </summary>
    public class PaymentDetailsDto : PaymentDto
    {
        /// <summary>
        /// Gets or sets the name of the payment type.
        /// </summary>
        public string PaymentTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the payment method type.
        /// </summary>
        public string PaymentMethodTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the payment status.
        /// </summary>
        public string PaymentStatusName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the invoice number associated with this payment, if applicable.
        /// </summary>
        public string InvoiceNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the company associated with this payment, if applicable.
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the person associated with this payment, if applicable.
        /// </summary>
        public string PersonName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identifier of the user who created the payment.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the payment was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the payment.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the payment was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}