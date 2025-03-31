using MediatR;
using VibeCRM.Shared.DTOs.AccountType;

namespace VibeCRM.Application.Features.AccountType.Queries.GetAccountTypeByType
{
    /// <summary>
    /// Query to retrieve account types by their type name.
    /// </summary>
    public class GetAccountTypeByTypeQuery : IRequest<IEnumerable<AccountTypeListDto>>
    {
        /// <summary>
        /// Gets or sets the type name to search for.
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}