using MediatR;
using VibeCRM.Shared.DTOs.Person;

namespace VibeCRM.Application.Features.Person.Queries.GetPersonById
{
    /// <summary>
    /// Query to retrieve a specific person by their unique identifier.
    /// This is used in the CQRS pattern as the request object for retrieving person details.
    /// </summary>
    public class GetPersonByIdQuery : IRequest<PersonDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the person to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}