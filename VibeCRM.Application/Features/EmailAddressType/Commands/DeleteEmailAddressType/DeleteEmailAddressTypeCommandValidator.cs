using FluentValidation;

namespace VibeCRM.Application.Features.EmailAddressType.Commands.DeleteEmailAddressType
{
    /// <summary>
    /// Validator for the DeleteEmailAddressTypeCommand.
    /// </summary>
    public class DeleteEmailAddressTypeCommandValidator : AbstractValidator<DeleteEmailAddressTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEmailAddressTypeCommandValidator"/> class.
        /// </summary>
        public DeleteEmailAddressTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Email address type ID is required.")
                .NotEqual(Guid.Empty)
                .WithMessage("Email address type ID cannot be empty.");
        }
    }
}