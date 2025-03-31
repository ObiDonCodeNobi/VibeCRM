namespace VibeCRM.Shared.DTOs.CallDirection
{
    /// <summary>
    /// Data Transfer Object for basic call direction information.
    /// Contains the essential properties needed for most call direction operations.
    /// </summary>
    public class CallDirectionDto
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
    }
}