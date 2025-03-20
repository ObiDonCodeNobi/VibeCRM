using FluentValidation;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetAllAttachmentTypes
{
    /// <summary>
    /// Validator for the GetAllAttachmentTypesQuery.
    /// Included for consistency with the validation pattern across the application.
    /// </summary>
    public class GetAllAttachmentTypesQueryValidator : AbstractValidator<GetAllAttachmentTypesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllAttachmentTypesQueryValidator"/> class.
        /// No specific rules are defined as the query has no parameters.
        /// </summary>
        public GetAllAttachmentTypesQueryValidator()
        {
            // No validation rules needed as this query has no parameters
        }
    }
}