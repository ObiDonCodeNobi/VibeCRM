using FluentValidation;

namespace VibeCRM.Application.Features.Activity.Commands.DeleteActivity
{
    /// <summary>
    /// Validator for the DeleteActivityCommand.
    /// Implements validation rules using FluentValidation to ensure data integrity
    /// when soft deleting an activity by setting its Active property to false.
    /// </summary>
    public class DeleteActivityCommandValidator : AbstractValidator<DeleteActivityCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteActivityCommandValidator"/> class.
        /// Sets up all validation rules for activity soft deletion.
        /// </summary>
        public DeleteActivityCommandValidator()
        {
            // Activity ID validation
            RuleFor(x => x.ActivityId)
                .NotEmpty().WithMessage("Activity ID is required.")
                .NotEqual(Guid.Empty).WithMessage("Activity ID must be a valid ID.");

            // Modified By validation
            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required.")
                .NotEqual(Guid.Empty).WithMessage("Modified by must be a valid user ID.");
        }
    }
}