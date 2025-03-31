using FluentValidation;
using VibeCRM.Shared.DTOs.ActivityDefinition;

namespace VibeCRM.Application.Features.ActivityDefinition.Validators
{
    /// <summary>
    /// Validator for the ActivityDefinitionListDto class.
    /// Defines validation rules for activity definition list data.
    /// </summary>
    public class ActivityDefinitionListDtoValidator : AbstractValidator<ActivityDefinitionListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityDefinitionListDtoValidator"/> class.
        /// Sets up validation rules for activity definition list properties.
        /// </summary>
        public ActivityDefinitionListDtoValidator()
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

            RuleFor(x => x.DueDateOffset)
                .GreaterThanOrEqualTo(0).WithMessage("Due date offset must be zero or positive.");

            RuleFor(x => x.AssignedUserName)
                .NotEmpty().WithMessage("Assigned user name is required.")
                .MaximumLength(100).WithMessage("Assigned user name cannot exceed 100 characters.");

            RuleFor(x => x.AssignedTeamName)
                .NotEmpty().WithMessage("Assigned team name is required.")
                .MaximumLength(100).WithMessage("Assigned team name cannot exceed 100 characters.");
        }
    }
}