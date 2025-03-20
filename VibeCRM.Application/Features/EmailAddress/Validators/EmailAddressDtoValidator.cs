using FluentValidation;
using VibeCRM.Application.Features.EmailAddress.DTOs;

namespace VibeCRM.Application.Features.EmailAddress.Validators
{
    /// <summary>
    /// Validator for the EmailAddressDto class.
    /// Defines validation rules for email address data.
    /// </summary>
    public class EmailAddressDtoValidator : AbstractValidator<EmailAddressDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddressDtoValidator"/> class.
        /// Sets up validation rules for the EmailAddressDto properties.
        /// </summary>
        public EmailAddressDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Email address ID is required.");

            RuleFor(x => x.EmailAddressTypeId)
                .NotEmpty().WithMessage("Email address type ID is required.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Email address is required.")
                .MaximumLength(255).WithMessage("Email address cannot exceed 255 characters.")
                .EmailAddress().WithMessage("A valid email address is required.");
        }
    }
}