using FluentValidation;
using VibeCRM.Application.Features.EmailAddress.DTOs;

namespace VibeCRM.Application.Features.EmailAddress.Validators
{
    /// <summary>
    /// Validator for the EmailAddressListDto class.
    /// Extends the base EmailAddressDtoValidator with additional validation rules.
    /// </summary>
    public class EmailAddressListDtoValidator : AbstractValidator<EmailAddressListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddressListDtoValidator"/> class.
        /// Sets up validation rules for the EmailAddressListDto properties.
        /// </summary>
        public EmailAddressListDtoValidator()
        {
            Include(new EmailAddressDtoValidator());

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Creation date is required.");
        }
    }
}