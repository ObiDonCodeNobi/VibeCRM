using MediatR;
using VibeCRM.Application.Features.AccountStatus.DTOs;

namespace VibeCRM.Application.Features.AccountStatus.Queries.GetAllAccountStatuses
{
    /// <summary>
    /// Query for retrieving all active account statuses.
    /// Implements the CQRS query pattern for account status retrieval.
    /// </summary>
    public class GetAllAccountStatusesQuery : IRequest<IEnumerable<AccountStatusListDto>>
    {
        // This is an empty query that doesn't require any parameters
        // It will return all active account statuses
    }
}