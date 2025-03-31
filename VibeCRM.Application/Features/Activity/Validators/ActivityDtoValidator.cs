using FluentValidation;
using VibeCRM.Shared.DTOs.Activity;

namespace VibeCRM.Application.Features.Activity.Validators
{
    /// <summary>
    /// Validator for the ActivityDto.
    /// Provides validation rules for the base activity data transfer object.
    /// </summary>
    public class ActivityDtoValidator : AbstractValidator<ActivityDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityDtoValidator"/> class
        /// and defines the validation rules.
        /// </summary>
        public ActivityDtoValidator()
        {
            RuleFor(x => x.ActivityId)
                .NotEqual(Guid.Empty).WithMessage("Activity ID is required");

            RuleFor(x => x.ActivityTypeId)
                .NotEqual(Guid.Empty).WithMessage("Activity type ID is required");

            RuleFor(x => x.ActivityStatusId)
                .NotEqual(Guid.Empty).WithMessage("Activity status ID is required");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(100).WithMessage("Subject cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.StartDate.HasValue && x.DueDate.HasValue)
                .WithMessage("Due date must be after or equal to start date.");

            RuleFor(x => x.CompletedDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .When(x => x.CompletedDate.HasValue)
                .WithMessage("Completed date cannot be in the future.");

            RuleFor(x => x.CompletedBy)
                .NotEmpty()
                .When(x => x.IsCompleted)
                .WithMessage("Completed by is required when activity is marked as completed.");

            RuleFor(x => x.CompletedDate)
                .NotEmpty()
                .When(x => x.IsCompleted)
                .WithMessage("Completed date is required when activity is marked as completed.");
        }
    }
}