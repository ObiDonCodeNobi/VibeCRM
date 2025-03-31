using FluentValidation;
using VibeCRM.Shared.DTOs.EmailAddressType;

namespace VibeCRM.Application.Features.EmailAddressType.Validators
{
    /// <summary>
    /// Validator for the EmailAddressTypeDetailsDto class.
    /// Defines validation rules for detailed email address type properties.
    /// </summary>
    public class EmailAddressTypeDetailsDtoValidator : AbstractValidator<EmailAddressTypeDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddressTypeDetailsDtoValidator"/> class.
        /// Configures validation rules for EmailAddressTypeDetailsDto properties.
        /// </summary>
        public EmailAddressTypeDetailsDtoValidator()
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

            RuleFor(x => x.CreatedDate)
                .NotEmpty()
                .WithMessage("Created date is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty()
                .WithMessage("Modified date is required.");
        }
    }
}