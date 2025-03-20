using FluentValidation;

namespace VibeCRM.Application.Features.Call.Commands.UpdateCall
{
    /// <summary>
    /// Validator for the UpdateCallCommand.
    /// Defines validation rules for call update commands.
    /// </summary>
    public class UpdateCallCommandValidator : AbstractValidator<UpdateCallCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCallCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public UpdateCallCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Call ID is required.");

            RuleFor(c => c.TypeId)
                .NotEmpty().WithMessage("Call type is required.");

            RuleFor(c => c.StatusId)
                .NotEmpty().WithMessage("Call status is required.");

            RuleFor(c => c.DirectionId)
                .NotEmpty().WithMessage("Call direction is required.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(c => c.Duration)
                .GreaterThanOrEqualTo(0).WithMessage("Duration cannot be negative.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}