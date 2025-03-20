using FluentValidation;

namespace VibeCRM.Application.Features.Person.Commands.CreatePerson
{
    /// <summary>
    /// Validator for the CreatePersonCommand class.
    /// Defines validation rules for person creation commands.
    /// </summary>
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePersonCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public CreatePersonCommandValidator()
        {
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

            RuleFor(p => p.CreatedBy)
                .NotEmpty().WithMessage("Creator ID is required.");

            RuleFor(p => p.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required.");
        }
    }
}