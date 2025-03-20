using MediatR;
using VibeCRM.Application.Features.Call.DTOs;

namespace VibeCRM.Application.Features.Call.Queries.GetCallById
{
    /// <summary>
    /// Query for retrieving a specific call by its unique identifier.
    /// Implements the CQRS query pattern for call retrieval.
    /// </summary>
    public class GetCallByIdQuery : IRequest<CallDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCallByIdQuery"/> class.
        /// </summary>
        public GetCallByIdQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCallByIdQuery"/> class with a specified call ID.
        /// </summary>
        /// <param name="id">The unique identifier of the call to retrieve.</param>
        public GetCallByIdQuery(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the call to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}