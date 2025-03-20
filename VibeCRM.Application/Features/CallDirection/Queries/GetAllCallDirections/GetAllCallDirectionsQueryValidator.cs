using FluentValidation;

namespace VibeCRM.Application.Features.CallDirection.Queries.GetAllCallDirections
{
    /// <summary>
    /// Validator for the GetAllCallDirectionsQuery.
    /// Included for consistency with the validation pattern across the application.
    /// </summary>
    public class GetAllCallDirectionsQueryValidator : AbstractValidator<GetAllCallDirectionsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllCallDirectionsQueryValidator"/> class.
        /// No specific rules are defined as the query has no parameters.
        /// </summary>
        public GetAllCallDirectionsQueryValidator()
        {
            // No validation rules needed as this query has no parameters
        }
    }
}