using FluentValidation;
using VibeCRM.Application.Features.ContactType.DTOs;

namespace VibeCRM.Application.Features.ContactType.Validators
{
    /// <summary>
    /// Validator for the ContactTypeDto class.
    /// Defines validation rules for contact type properties.
    /// </summary>
    public class ContactTypeDtoValidator : AbstractValidator<ContactTypeDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactTypeDtoValidator"/> class.
        /// Configures validation rules for ContactTypeDto properties.
        /// </summary>
        public ContactTypeDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Contact type ID is required.");

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Contact type name is required.")
                .MaximumLength(50)
                .WithMessage("Contact type name cannot exceed 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}