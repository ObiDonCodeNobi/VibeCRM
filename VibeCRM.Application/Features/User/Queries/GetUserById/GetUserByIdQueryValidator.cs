using FluentValidation;

namespace VibeCRM.Application.Features.User.Queries.GetUserById
{
    /// <summary>
    /// Validator for the GetUserByIdQuery
    /// </summary>
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByIdQueryValidator"/> class
        /// </summary>
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("User ID is required");
        }
    }
}