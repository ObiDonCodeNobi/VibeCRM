using FluentValidation;

namespace VibeCRM.Application.Features.User.Commands.DeleteUser
{
    /// <summary>
    /// Validator for the DeleteUserCommand
    /// </summary>
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserCommandValidator"/> class
        /// </summary>
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("User ID is required");
        }
    }
}