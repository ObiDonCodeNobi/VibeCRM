using FluentValidation;
using VibeCRM.Application.Features.ActivityDefinition.DTOs;

namespace VibeCRM.Application.Features.ActivityDefinition.Validators
{
    /// <summary>
    /// Validator for the ActivityDefinitionDto class.
    /// Defines validation rules for activity definition data.
    /// </summary>
    public class ActivityDefinitionDtoValidator : AbstractValidator<ActivityDefinitionDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityDefinitionDtoValidator"/> class.
        /// Sets up validation rules for activity definition properties.
        /// </summary>
        public ActivityDefinitionDtoValidator()
        {
            RuleFor(x => x.ActivityTypeId)
                .NotEmpty().WithMessage("Activity type is required.");

            RuleFor(x => x.ActivityStatusId)
                .NotEmpty().WithMessage("Activity status is required.");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(200).WithMessage("Subject cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.");

            RuleFor(x => x.DueDateOffset)
                .GreaterThanOrEqualTo(0).WithMessage("Due date offset must be zero or positive.");

            RuleFor(x => x.AssignedUserId)
                .NotEmpty().WithMessage("Assigned user is required.");

            RuleFor(x => x.AssignedTeamId)
                .NotEmpty().WithMessage("Assigned team is required.");
        }
    }
}