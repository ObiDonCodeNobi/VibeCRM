using FluentValidation;
using VibeCRM.Application.Features.Address.DTOs;

namespace VibeCRM.Application.Features.Address.Validators
{
    /// <summary>
    /// Validator for the AddressListDto class.
    /// Defines validation rules for address list data transfer objects.
    /// </summary>
    public class AddressListDtoValidator : AbstractValidator<AddressListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressListDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public AddressListDtoValidator()
        {
            // Include validation rules from the base AddressDto validator
            Include(new AddressDtoValidator());

            RuleFor(a => a.AddressTypeName)
                .NotEmpty().WithMessage("Address type name is required.")
                .MaximumLength(50).WithMessage("Address type name must not exceed 50 characters.");

            RuleFor(a => a.StateName)
                .NotEmpty().WithMessage("State name is required.")
                .MaximumLength(50).WithMessage("State name must not exceed 50 characters.");

            RuleFor(a => a.FullAddress)
                .NotEmpty().WithMessage("Full address is required.")
                .MaximumLength(250).WithMessage("Full address must not exceed 250 characters.");
        }
    }
}