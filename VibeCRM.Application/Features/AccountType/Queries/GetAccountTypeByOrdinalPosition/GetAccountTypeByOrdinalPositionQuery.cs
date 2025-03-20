using MediatR;
using VibeCRM.Application.Features.AccountType.DTOs;

namespace VibeCRM.Application.Features.AccountType.Queries.GetAccountTypeByOrdinalPosition
{
    /// <summary>
    /// Query to retrieve account types ordered by their ordinal position.
    /// </summary>
    public class GetAccountTypeByOrdinalPositionQuery : IRequest<IEnumerable<AccountTypeListDto>>
    {
        // This is a parameter-less query to retrieve account types ordered by ordinal position
    }
}