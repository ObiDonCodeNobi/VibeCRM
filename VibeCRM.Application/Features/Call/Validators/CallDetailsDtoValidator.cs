using FluentValidation;
using VibeCRM.Application.Features.Call.DTOs;

namespace VibeCRM.Application.Features.Call.Validators
{
    /// <summary>
    /// Validator for the CallDetailsDto.
    /// Defines validation rules for detailed call data transfer objects.
    /// </summary>
    public class CallDetailsDtoValidator : AbstractValidator<CallDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallDetailsDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public CallDetailsDtoValidator()
        {
            Include(new CallDtoValidator());

            RuleFor(c => c.TypeName)
                .NotEmpty().WithMessage("Call type name is required.")
                .MaximumLength(100).WithMessage("Call type name must not exceed 100 characters.");

            RuleFor(c => c.StatusName)
                .NotEmpty().WithMessage("Call status name is required.")
                .MaximumLength(100).WithMessage("Call status name must not exceed 100 characters.");

            RuleFor(c => c.DirectionName)
                .NotEmpty().WithMessage("Call direction name is required.")
                .MaximumLength(100).WithMessage("Call direction name must not exceed 100 characters.");

            RuleFor(c => c.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required.");

            RuleFor(c => c.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");

            RuleFor(c => c.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.");
        }
    }
}