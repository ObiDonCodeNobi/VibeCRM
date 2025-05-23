using MediatR;
using VibeCRM.Shared.DTOs.ShipMethod;

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