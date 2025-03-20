using MediatR;
using VibeCRM.Application.Features.State.DTOs;

namespace VibeCRM.Application.Features.State.Queries.GetDefaultState
{
    /// <summary>
    /// Query for retrieving the default state.
    /// </summary>
    public class GetDefaultStateQuery : IRequest<StateDto>
    {
        // No parameters needed for retrieving the default state
    }
}
