using FluentValidation;

namespace VibeCRM.Application.Features.Team.Queries.GetTeamById
{
    /// <summary>
    /// Validator for the GetTeamByIdQuery
    /// </summary>
    public class GetTeamByIdQueryValidator : AbstractValidator<GetTeamByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTeamByIdQueryValidator"/> class
        /// </summary>
        public GetTeamByIdQueryValidator()
        {
            // Id is required
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Team ID is required");
        }
    }
}