using FluentValidation;

namespace VibeCRM.Application.Features.ContactType.Commands.CreateContactType
{
    /// <summary>
    /// Validator for the CreateContactTypeCommand class.
    /// Defines validation rules for creating a new contact type.
    /// </summary>
    public class CreateContactTypeCommandValidator : AbstractValidator<CreateContactTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateContactTypeCommandValidator"/> class.
        /// Configures validation rules for CreateContactTypeCommand properties.
        /// </summary>
        public CreateContactTypeCommandValidator()
        {
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