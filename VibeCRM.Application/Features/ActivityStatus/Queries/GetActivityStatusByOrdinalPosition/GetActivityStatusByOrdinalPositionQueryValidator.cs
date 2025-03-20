using FluentValidation;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetActivityStatusByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetActivityStatusByOrdinalPositionQuery class.
    /// This is a parameter-less query, so the validator has no specific rules.
    /// </summary>
    public class GetActivityStatusByOrdinalPositionQueryValidator : AbstractValidator<GetActivityStatusByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityStatusByOrdinalPositionQueryValidator"/> class.
        /// Since this is a parameter-less query, there are no specific validation rules.
        /// </summary>
        public GetActivityStatusByOrdinalPositionQueryValidator()
        {
            // No specific validation rules for this parameter-less query
        }
    }
}