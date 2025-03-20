using System.Collections.Generic;
using MediatR;
using VibeCRM.Application.Features.State.DTOs;

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
