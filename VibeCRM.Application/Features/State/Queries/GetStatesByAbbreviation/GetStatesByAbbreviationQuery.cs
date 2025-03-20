using MediatR;
using VibeCRM.Application.Features.State.DTOs;

namespace VibeCRM.Application.Features.State.Queries.GetStatesByAbbreviation
{
    /// <summary>
    /// Query for retrieving states by their abbreviation.
    /// </summary>
    public class GetStatesByAbbreviationQuery : IRequest<IEnumerable<StateListDto>>
    {
        /// <summary>
        /// Gets or sets the abbreviation or partial abbreviation to search for.
        /// </summary>
        public string Abbreviation { get; set; } = string.Empty;
    }
}