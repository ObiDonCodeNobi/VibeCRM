using FluentValidation;

namespace VibeCRM.Application.Features.ActivityType.Commands.CreateActivityType
{
    /// <summary>
    /// Validator for the CreateActivityTypeCommand class.
    /// Defines validation rules for creating an activity type.
    /// </summary>
    public class CreateActivityTypeCommandValidator : AbstractValidator<CreateActivityTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateActivityTypeCommandValidator"/> class.
        /// Configures validation rules for CreateActivityTypeCommand properties.
        /// </summary>
        public CreateActivityTypeCommandValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(100).WithMessage("Type cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}