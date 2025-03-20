using FluentValidation;
using VibeCRM.Application.Features.Call.DTOs;

namespace VibeCRM.Application.Features.Call.Validators
{
    /// <summary>
    /// Validator for the CallDto.
    /// Defines validation rules for call data transfer objects.
    /// </summary>
    public class CallDtoValidator : AbstractValidator<CallDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public CallDtoValidator()
        {
            RuleFor(c => c.TypeId)
                .NotEmpty().WithMessage("Call type is required.");

            RuleFor(c => c.StatusId)
                .NotEmpty().WithMessage("Call status is required.");

            RuleFor(c => c.DirectionId)
                .NotEmpty().WithMessage("Call direction is required.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(c => c.Duration)
                .GreaterThanOrEqualTo(0).WithMessage("Duration cannot be negative.");
        }
    }
}