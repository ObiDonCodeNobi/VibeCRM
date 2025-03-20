using FluentValidation;

namespace VibeCRM.Application.Features.ActivityDefinition.Commands.UpdateActivityDefinition
{
    /// <summary>
    /// Validator for the UpdateActivityDefinitionCommand class.
    /// Defines validation rules for updating activity definition entities.
    /// </summary>
    public class UpdateActivityDefinitionCommandValidator : AbstractValidator<UpdateActivityDefinitionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateActivityDefinitionCommandValidator"/> class.
        /// Sets up validation rules for updating activity definition entities.
        /// </summary>
        public UpdateActivityDefinitionCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Activity definition ID is required.");

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

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}