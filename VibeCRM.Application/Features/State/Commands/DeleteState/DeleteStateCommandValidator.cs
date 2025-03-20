using FluentValidation;

namespace VibeCRM.Application.Features.State.Commands.DeleteState
{
    /// <summary>
    /// Validator for the DeleteStateCommand class.
    /// Defines validation rules for deleting an existing state.
    /// </summary>
    public class DeleteStateCommandValidator : AbstractValidator<DeleteStateCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteStateCommandValidator"/> class.
        /// Configures validation rules for the DeleteStateCommand properties.
        /// </summary>
        public DeleteStateCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("State ID is required.");
        }
    }
}