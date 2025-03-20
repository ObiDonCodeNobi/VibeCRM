using FluentValidation;

namespace VibeCRM.Application.Features.PersonStatus.Queries.GetPersonStatusByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetPersonStatusByOrdinalPositionQuery.
    /// Defines validation rules for retrieving person statuses by ordinal position queries.
    /// </summary>
    public class GetPersonStatusByOrdinalPositionQueryValidator : AbstractValidator<GetPersonStatusByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonStatusByOrdinalPositionQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetPersonStatusByOrdinalPositionQueryValidator()
        {
            // No specific validation rules needed for this query
            // The IncludeInactive property is a boolean with a default value
        }
    }
}
