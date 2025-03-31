using MediatR;
using VibeCRM.Shared.DTOs.AccountStatus;

namespace VibeCRM.Application.Features.AccountStatus.Queries.GetAccountStatusByOrdinalPosition
{
    /// <summary>
    /// Query for retrieving an account status by its ordinal position.
    /// Implements the CQRS query pattern for account status retrieval by ordinal position.
    /// </summary>
    public class GetAccountStatusByOrdinalPositionQuery : IRequest<AccountStatusDetailsDto>
    {
        /// <summary>
        /// Gets or sets the ordinal position of the account status to retrieve.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}