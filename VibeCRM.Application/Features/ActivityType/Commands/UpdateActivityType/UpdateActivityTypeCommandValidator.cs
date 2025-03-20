using FluentValidation;

namespace VibeCRM.Application.Features.ActivityType.Commands.UpdateActivityType
{
    /// <summary>
    /// Validator for the UpdateActivityTypeCommand class.
    /// Defines validation rules for updating an activity type.
    /// </summary>
    public class UpdateActivityTypeCommandValidator : AbstractValidator<UpdateActivityTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateActivityTypeCommandValidator"/> class.
        /// Configures validation rules for UpdateActivityTypeCommand properties.
        /// </summary>
        public UpdateActivityTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Activity type ID is required.");

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