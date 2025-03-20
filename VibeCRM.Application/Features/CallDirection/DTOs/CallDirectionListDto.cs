namespace VibeCRM.Application.Features.CallDirection.DTOs
{
    /// <summary>
    /// Data Transfer Object for call direction information in list views.
    /// Extends the basic CallDirectionDto with additional properties useful for list displays.
    /// </summary>
    public class CallDirectionListDto
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
        /// Useful for displaying usage statistics in list views.
        /// </summary>
        public int ActivityCount { get; set; }
    }
}