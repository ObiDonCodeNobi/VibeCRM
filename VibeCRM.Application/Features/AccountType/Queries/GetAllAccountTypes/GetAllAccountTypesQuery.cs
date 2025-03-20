using MediatR;
using VibeCRM.Application.Features.AccountType.DTOs;

namespace VibeCRM.Application.Features.AccountType.Queries.GetAllAccountTypes
{
    /// <summary>
    /// Query to retrieve all account types in the system.
    /// </summary>
    public class GetAllAccountTypesQuery : IRequest<IEnumerable<AccountTypeListDto>>
    {
        // This is a parameter-less query to retrieve all account types
    }
}