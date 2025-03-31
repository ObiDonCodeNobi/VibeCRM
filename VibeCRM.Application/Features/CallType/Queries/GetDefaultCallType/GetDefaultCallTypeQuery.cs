using MediatR;
using VibeCRM.Shared.DTOs.CallType;

namespace VibeCRM.Application.Features.CallType.Queries.GetDefaultCallType
{
    /// <summary>
    /// Query for retrieving the default call type.
    /// Implements IRequest to return a CallTypeDto.
    /// </summary>
    public class GetDefaultCallTypeQuery : IRequest<CallTypeDto>
    {
        // No parameters needed for this query
    }
}