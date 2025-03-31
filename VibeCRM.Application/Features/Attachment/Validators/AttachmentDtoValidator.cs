using FluentValidation;
using VibeCRM.Shared.DTOs.Attachment;

namespace VibeCRM.Application.Features.Attachment.Validators
{
    /// <summary>
    /// Validator for the AttachmentDto.
    /// Defines validation rules for attachment data transfer objects.
    /// </summary>
    public class AttachmentDtoValidator : AbstractValidator<AttachmentDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public AttachmentDtoValidator()
        {
            RuleFor(a => a.AttachmentTypeId)
                .NotEmpty().WithMessage("Attachment type is required.");

            RuleFor(a => a.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(255).WithMessage("Subject must not exceed 255 characters.");

            RuleFor(a => a.Path)
                .NotEmpty().WithMessage("Path is required.")
                .MaximumLength(1000).WithMessage("Path must not exceed 1000 characters.");
        }
    }
}