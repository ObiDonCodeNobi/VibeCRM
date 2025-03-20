namespace VibeCRM.Application.Features.Invoice.DTOs
{
    /// <summary>
    /// Data Transfer Object for Invoice information optimized for list views
    /// </summary>
    public class InvoiceListDto : InvoiceDto
    {
        /// <summary>
        /// Gets or sets the date when the invoice was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the invoice was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}