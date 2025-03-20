using MediatR;
using VibeCRM.Application.Features.ShipMethod.DTOs;

namespace VibeCRM.Application.Features.ShipMethod.Queries.GetShipMethodByMethod
{
    /// <summary>
    /// Query for retrieving shipping methods by their method name.
    /// </summary>
    public class GetShipMethodByMethodQuery : IRequest<IEnumerable<ShipMethodListDto>>
    {
        /// <summary>
        /// Gets or sets the method name to search for.
        /// </summary>
        public string Method { get; set; } = string.Empty;
    }
}