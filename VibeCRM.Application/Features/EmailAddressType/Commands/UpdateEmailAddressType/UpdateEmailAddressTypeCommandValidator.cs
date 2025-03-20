using FluentValidation;

namespace VibeCRM.Application.Features.EmailAddressType.Commands.UpdateEmailAddressType
{
    /// <summary>
    /// Validator for the UpdateEmailAddressTypeCommand.
    /// </summary>
    public class UpdateEmailAddressTypeCommandValidator : AbstractValidator<UpdateEmailAddressTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEmailAddressTypeCommandValidator"/> class.
        /// </summary>
        public UpdateEmailAddressTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Email address type ID is required.")
                .NotEqual(Guid.Empty)
                .WithMessage("Email address type ID cannot be empty.");

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