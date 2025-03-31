using FluentValidation;
using VibeCRM.Shared.DTOs.ActivityDefinition;

namespace VibeCRM.Application.Features.ActivityDefinition.Validators
{
    /// <summary>
    /// Validator for the ActivityDefinitionDetailsDto class.
    /// Defines validation rules for detailed activity definition data.
    /// </summary>
    public class ActivityDefinitionDetailsDtoValidator : AbstractValidator<ActivityDefinitionDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityDefinitionDetailsDtoValidator"/> class.
        /// Sets up validation rules for detailed activity definition properties.
        /// </summary>
        public ActivityDefinitionDetailsDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Activity definition ID is required.");

            RuleFor(x => x.ActivityTypeId)
                .NotEmpty().WithMessage("Activity type is required.");

            RuleFor(x => x.ActivityTypeName)
                .NotEmpty().WithMessage("Activity type name is required.")
                .MaximumLength(100).WithMessage("Activity type name cannot exceed 100 characters.");

            RuleFor(x => x.ActivityStatusId)
                .NotEmpty().WithMessage("Activity status is required.");

            RuleFor(x => x.ActivityStatusName)
                .NotEmpty().WithMessage("Activity status name is required.")
                .MaximumLength(100).WithMessage("Activity status name cannot exceed 100 characters.");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(200).WithMessage("Subject cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.");

            RuleFor(x => x.DueDateOffset)
                .GreaterThanOrEqualTo(0).WithMessage("Due date offset must be zero or positive.");

            RuleFor(x => x.AssignedUserId)
                .NotEmpty().WithMessage("Assigned user is required.");

            RuleFor(x => x.AssignedUserName)
                .NotEmpty().WithMessage("Assigned user name is required.")
                .MaximumLength(100).WithMessage("Assigned user name cannot exceed 100 characters.");

            RuleFor(x => x.AssignedTeamId)
                .NotEmpty().WithMessage("Assigned team is required.");

            RuleFor(x => x.AssignedTeamName)
                .NotEmpty().WithMessage("Assigned team name is required.")
                .MaximumLength(100).WithMessage("Assigned team name cannot exceed 100 characters.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by is required.");

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.");
        }
    }
}