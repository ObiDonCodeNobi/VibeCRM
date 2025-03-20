using FluentValidation;

namespace VibeCRM.Application.Features.CallType.Commands.CreateCallType
{
    /// <summary>
    /// Validator for the CreateCallTypeCommand.
    /// Defines validation rules for creating a new call type.
    /// </summary>
    public class CreateCallTypeCommandValidator : AbstractValidator<CreateCallTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCallTypeCommandValidator"/> class.
        /// Sets up validation rules for the CreateCallTypeCommand properties.
        /// </summary>
        public CreateCallTypeCommandValidator()
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