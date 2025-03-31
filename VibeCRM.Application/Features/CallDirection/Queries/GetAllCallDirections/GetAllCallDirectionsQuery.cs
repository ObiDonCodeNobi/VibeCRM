using MediatR;
using VibeCRM.Shared.DTOs.CallDirection;

namespace VibeCRM.Application.Features.CallDirection.Queries.GetAllCallDirections
{
    /// <summary>
    /// Query to retrieve all active call directions.
    /// </summary>
    public class GetAllCallDirectionsQuery : IRequest<IEnumerable<CallDirectionListDto>>
    {
        // This query doesn't require any parameters as it retrieves all active call directions
    }
}