namespace VibeCRM.Shared.DTOs.Invoice
{
    /// <summary>
    /// Detailed Data Transfer Object for Invoice information with additional properties
    /// </summary>
    public class InvoiceDetailsDto : InvoiceDto
    {
        /// <summary>
        /// Gets or sets the date when the invoice was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the invoice
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date when the invoice was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the invoice
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the invoice is active
        /// </summary>
        public bool Active { get; set; }
    }
}