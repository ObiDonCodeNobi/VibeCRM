using MediatR;
using VibeCRM.Application.Features.PersonStatus.DTOs;

namespace VibeCRM.Application.Features.PersonStatus.Queries.GetPersonStatusById
{
    /// <summary>
    /// Query for retrieving a person status by its ID.
    /// Implements the CQRS query pattern for retrieving a single person status entity.
    /// </summary>
    public class GetPersonStatusByIdQuery : IRequest<PersonStatusDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the person status to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}