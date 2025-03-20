using FluentValidation;

namespace VibeCRM.Application.Features.User.Queries.GetAllUsers
{
    /// <summary>
    /// Validator for the GetAllUsersQuery
    /// </summary>
    public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllUsersQueryValidator"/> class
        /// </summary>
        public GetAllUsersQueryValidator()
        {
            // No validation rules needed as this query has no parameters
        }
    }
}