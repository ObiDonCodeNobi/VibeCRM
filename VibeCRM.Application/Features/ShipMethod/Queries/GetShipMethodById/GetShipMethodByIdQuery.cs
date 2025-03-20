using System;
using MediatR;
using VibeCRM.Application.Features.ShipMethod.DTOs;

namespace VibeCRM.Application.Features.ShipMethod.Queries.GetShipMethodById
{
    /// <summary>
    /// Query for retrieving a shipping method by its unique identifier.
    /// </summary>
    public class GetShipMethodByIdQuery : IRequest<ShipMethodDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the shipping method to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}
