namespace VibeCRM.Application.Features.InvoiceStatus.DTOs
{
    /// <summary>
    /// Detailed data transfer object for invoice status, including audit information
    /// </summary>
    public class InvoiceStatusDetailsDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the invoice status
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the invoice status (e.g., "Draft", "Sent", "Paid", "Overdue")
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a detailed description of the invoice status
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting invoice statuses in listings
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the invoice status was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the invoice status
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the invoice status was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the invoice status
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the invoice status is active
        /// </summary>
        public bool Active { get; set; }
    }
}
