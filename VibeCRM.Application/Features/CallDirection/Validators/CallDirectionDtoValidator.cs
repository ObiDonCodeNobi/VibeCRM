using FluentValidation;
using VibeCRM.Shared.DTOs.CallDirection;

namespace VibeCRM.Application.Features.CallDirection.Validators
{
    /// <summary>
    /// Validator for the CallDirectionDto.
    /// Defines validation rules for the basic call direction properties.
    /// </summary>
    public class CallDirectionDtoValidator : AbstractValidator<CallDirectionDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallDirectionDtoValidator"/> class.
        /// Sets up validation rules for the CallDirectionDto properties.
        /// </summary>
        public CallDirectionDtoValidator()
        {
            RuleFor(x => x.Direction)
                .NotEmpty().WithMessage("Direction is required.")
                .MaximumLength(100).WithMessage("Direction must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}