namespace VibeCRM.Application.Features.PaymentLineItem.DTOs
{
    /// <summary>
    /// List Data Transfer Object for payment line item information.
    /// </summary>
    /// <remarks>
    /// This DTO extends the base PaymentLineItemDto with additional properties for list views.
    /// </remarks>
    public class PaymentLineItemListDto : PaymentLineItemDto
    {
        /// <summary>
        /// Gets or sets the date and time when the payment line item was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who created the payment line item.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
    }
}