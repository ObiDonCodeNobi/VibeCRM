using FluentValidation;

namespace VibeCRM.Application.Features.AccountType.Commands.DeleteAccountType
{
    /// <summary>
    /// Validator for the DeleteAccountTypeCommand class.
    /// Defines validation rules for deleting an account type.
    /// </summary>
    public class DeleteAccountTypeCommandValidator : AbstractValidator<DeleteAccountTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAccountTypeCommandValidator"/> class.
        /// Configures validation rules for DeleteAccountTypeCommand properties.
        /// </summary>
        public DeleteAccountTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Account type ID is required.");
        }
    }
}