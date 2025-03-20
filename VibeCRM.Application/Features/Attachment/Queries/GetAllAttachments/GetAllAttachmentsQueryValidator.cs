using FluentValidation;

namespace VibeCRM.Application.Features.Attachment.Queries.GetAllAttachments
{
    /// <summary>
    /// Validator for the GetAllAttachmentsQuery to ensure the query contains valid pagination parameters.
    /// </summary>
    public class GetAllAttachmentsQueryValidator : AbstractValidator<GetAllAttachmentsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllAttachmentsQueryValidator class with validation rules.
        /// </summary>
        public GetAllAttachmentsQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.");

            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not exceed 100.");
        }
    }
}