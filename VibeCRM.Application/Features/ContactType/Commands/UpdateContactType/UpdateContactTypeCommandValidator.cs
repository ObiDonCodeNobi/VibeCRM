using FluentValidation;

namespace VibeCRM.Application.Features.ContactType.Commands.UpdateContactType
{
    /// <summary>
    /// Validator for the UpdateContactTypeCommand class.
    /// Defines validation rules for updating an existing contact type.
    /// </summary>
    public class UpdateContactTypeCommandValidator : AbstractValidator<UpdateContactTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateContactTypeCommandValidator"/> class.
        /// Configures validation rules for UpdateContactTypeCommand properties.
        /// </summary>
        public UpdateContactTypeCommandValidator()
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