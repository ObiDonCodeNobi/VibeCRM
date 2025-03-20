using FluentValidation;

namespace VibeCRM.Application.Features.User.Commands.CreateUser
{
    /// <summary>
    /// Validator for the CreateUserCommand
    /// </summary>
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandValidator"/> class
        /// </summary>
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.PersonId)
                .NotEmpty().WithMessage("Person ID is required");

            RuleFor(x => x.LoginName)
                .NotEmpty().WithMessage("Login name is required")
                .MaximumLength(100).WithMessage("Login name cannot exceed 100 characters");

            RuleFor(x => x.LoginPassword)
                .NotEmpty().WithMessage("Login password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
        }
    }
}