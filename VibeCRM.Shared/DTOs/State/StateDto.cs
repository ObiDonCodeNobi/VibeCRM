namespace VibeCRM.Shared.DTOs.State
{
    /// <summary>
    /// Data Transfer Object for basic state information.
    /// </summary>
    public class StateDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the state.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the state or province.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the state or province abbreviation code (e.g., "CA" for California).
        /// </summary>
        public string Abbreviation { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the country code to which this state belongs (e.g., "US" for United States).
        /// </summary>
        public string CountryCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting states in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}