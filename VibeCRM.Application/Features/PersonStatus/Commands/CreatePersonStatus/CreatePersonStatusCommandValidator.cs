using FluentValidation;

namespace VibeCRM.Application.Features.PersonStatus.Commands.CreatePersonStatus
{
    /// <summary>
    /// Validator for the CreatePersonStatusCommand.
    /// Defines validation rules for creating person status commands.
    /// </summary>
    public class CreatePersonStatusCommandValidator : AbstractValidator<CreatePersonStatusCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePersonStatusCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public CreatePersonStatusCommandValidator()
        {
            RuleFor(c => c.Status)
                .NotEmpty().WithMessage("Status name is required.")
                .MaximumLength(100).WithMessage("Status name must not exceed 100 characters.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(c => c.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");

            RuleFor(c => c.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}
