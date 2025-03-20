using FluentValidation;
using VibeCRM.Application.Features.Person.DTOs;

namespace VibeCRM.Application.Features.Person.Validators
{
    /// <summary>
    /// Validator for the PersonListDto class.
    /// Defines validation rules for person list data transfer objects.
    /// </summary>
    public class PersonListDtoValidator : AbstractValidator<PersonListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonListDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public PersonListDtoValidator()
        {
            // Include all rules from the base validator
            Include(new PersonDtoValidator());

            RuleFor(p => p.PrimaryCompanyName)
                .MaximumLength(200).WithMessage("Primary company name must not exceed 200 characters.")
                .When(p => !string.IsNullOrEmpty(p.PrimaryCompanyName));

            RuleFor(p => p.PrimaryEmail)
                .EmailAddress().WithMessage("Primary email must be a valid email address.")
                .MaximumLength(255).WithMessage("Primary email must not exceed 255 characters.")
                .When(p => !string.IsNullOrEmpty(p.PrimaryEmail));

            RuleFor(p => p.PrimaryPhone)
                .MaximumLength(50).WithMessage("Primary phone must not exceed 50 characters.")
                .When(p => !string.IsNullOrEmpty(p.PrimaryPhone));

            RuleFor(p => p.PrimaryAddress)
                .MaximumLength(500).WithMessage("Primary address must not exceed 500 characters.")
                .When(p => !string.IsNullOrEmpty(p.PrimaryAddress));
        }
    }
}