using FluentValidation;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeByFileExtension
{
    /// <summary>
    /// Validator for the GetAttachmentTypeByFileExtensionQuery.
    /// Defines validation rules for retrieving attachment types by file extension.
    /// </summary>
    public class GetAttachmentTypeByFileExtensionQueryValidator : AbstractValidator<GetAttachmentTypeByFileExtensionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAttachmentTypeByFileExtensionQueryValidator"/> class.
        /// Sets up validation rules for the GetAttachmentTypeByFileExtensionQuery properties.
        /// </summary>
        public GetAttachmentTypeByFileExtensionQueryValidator()
        {
            RuleFor(x => x.FileExtension)
                .NotEmpty().WithMessage("File extension is required.")
                .MaximumLength(20).WithMessage("File extension must not exceed 20 characters.");
        }
    }
}