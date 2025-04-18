using MediatR;
using VibeCRM.Shared.DTOs.ShipMethod;

namespace VibeCRM.Application.Features.ShipMethod.Queries.GetDefaultShipMethod
{
    /// <summary>
    /// Query for retrieving the default shipping method (the one with the lowest ordinal position).
    /// </summary>
    public class GetDefaultShipMethodQuery : IRequest<ShipMethodDto>
    {
        // No parameters needed for retrieving the default shipping method
    }
}