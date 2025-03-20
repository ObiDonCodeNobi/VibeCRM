using FluentValidation;
using VibeCRM.Application.Features.Role.Queries.GetAllRoles;

namespace VibeCRM.Application.Features.Role.Validators
{
    /// <summary>
    /// Validator for the GetAllRolesQuery
    /// </summary>
    public class GetAllRolesQueryValidator : AbstractValidator<GetAllRolesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllRolesQueryValidator"/> class
        /// </summary>
        public GetAllRolesQueryValidator()
        {
            // No validation rules needed for this query as it doesn't have any parameters
        }
    }
}