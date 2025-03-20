using MediatR;
using VibeCRM.Application.Features.Person.DTOs;

namespace VibeCRM.Application.Features.Person.Queries.GetAllPersons
{
    /// <summary>
    /// Query to retrieve all active persons in the system.
    /// This is used in the CQRS pattern as the request object for retrieving person lists.
    /// </summary>
    public class GetAllPersonsQuery : IRequest<IEnumerable<PersonListDto>>
    {
        /// <summary>
        /// Gets or sets the optional search term to filter persons by name.
        /// </summary>
        public string? SearchTerm { get; set; }

        /// <summary>
        /// Gets or sets the page number for pagination (1-based).
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the page size for pagination.
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}