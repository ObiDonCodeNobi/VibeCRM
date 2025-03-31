using MediatR;
using VibeCRM.Shared.DTOs.CallType;

namespace VibeCRM.Application.Features.CallType.Queries.GetInboundCallTypes
{
    /// <summary>
    /// Query for retrieving all inbound call types.
    /// Implements IRequest to return a collection of CallTypeListDto.
    /// </summary>
    public class GetInboundCallTypesQuery : IRequest<IEnumerable<CallTypeListDto>>
    {
        // No parameters needed for this query
    }
}