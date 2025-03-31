using MediatR;
using VibeCRM.Shared.DTOs.CallType;

namespace VibeCRM.Application.Features.CallType.Queries.GetOutboundCallTypes
{
    /// <summary>
    /// Query for retrieving all outbound call types.
    /// Implements IRequest to return a collection of CallTypeListDto.
    /// </summary>
    public class GetOutboundCallTypesQuery : IRequest<IEnumerable<CallTypeListDto>>
    {
        // No parameters needed for this query
    }
}