using FluentValidation;

namespace VibeCRM.Application.Features.Address.Commands.DeleteAddress
{
    /// <summary>
    /// Validator for the DeleteAddressCommand to ensure the command contains valid parameters.
    /// </summary>
    public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
    {
        /// <summary>
        /// Initializes a new instance of the DeleteAddressCommandValidator class with validation rules.
        /// </summary>
        public DeleteAddressCommandValidator()
        {
            RuleFor(c => c.AddressId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}