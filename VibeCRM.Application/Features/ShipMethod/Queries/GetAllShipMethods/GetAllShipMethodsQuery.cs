using System.Collections.Generic;
using MediatR;
using VibeCRM.Application.Features.ShipMethod.DTOs;

namespace VibeCRM.Application.Features.ShipMethod.Queries.GetAllShipMethods
{
    /// <summary>
    /// Query for retrieving all shipping methods.
    /// </summary>
    public class GetAllShipMethodsQuery : IRequest<IEnumerable<ShipMethodListDto>>
    {
        // No parameters needed for retrieving all shipping methods
    }
}
