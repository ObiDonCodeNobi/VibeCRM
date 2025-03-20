using FluentValidation;

namespace VibeCRM.Application.Features.Address.Commands.UpdateAddress
{
    /// <summary>
    /// Validator for the UpdateAddressCommand.
    /// Defines validation rules for address updates.
    /// </summary>
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAddressCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public UpdateAddressCommandValidator()
        {
            RuleFor(a => a.AddressId)
                .NotEmpty().WithMessage("Address ID is required.");

            RuleFor(a => a.AddressTypeId)
                .NotEmpty().WithMessage("Address type is required.");

            RuleFor(a => a.Line1)
                .NotEmpty().WithMessage("Address line 1 is required.")
                .MaximumLength(100).WithMessage("Address line 1 must not exceed 100 characters.");

            RuleFor(a => a.Line2)
                .MaximumLength(100).WithMessage("Address line 2 must not exceed 100 characters.");

            RuleFor(a => a.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City must not exceed 50 characters.");

            RuleFor(a => a.StateId)
                .NotEmpty().WithMessage("State is required.");

            RuleFor(a => a.Zip)
                .NotEmpty().WithMessage("Zip code is required.")
                .MaximumLength(20).WithMessage("Zip code must not exceed 20 characters.");

            RuleFor(a => a.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}