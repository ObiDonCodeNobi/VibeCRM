using FluentValidation;
using VibeCRM.Shared.DTOs.Call;

namespace VibeCRM.Application.Features.Call.Validators
{
    /// <summary>
    /// Validator for the CallListDto.
    /// Defines validation rules for call list data transfer objects.
    /// </summary>
    public class CallListDtoValidator : AbstractValidator<CallListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallListDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public CallListDtoValidator()
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

            RuleFor(c => c.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");
        }
    }
}