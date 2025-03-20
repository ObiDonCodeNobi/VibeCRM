using FluentValidation;

namespace VibeCRM.Application.Features.ActivityStatus.Commands.CreateActivityStatus
{
    /// <summary>
    /// Validator for the CreateActivityStatusCommand class.
    /// Defines validation rules for creating an activity status.
    /// </summary>
    public class CreateActivityStatusCommandValidator : AbstractValidator<CreateActivityStatusCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateActivityStatusCommandValidator"/> class.
        /// Configures validation rules for CreateActivityStatusCommand properties.
        /// </summary>
        public CreateActivityStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .MaximumLength(100).WithMessage("Status cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}