namespace VibeCRM.Shared.DTOs.CallType
{
    /// <summary>
    /// Data Transfer Object for detailed call type information.
    /// Includes all properties of the call type entity, including audit fields.
    /// </summary>
    public class CallTypeDetailsDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the call type.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Sales", "Support", "Follow-up").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the call type with additional details.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting call types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the count of calls using this call type.
        /// Useful for displaying usage statistics.
        /// </summary>
        public int CallCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this call type is used for inbound calls.
        /// </summary>
        public bool IsInbound { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this call type is used for outbound calls.
        /// </summary>
        public bool IsOutbound { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the call type.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the call type was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the call type.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the call type was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the call type is active.
        /// Used for soft delete functionality.
        /// </summary>
        public bool Active { get; set; }
    }
}