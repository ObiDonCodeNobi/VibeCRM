using FluentValidation;
using VibeCRM.Application.Features.Role.Queries.GetRoleByName;

namespace VibeCRM.Application.Features.Role.Validators
{
    /// <summary>
    /// Validator for the GetRoleByNameQuery
    /// </summary>
    public class GetRoleByNameQueryValidator : AbstractValidator<GetRoleByNameQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRoleByNameQueryValidator"/> class
        /// </summary>
        public GetRoleByNameQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");
        }
    }
}