using FluentValidation;

namespace VibeCRM.Application.Features.CallDirection.Commands.UpdateCallDirection
{
    /// <summary>
    /// Validator for the UpdateCallDirectionCommand.
    /// Defines validation rules for updating an existing call direction.
    /// </summary>
    public class UpdateCallDirectionCommandValidator : AbstractValidator<UpdateCallDirectionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCallDirectionCommandValidator"/> class.
        /// Sets up validation rules for the UpdateCallDirectionCommand properties.
        /// </summary>
        public UpdateCallDirectionCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Call direction ID is required.");

            RuleFor(x => x.Direction)
                .NotEmpty().WithMessage("Direction is required.")
                .MaximumLength(100).WithMessage("Direction must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required.");
        }
    }
}