using FluentValidation;

namespace VibeCRM.Application.Features.AddressType.Commands.CreateAddressType
{
    /// <summary>
    /// Validator for the CreateAddressTypeCommand class.
    /// Defines validation rules for creating a new address type.
    /// </summary>
    public class CreateAddressTypeCommandValidator : AbstractValidator<CreateAddressTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAddressTypeCommandValidator"/> class.
        /// Configures validation rules for CreateAddressTypeCommand properties.
        /// </summary>
        public CreateAddressTypeCommandValidator()
        {
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