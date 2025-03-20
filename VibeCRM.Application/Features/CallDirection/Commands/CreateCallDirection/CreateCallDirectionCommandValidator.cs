using FluentValidation;

namespace VibeCRM.Application.Features.CallDirection.Commands.CreateCallDirection
{
    /// <summary>
    /// Validator for the CreateCallDirectionCommand.
    /// Defines validation rules for creating a new call direction.
    /// </summary>
    public class CreateCallDirectionCommandValidator : AbstractValidator<CreateCallDirectionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCallDirectionCommandValidator"/> class.
        /// Sets up validation rules for the CreateCallDirectionCommand properties.
        /// </summary>
        public CreateCallDirectionCommandValidator()
        {
            RuleFor(x => x.Direction)
                .NotEmpty().WithMessage("Direction is required.")
                .MaximumLength(100).WithMessage("Direction must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Creator ID is required.");
        }
    }
}