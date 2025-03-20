using FluentValidation;

namespace VibeCRM.Application.Features.CallDirection.Queries.GetCallDirectionsByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetCallDirectionsByOrdinalPositionQuery.
    /// Included for consistency with the validation pattern across the application.
    /// </summary>
    public class GetCallDirectionsByOrdinalPositionQueryValidator : AbstractValidator<GetCallDirectionsByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCallDirectionsByOrdinalPositionQueryValidator"/> class.
        /// No specific rules are defined as the query has no parameters.
        /// </summary>
        public GetCallDirectionsByOrdinalPositionQueryValidator()
        {
            // No validation rules needed as this query has no parameters
        }
    }
}