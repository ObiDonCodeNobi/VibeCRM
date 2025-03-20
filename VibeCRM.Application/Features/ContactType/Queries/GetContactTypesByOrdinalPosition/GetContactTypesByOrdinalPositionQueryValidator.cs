using FluentValidation;

namespace VibeCRM.Application.Features.ContactType.Queries.GetContactTypesByOrdinalPosition
{
    /// <summary>
    /// Validator for the <see cref="GetContactTypesByOrdinalPositionQuery"/> class.
    /// </summary>
    public class GetContactTypesByOrdinalPositionQueryValidator : AbstractValidator<GetContactTypesByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetContactTypesByOrdinalPositionQueryValidator"/> class.
        /// </summary>
        public GetContactTypesByOrdinalPositionQueryValidator()
        {
            // No validation rules needed as this query has no parameters
        }
    }
}