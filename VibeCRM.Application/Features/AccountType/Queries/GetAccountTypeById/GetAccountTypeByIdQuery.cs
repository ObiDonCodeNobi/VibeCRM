using MediatR;
using VibeCRM.Shared.DTOs.AccountType;

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