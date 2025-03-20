using FluentValidation;
using VibeCRM.Application.Features.EmailAddressType.DTOs;

namespace VibeCRM.Application.Features.EmailAddressType.Validators
{
    /// <summary>
    /// Validator for the EmailAddressTypeDto class.
    /// Defines validation rules for email address type properties.
    /// </summary>
    public class EmailAddressTypeDtoValidator : AbstractValidator<EmailAddressTypeDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddressTypeDtoValidator"/> class.
        /// Configures validation rules for EmailAddressTypeDto properties.
        /// </summary>
        public EmailAddressTypeDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Email address type ID is required.");

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Email address type name is required.")
                .MaximumLength(100)
                .WithMessage("Email address type name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}