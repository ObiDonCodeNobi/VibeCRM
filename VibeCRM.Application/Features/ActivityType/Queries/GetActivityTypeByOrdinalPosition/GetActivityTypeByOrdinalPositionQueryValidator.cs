using FluentValidation;

namespace VibeCRM.Application.Features.ActivityType.Queries.GetActivityTypeByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetActivityTypeByOrdinalPositionQuery class.
    /// Defines validation rules for retrieving an activity type by ordinal position.
    /// </summary>
    public class GetActivityTypeByOrdinalPositionQueryValidator : AbstractValidator<GetActivityTypeByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityTypeByOrdinalPositionQueryValidator"/> class.
        /// Configures validation rules for GetActivityTypeByOrdinalPositionQuery properties.
        /// </summary>
        public GetActivityTypeByOrdinalPositionQueryValidator()
        {
            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}