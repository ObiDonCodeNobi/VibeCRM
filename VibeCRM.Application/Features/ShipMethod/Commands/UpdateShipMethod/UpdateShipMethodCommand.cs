using System;
using MediatR;
using VibeCRM.Application.Features.ShipMethod.DTOs;

namespace VibeCRM.Application.Features.ShipMethod.Commands.UpdateShipMethod
{
    /// <summary>
    /// Command for updating an existing shipping method.
    /// </summary>
    public class UpdateShipMethodCommand : IRequest<ShipMethodDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the shipping method to update.
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
    }
}
