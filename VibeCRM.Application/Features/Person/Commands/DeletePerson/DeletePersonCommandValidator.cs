using FluentValidation;

namespace VibeCRM.Application.Features.Person.Commands.DeletePerson
{
    /// <summary>
    /// Validator for the DeletePersonCommand class.
    /// Defines validation rules for person deletion commands.
    /// </summary>
    public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePersonCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public DeletePersonCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Person ID is required for deletion.");

            RuleFor(p => p.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required for tracking who performed the deletion.");
        }
    }
}