using FluentValidation;

namespace VibeCRM.Application.Features.ActivityDefinition.Commands.CreateActivityDefinition
{
    /// <summary>
    /// Validator for the CreateActivityDefinitionCommand class.
    /// Defines validation rules for creating activity definition entities.
    /// </summary>
    public class CreateActivityDefinitionCommandValidator : AbstractValidator<CreateActivityDefinitionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateActivityDefinitionCommandValidator"/> class.
        /// Sets up validation rules for creating activity definition entities.
        /// </summary>
        public CreateActivityDefinitionCommandValidator()
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

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}