using FluentValidation;
using VibeCRM.Application.Features.User.DTOs;

namespace VibeCRM.Application.Features.User.Validators
{
    /// <summary>
    /// Validator for the UserListDto class
    /// </summary>
    public class UserListDtoValidator : AbstractValidator<UserListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserListDtoValidator"/> class
        /// </summary>
        public UserListDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("User ID is required");

            RuleFor(x => x.PersonId)
                .NotEmpty().WithMessage("Person ID is required");

            RuleFor(x => x.LoginName)
                .NotEmpty().WithMessage("Login name is required")
                .MaximumLength(100).WithMessage("Login name cannot exceed 100 characters");
        }
    }
}