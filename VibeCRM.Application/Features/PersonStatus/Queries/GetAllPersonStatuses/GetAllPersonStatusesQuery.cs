using MediatR;
using VibeCRM.Shared.DTOs.PersonStatus;

namespace VibeCRM.Application.Features.PersonStatus.Queries.GetAllPersonStatuses
{
    /// <summary>
    /// Query for retrieving all person statuses in the system.
    /// Implements the CQRS query pattern for retrieving a collection of person statuses.
    /// </summary>
    public class GetAllPersonStatusesQuery : IRequest<IEnumerable<PersonStatusListDto>>
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include inactive (soft-deleted) person statuses.
        /// </summary>
        public bool IncludeInactive { get; set; } = false;
    }
}