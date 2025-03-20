using FluentValidation;

namespace VibeCRM.Application.Features.Team.Commands.DeleteTeam
{
    /// <summary>
    /// Validator for the DeleteTeamCommand
    /// </summary>
    public class DeleteTeamCommandValidator : AbstractValidator<DeleteTeamCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTeamCommandValidator"/> class
        /// </summary>
        public DeleteTeamCommandValidator()
        {
            // Id is required
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Team ID is required");
        }
    }
}