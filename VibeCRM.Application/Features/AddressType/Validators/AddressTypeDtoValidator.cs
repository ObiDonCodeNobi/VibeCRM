using FluentValidation;
using VibeCRM.Application.Features.AddressType.DTOs;

namespace VibeCRM.Application.Features.AddressType.Validators
{
    /// <summary>
    /// Validator for the AddressTypeDto class.
    /// Defines validation rules for address type properties.
    /// </summary>
    public class AddressTypeDtoValidator : AbstractValidator<AddressTypeDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressTypeDtoValidator"/> class.
        /// Configures validation rules for AddressTypeDto properties.
        /// </summary>
        public AddressTypeDtoValidator()
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