using MediatR;
using VibeCRM.Application.Features.State.DTOs;

namespace VibeCRM.Application.Features.State.Queries.GetStatesByName
{
    /// <summary>
    /// Query for retrieving states by their name.
    /// </summary>
    public class GetStatesByNameQuery : IRequest<IEnumerable<StateListDto>>
    {
        /// <summary>
        /// Gets or sets the name or partial name to search for.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}