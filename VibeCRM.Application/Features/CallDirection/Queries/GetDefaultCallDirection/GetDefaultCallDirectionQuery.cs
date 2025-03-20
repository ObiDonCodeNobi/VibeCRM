using MediatR;
using VibeCRM.Application.Features.CallDirection.DTOs;

namespace VibeCRM.Application.Features.CallDirection.Queries.GetDefaultCallDirection
{
    /// <summary>
    /// Query to retrieve the default call direction.
    /// The default call direction is typically the one with the lowest ordinal position.
    /// </summary>
    public class GetDefaultCallDirectionQuery : IRequest<CallDirectionDto>
    {
        // This query doesn't require any parameters as it retrieves the default call direction
    }
}