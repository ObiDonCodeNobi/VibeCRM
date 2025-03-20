using FluentValidation;
using VibeCRM.Application.Features.ActivityStatus.DTOs;

namespace VibeCRM.Application.Features.ActivityStatus.Validators
{
    /// <summary>
    /// Validator for the ActivityStatusDetailsDto class.
    /// Defines validation rules for detailed activity status data.
    /// </summary>
    public class ActivityStatusDetailsDtoValidator : AbstractValidator<ActivityStatusDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityStatusDetailsDtoValidator"/> class.
        /// Configures validation rules for ActivityStatusDetailsDto properties.
        /// </summary>
        public ActivityStatusDetailsDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(System.Guid.Empty).WithMessage("Activity status ID is required.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .MaximumLength(100).WithMessage("Status cannot exceed 100 characters.");

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