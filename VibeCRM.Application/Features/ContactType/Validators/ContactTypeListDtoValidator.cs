using FluentValidation;
using VibeCRM.Shared.DTOs.ContactType;

namespace VibeCRM.Application.Features.ContactType.Validators
{
    /// <summary>
    /// Validator for the ContactTypeListDto class.
    /// Defines validation rules for contact type list properties.
    /// </summary>
    public class ContactTypeListDtoValidator : AbstractValidator<ContactTypeListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactTypeListDtoValidator"/> class.
        /// Configures validation rules for ContactTypeListDto properties.
        /// </summary>
        public ContactTypeListDtoValidator()
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

            RuleFor(x => x.ContactCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Contact count must be a non-negative number.");
        }
    }
}