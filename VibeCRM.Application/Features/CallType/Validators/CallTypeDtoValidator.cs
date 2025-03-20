using FluentValidation;
using VibeCRM.Application.Features.CallType.DTOs;

namespace VibeCRM.Application.Features.CallType.Validators
{
    /// <summary>
    /// Validator for the CallTypeDto.
    /// Defines validation rules for the basic call type properties.
    /// </summary>
    public class CallTypeDtoValidator : AbstractValidator<CallTypeDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallTypeDtoValidator"/> class.
        /// Sets up validation rules for the CallTypeDto properties.
        /// </summary>
        public CallTypeDtoValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(100).WithMessage("Type must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}