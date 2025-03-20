using MediatR;
using VibeCRM.Application.Features.CallType.DTOs;

namespace VibeCRM.Application.Features.CallType.Queries.GetAllCallTypes
{
    /// <summary>
    /// Query for retrieving all active call types.
    /// Implements IRequest to return a collection of CallTypeListDto.
    /// </summary>
    public class GetAllCallTypesQuery : IRequest<IEnumerable<CallTypeListDto>>
    {
        // No parameters needed for this query
    }
}