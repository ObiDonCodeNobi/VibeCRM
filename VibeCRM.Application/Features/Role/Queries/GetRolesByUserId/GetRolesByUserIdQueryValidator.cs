using FluentValidation;
using VibeCRM.Application.Features.Role.Queries.GetRolesByUserId;

namespace VibeCRM.Application.Features.Role.Validators
{
    /// <summary>
    /// Validator for the GetRolesByUserIdQuery
    /// </summary>
    public class GetRolesByUserIdQueryValidator : AbstractValidator<GetRolesByUserIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRolesByUserIdQueryValidator"/> class
        /// </summary>
        public GetRolesByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required");
        }
    }
}