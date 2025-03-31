using FluentValidation;
using VibeCRM.Shared.DTOs.Attachment;

namespace VibeCRM.Application.Features.Attachment.Validators
{
    /// <summary>
    /// Validator for the AttachmentListDto.
    /// Defines validation rules for attachment list data transfer objects.
    /// </summary>
    public class AttachmentListDtoValidator : AbstractValidator<AttachmentListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentListDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public AttachmentListDtoValidator()
        {
            Include(new AttachmentDtoValidator());

            RuleFor(a => a.AttachmentTypeName)
                .NotEmpty().WithMessage("Attachment type name is required.")
                .MaximumLength(100).WithMessage("Attachment type name must not exceed 100 characters.");
        }
    }
}