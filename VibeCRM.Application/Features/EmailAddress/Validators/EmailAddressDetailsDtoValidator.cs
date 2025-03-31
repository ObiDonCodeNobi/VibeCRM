using FluentValidation;
using VibeCRM.Shared.DTOs.EmailAddress;

namespace VibeCRM.Application.Features.EmailAddress.Validators
{
    /// <summary>
    /// Validator for the EmailAddressDetailsDto class.
    /// Extends the base EmailAddressDtoValidator with additional validation rules.
    /// </summary>
    public class EmailAddressDetailsDtoValidator : AbstractValidator<EmailAddressDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddressDetailsDtoValidator"/> class.
        /// Sets up validation rules for the EmailAddressDetailsDto properties.
        /// </summary>
        public EmailAddressDetailsDtoValidator()
        {
            Include(new EmailAddressDtoValidator());

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Creator ID is required.");

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Creation date is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modification date is required.");
        }
    }
}