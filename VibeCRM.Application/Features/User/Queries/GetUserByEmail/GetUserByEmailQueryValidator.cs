using FluentValidation;

namespace VibeCRM.Application.Features.User.Queries.GetUserByEmail
{
    /// <summary>
    /// Validator for the GetUserByEmailQuery
    /// </summary>
    public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByEmailQueryValidator"/> class
        /// </summary>
        public GetUserByEmailQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("A valid email address is required")
                .MaximumLength(255).WithMessage("Email address cannot exceed 255 characters");
        }
    }
}