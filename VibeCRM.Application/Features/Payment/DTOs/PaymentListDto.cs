namespace VibeCRM.Application.Features.Payment.DTOs
{
    /// <summary>
    /// Data Transfer Object for payment list information.
    /// This DTO contains optimized properties for displaying payments in lists.
    /// </summary>
    public class PaymentListDto : PaymentDto
    {
        /// <summary>
        /// Gets or sets the name of the payment type.
        /// </summary>
        public string PaymentTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the payment status.
        /// </summary>
        public string PaymentStatusName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the invoice number associated with this payment, if applicable.
        /// </summary>
        public string InvoiceNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the payment was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}