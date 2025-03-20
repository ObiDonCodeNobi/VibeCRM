using FluentValidation;

namespace VibeCRM.Application.Features.EmailAddress.Commands.CreateEmailAddress
{
    /// <summary>
    /// Validator for the CreateEmailAddressCommand class.
    /// Defines validation rules for creating a new email address.
    /// </summary>
    public class CreateEmailAddressCommandValidator : AbstractValidator<CreateEmailAddressCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEmailAddressCommandValidator"/> class.
        /// Sets up validation rules for the CreateEmailAddressCommand properties.
        /// </summary>
        public CreateEmailAddressCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Email address ID is required.");

            RuleFor(x => x.EmailAddressTypeId)
                .NotEmpty().WithMessage("Email address type ID is required.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Email address is required.")
                .MaximumLength(255).WithMessage("Email address cannot exceed 255 characters.")
                .EmailAddress().WithMessage("A valid email address is required.");

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