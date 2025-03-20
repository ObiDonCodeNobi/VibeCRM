using FluentValidation;

namespace VibeCRM.Application.Features.PersonType.Queries.GetPersonTypeByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetPersonTypeByOrdinalPositionQuery.
    /// Since this query doesn't have any parameters, the validator is minimal.
    /// </summary>
    public class GetPersonTypeByOrdinalPositionQueryValidator : AbstractValidator<GetPersonTypeByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetPersonTypeByOrdinalPositionQueryValidator class.
        /// </summary>
        public GetPersonTypeByOrdinalPositionQueryValidator()
        {
            // No validation rules needed as the query doesn't have any parameters
        }
    }
}