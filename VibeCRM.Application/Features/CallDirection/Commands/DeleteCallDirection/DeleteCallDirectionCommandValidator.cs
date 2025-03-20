using FluentValidation;

namespace VibeCRM.Application.Features.CallDirection.Commands.DeleteCallDirection
{
    /// <summary>
    /// Validator for the DeleteCallDirectionCommand.
    /// Defines validation rules for soft deleting an existing call direction.
    /// </summary>
    public class DeleteCallDirectionCommandValidator : AbstractValidator<DeleteCallDirectionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCallDirectionCommandValidator"/> class.
        /// Sets up validation rules for the DeleteCallDirectionCommand properties.
        /// </summary>
        public DeleteCallDirectionCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Call direction ID is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required.");
        }
    }
}