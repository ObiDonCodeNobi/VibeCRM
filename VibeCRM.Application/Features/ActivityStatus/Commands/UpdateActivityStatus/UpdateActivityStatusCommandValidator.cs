using FluentValidation;

namespace VibeCRM.Application.Features.ActivityStatus.Commands.UpdateActivityStatus
{
    /// <summary>
    /// Validator for the UpdateActivityStatusCommand class.
    /// Defines validation rules for updating an activity status.
    /// </summary>
    public class UpdateActivityStatusCommandValidator : AbstractValidator<UpdateActivityStatusCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateActivityStatusCommandValidator"/> class.
        /// Configures validation rules for UpdateActivityStatusCommand properties.
        /// </summary>
        public UpdateActivityStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Activity status ID is required.");

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