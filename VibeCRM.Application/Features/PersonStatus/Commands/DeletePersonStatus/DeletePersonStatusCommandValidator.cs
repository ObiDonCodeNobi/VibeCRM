using FluentValidation;

namespace VibeCRM.Application.Features.PersonStatus.Commands.DeletePersonStatus
{
    /// <summary>
    /// Validator for the DeletePersonStatusCommand.
    /// Defines validation rules for deleting person status commands.
    /// </summary>
    public class DeletePersonStatusCommandValidator : AbstractValidator<DeletePersonStatusCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePersonStatusCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public DeletePersonStatusCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Person status ID is required.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}
