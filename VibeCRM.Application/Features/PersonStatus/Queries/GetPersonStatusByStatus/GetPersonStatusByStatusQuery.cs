using MediatR;
using VibeCRM.Shared.DTOs.PersonStatus;

namespace VibeCRM.Application.Features.PersonStatus.Queries.GetPersonStatusByStatus
{
    /// <summary>
    /// Query for retrieving person statuses by status name.
    /// Implements the CQRS query pattern for retrieving person status entities by status name.
    /// </summary>
    public class GetPersonStatusByStatusQuery : IRequest<IEnumerable<PersonStatusListDto>>
    {
        /// <summary>
        /// Gets or sets the status name to search for.
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}