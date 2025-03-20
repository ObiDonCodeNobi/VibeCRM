using FluentValidation;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetDefaultAttachmentType
{
    /// <summary>
    /// Validator for the GetDefaultAttachmentTypeQuery.
    /// Included for consistency with the validation pattern across the application.
    /// </summary>
    public class GetDefaultAttachmentTypeQueryValidator : AbstractValidator<GetDefaultAttachmentTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultAttachmentTypeQueryValidator"/> class.
        /// No specific rules are defined as the query has no parameters.
        /// </summary>
        public GetDefaultAttachmentTypeQueryValidator()
        {
            // No validation rules needed as this query has no parameters
        }
    }
}