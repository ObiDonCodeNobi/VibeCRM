using FluentValidation;

namespace VibeCRM.Application.Features.CallType.Queries.GetCallTypesByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetCallTypesByOrdinalPositionQuery.
    /// Defines validation rules for retrieving call types ordered by their ordinal position.
    /// </summary>
    public class GetCallTypesByOrdinalPositionQueryValidator : AbstractValidator<GetCallTypesByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCallTypesByOrdinalPositionQueryValidator"/> class.
        /// Sets up validation rules for the GetCallTypesByOrdinalPositionQuery.
        /// </summary>
        public GetCallTypesByOrdinalPositionQueryValidator()
        {
            // No specific validation rules needed for this query as it has no parameters
        }
    }
}