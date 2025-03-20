using FluentValidation;

namespace VibeCRM.Application.Features.AddressType.Commands.DeleteAddressType
{
    /// <summary>
    /// Validator for the DeleteAddressTypeCommand class.
    /// Defines validation rules for deleting an address type.
    /// </summary>
    public class DeleteAddressTypeCommandValidator : AbstractValidator<DeleteAddressTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAddressTypeCommandValidator"/> class.
        /// Configures validation rules for DeleteAddressTypeCommand properties.
        /// </summary>
        public DeleteAddressTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Address type ID is required.");
        }
    }
}