using MediatR;
using VibeCRM.Application.Features.AccountType.DTOs;

namespace VibeCRM.Application.Features.AccountType.Queries.GetAccountTypeById
{
    /// <summary>
    /// Query to retrieve an account type by its unique identifier.
    /// </summary>
    public class GetAccountTypeByIdQuery : IRequest<AccountTypeListDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the account type to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}