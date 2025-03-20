using FluentValidation;

namespace VibeCRM.Application.Features.User.Queries.GetUsersByTeamId
{
    /// <summary>
    /// Validator for the GetUsersByTeamIdQuery
    /// </summary>
    public class GetUsersByTeamIdQueryValidator : AbstractValidator<GetUsersByTeamIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUsersByTeamIdQueryValidator"/> class
        /// </summary>
        public GetUsersByTeamIdQueryValidator()
        {
            RuleFor(x => x.TeamId)
                .NotEmpty().WithMessage("Team ID is required");
        }
    }
}