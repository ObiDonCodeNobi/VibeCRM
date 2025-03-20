using FluentValidation;

namespace VibeCRM.Application.Features.User.Queries.GetUserByUsername
{
    /// <summary>
    /// Validator for the GetUserByUsernameQuery
    /// </summary>
    public class GetUserByUsernameQueryValidator : AbstractValidator<GetUserByUsernameQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByUsernameQueryValidator"/> class
        /// </summary>
        public GetUserByUsernameQueryValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(100).WithMessage("Username cannot exceed 100 characters");
        }
    }
}