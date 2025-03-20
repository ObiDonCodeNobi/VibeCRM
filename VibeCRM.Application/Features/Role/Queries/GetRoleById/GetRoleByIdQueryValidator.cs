using FluentValidation;
using VibeCRM.Application.Features.Role.Queries.GetRoleById;

namespace VibeCRM.Application.Features.Role.Validators
{
    /// <summary>
    /// Validator for the GetRoleByIdQuery
    /// </summary>
    public class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRoleByIdQueryValidator"/> class
        /// </summary>
        public GetRoleByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID is required");
        }
    }
}