using FluentValidation;
using VibeCRM.Shared.DTOs.CallType;

namespace VibeCRM.Application.Features.CallType.Validators
{
    /// <summary>
    /// Validator for the CallTypeListDto.
    /// Defines validation rules for the call type list properties.
    /// </summary>
    public class CallTypeListDtoValidator : AbstractValidator<CallTypeListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallTypeListDtoValidator"/> class.
        /// Sets up validation rules for the CallTypeListDto properties.
        /// </summary>
        public CallTypeListDtoValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(100).WithMessage("Type must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");

            RuleFor(x => x.CallCount)
                .GreaterThanOrEqualTo(0).WithMessage("Call count must be a non-negative number.");
        }
    }
}