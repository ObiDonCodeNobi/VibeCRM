using FluentValidation;
using VibeCRM.Shared.DTOs.User;

namespace VibeCRM.Application.Features.User.Validators
{
    /// <summary>
    /// Validator for the UserDto class
    /// </summary>
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDtoValidator"/> class
        /// </summary>
        public UserDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("User ID is required");

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