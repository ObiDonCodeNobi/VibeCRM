namespace VibeCRM.Shared.DTOs.CallType
{
    /// <summary>
    /// Data Transfer Object for call type information in list views.
    /// Extends the basic CallTypeDto with additional properties useful for list displays.
    /// </summary>
    public class CallTypeListDto
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
        /// Useful for displaying usage statistics in list views.
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
    }
}