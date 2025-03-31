using FluentValidation;
using VibeCRM.Shared.DTOs.CallDirection;

namespace VibeCRM.Application.Features.CallDirection.Validators
{
    /// <summary>
    /// Validator for the CallDirectionDetailsDto.
    /// Defines validation rules for the detailed call direction properties including audit fields.
    /// </summary>
    public class CallDirectionDetailsDtoValidator : AbstractValidator<CallDirectionDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallDirectionDetailsDtoValidator"/> class.
        /// Sets up validation rules for the CallDirectionDetailsDto properties.
        /// </summary>
        public CallDirectionDetailsDtoValidator()
        {
            RuleFor(x => x.Direction)
                .NotEmpty().WithMessage("Direction is required.")
                .MaximumLength(100).WithMessage("Direction must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");

            RuleFor(x => x.ActivityCount)
                .GreaterThanOrEqualTo(0).WithMessage("Activity count must be a non-negative number.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Creator ID is required.");

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Creation date is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modification date is required.");
        }
    }
}