using FluentValidation;

namespace VibeCRM.Application.Features.EmailAddress.Commands.UpdateEmailAddress
{
    /// <summary>
    /// Validator for the UpdateEmailAddressCommand class.
    /// Defines validation rules for updating an existing email address.
    /// </summary>
    public class UpdateEmailAddressCommandValidator : AbstractValidator<UpdateEmailAddressCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEmailAddressCommandValidator"/> class.
        /// Sets up validation rules for the UpdateEmailAddressCommand properties.
        /// </summary>
        public UpdateEmailAddressCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Email address ID is required.");

            RuleFor(x => x.EmailAddressTypeId)
                .NotEmpty().WithMessage("Email address type ID is required.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Email address is required.")
                .MaximumLength(255).WithMessage("Email address cannot exceed 255 characters.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modification date is required.");
        }
    }
}