namespace VibeCRM.Shared.DTOs.CallDirection
{
    /// <summary>
    /// Data Transfer Object for detailed call direction information.
    /// Includes all properties of the call direction entity, including audit fields.
    /// </summary>
    public class CallDirectionDetailsDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the call direction.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the direction name (e.g., "Inbound", "Outbound", "Missed").
        /// </summary>
        public string Direction { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the call direction with additional details.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting call directions in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the count of activities using this call direction.
        /// Useful for displaying usage statistics.
        /// </summary>
        public int ActivityCount { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the call direction.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the call direction was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the call direction.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the call direction was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the call direction is active.
        /// Used for soft delete functionality.
        /// </summary>
        public bool Active { get; set; }
    }
}