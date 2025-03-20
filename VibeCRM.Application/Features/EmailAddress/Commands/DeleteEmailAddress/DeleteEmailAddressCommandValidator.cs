using FluentValidation;

namespace VibeCRM.Application.Features.EmailAddress.Commands.DeleteEmailAddress
{
    /// <summary>
    /// Validator for the DeleteEmailAddressCommand class.
    /// Defines validation rules for deleting an existing email address.
    /// </summary>
    public class DeleteEmailAddressCommandValidator : AbstractValidator<DeleteEmailAddressCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEmailAddressCommandValidator"/> class.
        /// Sets up validation rules for the DeleteEmailAddressCommand properties.
        /// </summary>
        public DeleteEmailAddressCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Email address ID is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modification date is required.");
        }
    }
}