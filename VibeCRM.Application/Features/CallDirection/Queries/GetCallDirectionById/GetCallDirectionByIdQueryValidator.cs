using FluentValidation;

namespace VibeCRM.Application.Features.CallDirection.Queries.GetCallDirectionById
{
    /// <summary>
    /// Validator for the GetCallDirectionByIdQuery.
    /// Defines validation rules for retrieving a call direction by its ID.
    /// </summary>
    public class GetCallDirectionByIdQueryValidator : AbstractValidator<GetCallDirectionByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCallDirectionByIdQueryValidator"/> class.
        /// Sets up validation rules for the GetCallDirectionByIdQuery properties.
        /// </summary>
        public GetCallDirectionByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Call direction ID is required.");
        }
    }
}