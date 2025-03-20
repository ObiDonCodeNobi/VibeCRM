using FluentValidation;

namespace VibeCRM.Application.Features.Activity.Commands.CreateActivity
{
    /// <summary>
    /// Validator for the CreateActivityCommand.
    /// Implements validation rules using FluentValidation to ensure data integrity
    /// when creating a new activity.
    /// </summary>
    public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateActivityCommandValidator"/> class.
        /// Sets up all validation rules for activity creation.
        /// </summary>
        public CreateActivityCommandValidator()
        {
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
                .When(x => x.DueDate.HasValue)
                .WithMessage("Due date cannot be in the past.");

            // Start Date validation
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.DueDate)
                .When(x => x.StartDate.HasValue && x.DueDate.HasValue)
                .WithMessage("Start date must be before or equal to due date.");

            // Created By validation
            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by is required.")
                .NotEqual(Guid.Empty).WithMessage("Created by must be a valid user ID.");

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