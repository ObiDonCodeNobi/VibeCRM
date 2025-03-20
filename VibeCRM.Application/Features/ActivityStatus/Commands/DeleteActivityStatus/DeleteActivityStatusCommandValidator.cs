using FluentValidation;

namespace VibeCRM.Application.Features.ActivityStatus.Commands.DeleteActivityStatus
{
    /// <summary>
    /// Validator for the DeleteActivityStatusCommand class.
    /// Defines validation rules for deleting an activity status.
    /// </summary>
    public class DeleteActivityStatusCommandValidator : AbstractValidator<DeleteActivityStatusCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteActivityStatusCommandValidator"/> class.
        /// Configures validation rules for DeleteActivityStatusCommand properties.
        /// </summary>
        public DeleteActivityStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Activity status ID is required.");
        }
    }
}