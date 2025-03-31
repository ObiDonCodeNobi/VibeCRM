using FluentValidation;
using VibeCRM.Shared.DTOs.Person;

namespace VibeCRM.Application.Features.Person.Validators
{
    /// <summary>
    /// Validator for the PersonDto class.
    /// Defines validation rules for person data transfer objects.
    /// </summary>
    public class PersonDtoValidator : AbstractValidator<PersonDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public PersonDtoValidator()
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
        }
    }
}