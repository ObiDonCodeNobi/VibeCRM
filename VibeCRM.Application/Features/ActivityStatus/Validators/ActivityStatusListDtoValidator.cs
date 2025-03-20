using FluentValidation;
using VibeCRM.Application.Features.ActivityStatus.DTOs;

namespace VibeCRM.Application.Features.ActivityStatus.Validators
{
    /// <summary>
    /// Validator for the ActivityStatusListDto class.
    /// Defines validation rules for activity status list data.
    /// </summary>
    public class ActivityStatusListDtoValidator : AbstractValidator<ActivityStatusListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityStatusListDtoValidator"/> class.
        /// Configures validation rules for ActivityStatusListDto properties.
        /// </summary>
        public ActivityStatusListDtoValidator()
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
        }
    }
}