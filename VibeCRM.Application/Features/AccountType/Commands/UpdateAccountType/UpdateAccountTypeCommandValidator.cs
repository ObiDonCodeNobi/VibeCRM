using FluentValidation;

namespace VibeCRM.Application.Features.AccountType.Commands.UpdateAccountType
{
    /// <summary>
    /// Validator for the UpdateAccountTypeCommand class.
    /// Defines validation rules for updating an account type.
    /// </summary>
    public class UpdateAccountTypeCommandValidator : AbstractValidator<UpdateAccountTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAccountTypeCommandValidator"/> class.
        /// Configures validation rules for UpdateAccountTypeCommand properties.
        /// </summary>
        public UpdateAccountTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Account type ID is required.");

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