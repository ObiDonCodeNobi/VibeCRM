using FluentValidation;

namespace VibeCRM.Application.Features.Activity.Commands.UpdateActivity
{
    /// <summary>
    /// Validator for the UpdateActivityCommand.
    /// Implements validation rules using FluentValidation to ensure data integrity
    /// when updating an existing activity.
    /// </summary>
    public class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateActivityCommandValidator"/> class.
        /// Sets up all validation rules for activity updates.
        /// </summary>
        public UpdateActivityCommandValidator()
        {
            // Activity ID validation
            RuleFor(x => x.ActivityId)
                .NotEmpty().WithMessage("Activity ID is required.")
                .NotEqual(Guid.Empty).WithMessage("Activity ID must be a valid ID.");

            // Activity Type validation
            RuleFor(x => x.ActivityTypeId)
                .NotEmpty().WithMessage("Activity type is required.")
                .NotEqual(Guid.Empty).WithMessage("Activity type must be a valid ID.");

            // Activity Status validation
            RuleFor(x => x.ActivityStatusId)
                .NotEmpty().WithMessage("Activity status is required.")
                .NotEqual(Guid.Empty).WithMessage("Activity status must be a valid ID.");

            // Subject validation
            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(200).WithMessage("Subject cannot exceed 200 characters.");

            // Description validation
            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.");

            // Due Date validation
            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .When(x => x.DueDate.HasValue && !x.IsCompleted)
                .WithMessage("Due date cannot be in the past for incomplete activities.");

            // Start Date validation
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.DueDate)
                .When(x => x.StartDate.HasValue && x.DueDate.HasValue)
                .WithMessage("Start date must be before or equal to due date.");

            // Completed Date validation
            RuleFor(x => x.CompletedDate)
                .NotEmpty()
                .When(x => x.IsCompleted)
                .WithMessage("Completed date is required when activity is marked as completed.");

            // Completed By validation
            RuleFor(x => x.CompletedBy)
                .NotEmpty()
                .When(x => x.IsCompleted)
                .WithMessage("Completed by is required when activity is marked as completed.");

            // Modified By validation
            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required.")
                .NotEqual(Guid.Empty).WithMessage("Modified by must be a valid user ID.");

            // Assigned User validation (if provided)
            RuleFor(x => x.AssignedUserId)
                .NotEqual(Guid.Empty)
                .When(x => x.AssignedUserId.HasValue)
                .WithMessage("Assigned user must be a valid user ID.");

            // Assigned Team validation (if provided)
            RuleFor(x => x.AssignedTeamId)
                .NotEqual(Guid.Empty)
                .When(x => x.AssignedTeamId.HasValue)
                .WithMessage("Assigned team must be a valid team ID.");
        }
    }
}