using MediatR;
using VibeCRM.Shared.DTOs.PersonStatus;

namespace VibeCRM.Application.Features.PersonStatus.Queries.GetDefaultPersonStatus
{
    /// <summary>
    /// Query for retrieving the default person status.
    /// Implements the CQRS query pattern for retrieving the default person status entity.
    /// </summary>
    public class GetDefaultPersonStatusQuery : IRequest<PersonStatusDto>
    {
        // No parameters needed for this query
    }
}