using FluentValidation;
using VibeCRM.Application.Features.ActivityType.DTOs;

namespace VibeCRM.Application.Features.ActivityType.Validators
{
    /// <summary>
    /// Validator for the ActivityTypeListDto class.
    /// Defines validation rules for activity type list data.
    /// </summary>
    public class ActivityTypeListDtoValidator : AbstractValidator<ActivityTypeListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTypeListDtoValidator"/> class.
        /// Configures validation rules for ActivityTypeListDto properties.
        /// </summary>
        public ActivityTypeListDtoValidator()
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

            RuleFor(x => x.ActivityCount)
                .GreaterThanOrEqualTo(0).WithMessage("Activity count must be a non-negative number.");
        }
    }
}