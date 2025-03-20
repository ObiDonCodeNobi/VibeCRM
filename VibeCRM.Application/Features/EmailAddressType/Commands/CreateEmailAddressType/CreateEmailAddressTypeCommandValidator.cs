using FluentValidation;

namespace VibeCRM.Application.Features.EmailAddressType.Commands.CreateEmailAddressType
{
    /// <summary>
    /// Validator for the CreateEmailAddressTypeCommand.
    /// </summary>
    public class CreateEmailAddressTypeCommandValidator : AbstractValidator<CreateEmailAddressTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEmailAddressTypeCommandValidator"/> class.
        /// </summary>
        public CreateEmailAddressTypeCommandValidator()
        {
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