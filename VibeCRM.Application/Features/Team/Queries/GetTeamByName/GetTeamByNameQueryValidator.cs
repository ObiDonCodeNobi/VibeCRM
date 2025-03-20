using FluentValidation;

namespace VibeCRM.Application.Features.Team.Queries.GetTeamByName
{
    /// <summary>
    /// Validator for the GetTeamByNameQuery
    /// </summary>
    public class GetTeamByNameQueryValidator : AbstractValidator<GetTeamByNameQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTeamByNameQueryValidator"/> class
        /// </summary>
        public GetTeamByNameQueryValidator()
        {
            // Name is required and has a maximum length
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Team Name is required")
                .MaximumLength(100)
                .WithMessage("Team Name cannot exceed 100 characters");
        }
    }
}