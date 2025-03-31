namespace VibeCRM.Shared.DTOs.InvoiceStatus
{
    /// <summary>
    /// Data transfer object for listing invoice statuses with additional information
    /// </summary>
    public class InvoiceStatusListDto
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
        /// Gets or sets the count of invoices with this status
        /// </summary>
        public int InvoiceCount { get; set; }
    }
}