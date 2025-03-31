using FluentValidation;
using VibeCRM.Shared.DTOs.CallType;

namespace VibeCRM.Application.Features.CallType.Validators
{
    /// <summary>
    /// Validator for the CallTypeDetailsDto.
    /// Defines validation rules for the detailed call type properties.
    /// </summary>
    public class CallTypeDetailsDtoValidator : AbstractValidator<CallTypeDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallTypeDetailsDtoValidator"/> class.
        /// Sets up validation rules for the CallTypeDetailsDto properties.
        /// </summary>
        public CallTypeDetailsDtoValidator()
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

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.")
                .GreaterThanOrEqualTo(x => x.CreatedDate).WithMessage("Modified date must be greater than or equal to created date.");
        }
    }
}