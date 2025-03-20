using FluentValidation;

namespace VibeCRM.Application.Features.AccountType.Commands.CreateAccountType
{
    /// <summary>
    /// Validator for the CreateAccountTypeCommand class.
    /// Defines validation rules for creating a new account type.
    /// </summary>
    public class CreateAccountTypeCommandValidator : AbstractValidator<CreateAccountTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAccountTypeCommandValidator"/> class.
        /// Configures validation rules for CreateAccountTypeCommand properties.
        /// </summary>
        public CreateAccountTypeCommandValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(100).WithMessage("Type cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}