using MediatR;
using VibeCRM.Application.Features.AccountStatus.DTOs;

namespace VibeCRM.Application.Features.AccountStatus.Queries.GetAccountStatusByStatus
{
    /// <summary>
    /// Query for retrieving an account status by its status name.
    /// Implements the CQRS query pattern for account status retrieval by status name.
    /// </summary>
    public class GetAccountStatusByStatusQuery : IRequest<AccountStatusDetailsDto>
    {
        /// <summary>
        /// Gets or sets the status name of the account status to retrieve.
        /// </summary>
        public string Status { get; set; }

        public GetAccountStatusByStatusQuery() { Status = string.Empty; }
    }
}