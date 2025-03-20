using FluentValidation;

namespace VibeCRM.Application.Features.ActivityType.Commands.DeleteActivityType
{
    /// <summary>
    /// Validator for the DeleteActivityTypeCommand class.
    /// Defines validation rules for deleting an activity type.
    /// </summary>
    public class DeleteActivityTypeCommandValidator : AbstractValidator<DeleteActivityTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteActivityTypeCommandValidator"/> class.
        /// Configures validation rules for DeleteActivityTypeCommand properties.
        /// </summary>
        public DeleteActivityTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Activity type ID is required.");
        }
    }
}