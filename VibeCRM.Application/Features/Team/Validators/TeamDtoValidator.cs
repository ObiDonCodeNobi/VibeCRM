using FluentValidation;
using VibeCRM.Application.Features.Team.DTOs;

namespace VibeCRM.Application.Features.Team.Validators
{
    /// <summary>
    /// Validator for the TeamDto
    /// </summary>
    public class TeamDtoValidator : AbstractValidator<TeamDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamDtoValidator"/> class
        /// </summary>
        public TeamDtoValidator()
        {
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