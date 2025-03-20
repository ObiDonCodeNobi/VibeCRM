using FluentValidation;
using VibeCRM.Application.Features.ActivityType.DTOs;

namespace VibeCRM.Application.Features.ActivityType.Validators
{
    /// <summary>
    /// Validator for the ActivityTypeDetailsDto class.
    /// Defines validation rules for detailed activity type data.
    /// </summary>
    public class ActivityTypeDetailsDtoValidator : AbstractValidator<ActivityTypeDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTypeDetailsDtoValidator"/> class.
        /// Configures validation rules for ActivityTypeDetailsDto properties.
        /// </summary>
        public ActivityTypeDetailsDtoValidator()
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

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required.");
        }
    }
}