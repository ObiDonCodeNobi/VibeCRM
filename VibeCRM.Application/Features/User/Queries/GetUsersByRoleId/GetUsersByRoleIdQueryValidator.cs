using FluentValidation;

namespace VibeCRM.Application.Features.User.Queries.GetUsersByRoleId
{
    /// <summary>
    /// Validator for the GetUsersByRoleIdQuery
    /// </summary>
    public class GetUsersByRoleIdQueryValidator : AbstractValidator<GetUsersByRoleIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUsersByRoleIdQueryValidator"/> class
        /// </summary>
        public GetUsersByRoleIdQueryValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Role ID is required");
        }
    }
}