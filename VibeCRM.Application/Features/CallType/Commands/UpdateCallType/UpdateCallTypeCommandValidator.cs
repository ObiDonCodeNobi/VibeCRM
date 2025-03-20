using FluentValidation;

namespace VibeCRM.Application.Features.CallType.Commands.UpdateCallType
{
    /// <summary>
    /// Validator for the UpdateCallTypeCommand.
    /// Defines validation rules for updating an existing call type.
    /// </summary>
    public class UpdateCallTypeCommandValidator : AbstractValidator<UpdateCallTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCallTypeCommandValidator"/> class.
        /// Sets up validation rules for the UpdateCallTypeCommand properties.
        /// </summary>
        public UpdateCallTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

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