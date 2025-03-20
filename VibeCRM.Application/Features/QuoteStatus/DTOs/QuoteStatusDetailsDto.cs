namespace VibeCRM.Application.Features.QuoteStatus.DTOs
{
    /// <summary>
    /// Data Transfer Object for detailed quote status information.
    /// Includes audit information and additional details for detailed views.
    /// </summary>
    public class QuoteStatusDetailsDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the quote status.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the quote status (e.g., "Draft", "Sent", "Accepted", "Rejected").
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a detailed description of the quote status.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting quote statuses in listings.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the count of quotes associated with this status.
        /// </summary>
        public int QuoteCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the quote status is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the quote status was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the quote status.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the quote status was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the quote status.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}