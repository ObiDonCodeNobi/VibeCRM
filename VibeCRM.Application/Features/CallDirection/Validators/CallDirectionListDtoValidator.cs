using FluentValidation;
using VibeCRM.Application.Features.CallDirection.DTOs;

namespace VibeCRM.Application.Features.CallDirection.Validators
{
    /// <summary>
    /// Validator for the CallDirectionListDto.
    /// Defines validation rules for the call direction list properties.
    /// </summary>
    public class CallDirectionListDtoValidator : AbstractValidator<CallDirectionListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallDirectionListDtoValidator"/> class.
        /// Sets up validation rules for the CallDirectionListDto properties.
        /// </summary>
        public CallDirectionListDtoValidator()
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
        }
    }
}