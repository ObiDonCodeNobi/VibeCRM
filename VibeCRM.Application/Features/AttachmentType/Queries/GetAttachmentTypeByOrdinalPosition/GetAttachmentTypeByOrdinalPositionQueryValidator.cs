using FluentValidation;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetAttachmentTypeByOrdinalPositionQuery.
    /// Included for consistency with the validation pattern across the application.
    /// </summary>
    public class GetAttachmentTypeByOrdinalPositionQueryValidator : AbstractValidator<GetAttachmentTypeByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAttachmentTypeByOrdinalPositionQueryValidator"/> class.
        /// No specific rules are defined as the query has no parameters.
        /// </summary>
        public GetAttachmentTypeByOrdinalPositionQueryValidator()
        {
            // No validation rules needed as this query has no parameters
        }
    }
}