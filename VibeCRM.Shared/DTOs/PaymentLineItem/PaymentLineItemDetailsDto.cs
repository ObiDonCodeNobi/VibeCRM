namespace VibeCRM.Shared.DTOs.PaymentLineItem
{
    /// <summary>
    /// Detailed Data Transfer Object for payment line item information.
    /// </summary>
    /// <remarks>
    /// This DTO extends the base PaymentLineItemDto with additional properties for detailed views.
    /// </remarks>
    public class PaymentLineItemDetailsDto : PaymentLineItemDto
    {
        /// <summary>
        /// Gets or sets additional notes or comments about the payment line item.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the payment line item was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who created the payment line item.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the payment line item was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the payment line item.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the payment line item is active.
        /// </summary>
        public bool Active { get; set; }
    }
}