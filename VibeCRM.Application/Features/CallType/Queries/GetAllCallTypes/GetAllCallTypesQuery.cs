using MediatR;
using VibeCRM.Shared.DTOs.CallType;

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