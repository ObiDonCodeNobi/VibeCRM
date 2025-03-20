using FluentValidation;

namespace VibeCRM.Application.Features.Person.Commands.UpdatePerson
{
    /// <summary>
    /// Validator for the UpdatePersonCommand class.
    /// Defines validation rules for person update commands.
    /// </summary>
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePersonCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public UpdatePersonCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Person ID is required.");

            RuleFor(p => p.Firstname)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

            RuleFor(p => p.MiddleInitial)
                .MaximumLength(10).WithMessage("Middle initial must not exceed 10 characters.");

            RuleFor(p => p.Lastname)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

            RuleFor(p => p.Title)
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(p => p.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required.");
        }
    }
}