using FluentValidation;

namespace VibeCRM.Application.Features.Team.Queries.GetTeamsByUserId
{
    /// <summary>
    /// Validator for the GetTeamsByUserIdQuery
    /// </summary>
    public class GetTeamsByUserIdQueryValidator : AbstractValidator<GetTeamsByUserIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTeamsByUserIdQueryValidator"/> class
        /// </summary>
        public GetTeamsByUserIdQueryValidator()
        {
            // UserId is required
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");
        }
    }
}