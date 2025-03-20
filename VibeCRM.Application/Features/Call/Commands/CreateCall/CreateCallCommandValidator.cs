using FluentValidation;

namespace VibeCRM.Application.Features.Call.Commands.CreateCall
{
    /// <summary>
    /// Validator for the CreateCallCommand.
    /// Defines validation rules for call creation commands.
    /// </summary>
    public class CreateCallCommandValidator : AbstractValidator<CreateCallCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCallCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public CreateCallCommandValidator()
        {
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

            RuleFor(c => c.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}