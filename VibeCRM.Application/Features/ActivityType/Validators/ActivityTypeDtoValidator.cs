using FluentValidation;
using VibeCRM.Shared.DTOs.ActivityType;

namespace VibeCRM.Application.Features.ActivityType.Validators
{
    /// <summary>
    /// Validator for the ActivityTypeDto class.
    /// Defines validation rules for activity type data.
    /// </summary>
    public class ActivityTypeDtoValidator : AbstractValidator<ActivityTypeDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTypeDtoValidator"/> class.
        /// Configures validation rules for ActivityTypeDto properties.
        /// </summary>
        public ActivityTypeDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(System.Guid.Empty).WithMessage("Activity type ID is required.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(100).WithMessage("Type cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}