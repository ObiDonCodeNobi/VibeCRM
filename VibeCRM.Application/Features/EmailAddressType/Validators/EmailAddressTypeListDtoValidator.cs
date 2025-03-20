using FluentValidation;
using VibeCRM.Application.Features.EmailAddressType.DTOs;

namespace VibeCRM.Application.Features.EmailAddressType.Validators
{
    /// <summary>
    /// Validator for the EmailAddressTypeListDto class.
    /// Defines validation rules for email address type list properties.
    /// </summary>
    public class EmailAddressTypeListDtoValidator : AbstractValidator<EmailAddressTypeListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddressTypeListDtoValidator"/> class.
        /// Configures validation rules for EmailAddressTypeListDto properties.
        /// </summary>
        public EmailAddressTypeListDtoValidator()
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

            RuleFor(x => x.EmailAddressCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Email address count must be a non-negative number.");
        }
    }
}