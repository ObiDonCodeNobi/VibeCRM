using MediatR;
using VibeCRM.Application.Features.AccountStatus.DTOs;

namespace VibeCRM.Application.Features.AccountStatus.Queries.GetAccountStatusById
{
    /// <summary>
    /// Query for retrieving an account status by its unique identifier.
    /// Implements the CQRS query pattern for account status retrieval by ID.
    /// </summary>
    public class GetAccountStatusByIdQuery : IRequest<AccountStatusDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the account status to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}