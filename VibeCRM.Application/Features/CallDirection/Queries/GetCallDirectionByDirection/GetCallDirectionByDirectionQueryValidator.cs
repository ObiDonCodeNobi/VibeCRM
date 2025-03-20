using FluentValidation;

namespace VibeCRM.Application.Features.CallDirection.Queries.GetCallDirectionByDirection
{
    /// <summary>
    /// Validator for the GetCallDirectionByDirectionQuery.
    /// Defines validation rules for retrieving a call direction by its direction name.
    /// </summary>
    public class GetCallDirectionByDirectionQueryValidator : AbstractValidator<GetCallDirectionByDirectionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCallDirectionByDirectionQueryValidator"/> class.
        /// Sets up validation rules for the GetCallDirectionByDirectionQuery properties.
        /// </summary>
        public GetCallDirectionByDirectionQueryValidator()
        {
            RuleFor(x => x.Direction)
                .NotEmpty().WithMessage("Direction name is required.")
                .MaximumLength(100).WithMessage("Direction name must not exceed 100 characters.");
        }
    }
}