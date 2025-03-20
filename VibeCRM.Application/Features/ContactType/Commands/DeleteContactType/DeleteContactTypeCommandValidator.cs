using FluentValidation;

namespace VibeCRM.Application.Features.ContactType.Commands.DeleteContactType
{
    /// <summary>
    /// Validator for the DeleteContactTypeCommand class.
    /// Defines validation rules for deleting a contact type.
    /// </summary>
    public class DeleteContactTypeCommandValidator : AbstractValidator<DeleteContactTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteContactTypeCommandValidator"/> class.
        /// Configures validation rules for DeleteContactTypeCommand properties.
        /// </summary>
        public DeleteContactTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Contact type ID is required.");
        }
    }
}