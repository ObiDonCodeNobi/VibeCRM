using FluentValidation;
using VibeCRM.Shared.DTOs.Person;

namespace VibeCRM.Application.Features.Person.Validators
{
    /// <summary>
    /// Validator for the PersonDetailsDto class.
    /// Defines validation rules for detailed person data transfer objects.
    /// </summary>
    public class PersonDetailsDtoValidator : AbstractValidator<PersonDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonDetailsDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public PersonDetailsDtoValidator()
        {
            // Include all rules from the base validator
            Include(new PersonDtoValidator());

            RuleFor(p => p.CreatedBy)
                .NotEmpty().WithMessage("Creator ID is required.");

            RuleFor(p => p.CreatedDate)
                .NotEmpty().WithMessage("Creation date is required.");

            RuleFor(p => p.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required.");

            RuleFor(p => p.ModifiedDate)
                .NotEmpty().WithMessage("Modification date is required.");
        }
    }
}