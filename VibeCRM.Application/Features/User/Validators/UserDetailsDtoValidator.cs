using FluentValidation;
using VibeCRM.Shared.DTOs.User;

namespace VibeCRM.Application.Features.User.Validators
{
    /// <summary>
    /// Validator for the UserDetailsDto class
    /// </summary>
    public class UserDetailsDtoValidator : AbstractValidator<UserDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDetailsDtoValidator"/> class
        /// </summary>
        public UserDetailsDtoValidator()
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

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by is required");

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Created date is required");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required");
        }
    }
}