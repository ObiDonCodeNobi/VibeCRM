using System;

namespace VibeCRM.Application.Features.QuoteStatus.DTOs
{
    /// <summary>
    /// Data Transfer Object for basic quote status information.
    /// Used for standard operations and list displays.
    /// </summary>
    public class QuoteStatusDto
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
    }
}
