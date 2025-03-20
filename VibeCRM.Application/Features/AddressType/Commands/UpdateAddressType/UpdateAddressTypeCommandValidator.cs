using FluentValidation;

namespace VibeCRM.Application.Features.AddressType.Commands.UpdateAddressType
{
    /// <summary>
    /// Validator for the UpdateAddressTypeCommand class.
    /// Defines validation rules for updating an existing address type.
    /// </summary>
    public class UpdateAddressTypeCommandValidator : AbstractValidator<UpdateAddressTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAddressTypeCommandValidator"/> class.
        /// Configures validation rules for UpdateAddressTypeCommand properties.
        /// </summary>
        public UpdateAddressTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Address type ID is required.");

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Address type name is required.")
                .MaximumLength(50)
                .WithMessage("Address type name cannot exceed 50 characters.");

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