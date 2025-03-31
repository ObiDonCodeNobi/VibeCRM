namespace VibeCRM.Shared.DTOs.CallType
{
    /// <summary>
    /// Data Transfer Object for basic call type information.
    /// Contains the essential properties of a call type.
    /// </summary>
    public class CallTypeDto
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
    }
}