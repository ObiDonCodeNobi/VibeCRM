namespace VibeCRM.Application.Features.ShipMethod.DTOs
{
    /// <summary>
    /// Data Transfer Object for detailed shipping method information.
    /// Includes audit information and additional details.
    /// </summary>
    public class ShipMethodDetailsDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the shipping method.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the shipping method (e.g., "Standard", "Express", "Overnight").
        /// </summary>
        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the shipping method with details about delivery times and costs.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting shipping methods in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the number of orders using this shipping method.
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// Gets or sets the date when the shipping method was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the shipping method.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date when the shipping method was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the shipping method.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the shipping method is active.
        /// </summary>
        public bool Active { get; set; }
    }
}