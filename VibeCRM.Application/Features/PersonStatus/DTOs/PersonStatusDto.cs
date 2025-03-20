using System;

namespace VibeCRM.Application.Features.PersonStatus.DTOs
{
    /// <summary>
    /// Data Transfer Object for basic PersonStatus information.
    /// Used for transferring person status data between application layers.
    /// </summary>
    public class PersonStatusDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the person status.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the status name (e.g., "Active", "Inactive", "Prospect").
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the person status.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting person statuses in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
