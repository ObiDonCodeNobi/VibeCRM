using System;

namespace VibeCRM.Application.Features.ShipMethod.DTOs
{
    /// <summary>
    /// Data Transfer Object for shipping method information in list views.
    /// Includes additional information like order count.
    /// </summary>
    public class ShipMethodListDto
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
    }
}
