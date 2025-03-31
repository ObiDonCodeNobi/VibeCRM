using MediatR;
using VibeCRM.Shared.DTOs.State;

namespace VibeCRM.Application.Features.State.Queries.GetAllStates
{
    /// <summary>
    /// Query for retrieving all states.
    /// </summary>
    public class GetAllStatesQuery : IRequest<IEnumerable<StateListDto>>
    {
        // No parameters needed for retrieving all states
    }
}