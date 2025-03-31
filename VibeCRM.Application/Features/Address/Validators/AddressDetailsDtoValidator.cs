using FluentValidation;
using VibeCRM.Shared.DTOs.Address;

namespace VibeCRM.Application.Features.Address.Validators
{
    /// <summary>
    /// Validator for the AddressDetailsDto class.
    /// Defines validation rules for detailed address data transfer objects.
    /// </summary>
    public class AddressDetailsDtoValidator : AbstractValidator<AddressDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressDetailsDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public AddressDetailsDtoValidator()
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

            RuleFor(a => a.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required.");

            RuleFor(a => a.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(a => a.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");

            RuleFor(a => a.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.");
        }
    }
}