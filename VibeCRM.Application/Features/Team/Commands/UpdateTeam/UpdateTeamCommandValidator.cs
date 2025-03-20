using FluentValidation;

namespace VibeCRM.Application.Features.Team.Commands.UpdateTeam
{
    /// <summary>
    /// Validator for the UpdateTeamCommand
    /// </summary>
    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTeamCommandValidator"/> class
        /// </summary>
        public UpdateTeamCommandValidator()
        {
            // Id is required
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Team ID is required");

            // TeamLeadEmployeeId is required
            RuleFor(x => x.TeamLeadEmployeeId)
                .NotEmpty()
                .WithMessage("Team Lead Employee ID is required");

            // Name is required and has a maximum length
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Team Name is required")
                .MaximumLength(100)
                .WithMessage("Team Name cannot exceed 100 characters");

            // Description has a maximum length
            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Team Description cannot exceed 500 characters");
        }
    }
}