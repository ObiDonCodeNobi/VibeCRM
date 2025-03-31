using MediatR;
using VibeCRM.Shared.DTOs.User;

namespace VibeCRM.Application.Features.User.Queries.GetAllUsers
{
    /// <summary>
    /// Query to retrieve all active users
    /// </summary>
    public class GetAllUsersQuery : IRequest<IEnumerable<UserListDto>>
    {
        // No parameters needed for this query
    }
}