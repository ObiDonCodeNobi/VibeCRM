using MediatR;
using VibeCRM.Application.Features.State.DTOs;

namespace VibeCRM.Application.Features.State.Queries.GetStateById
{
    /// <summary>
    /// Query for retrieving a state by its unique identifier.
    /// </summary>
    public class GetStateByIdQuery : IRequest<StateDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the state to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}