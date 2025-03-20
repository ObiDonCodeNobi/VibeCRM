using FluentValidation;

namespace VibeCRM.Application.Features.Attachment.Queries.GetAttachmentById
{
    /// <summary>
    /// Validator for the GetAttachmentByIdQuery to ensure the query contains valid parameters.
    /// </summary>
    public class GetAttachmentByIdQueryValidator : AbstractValidator<GetAttachmentByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAttachmentByIdQueryValidator class with validation rules.
        /// </summary>
        public GetAttachmentByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}