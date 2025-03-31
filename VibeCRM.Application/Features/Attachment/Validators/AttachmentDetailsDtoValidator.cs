using FluentValidation;
using VibeCRM.Shared.DTOs.Attachment;

namespace VibeCRM.Application.Features.Attachment.Validators
{
    /// <summary>
    /// Validator for the AttachmentDetailsDto.
    /// Defines validation rules for detailed attachment data transfer objects.
    /// </summary>
    public class AttachmentDetailsDtoValidator : AbstractValidator<AttachmentDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentDetailsDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public AttachmentDetailsDtoValidator()
        {
            Include(new AttachmentDtoValidator());

            RuleFor(a => a.AttachmentTypeName)
                .NotEmpty().WithMessage("Attachment type name is required.")
                .MaximumLength(100).WithMessage("Attachment type name must not exceed 100 characters.");

            RuleFor(a => a.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required.");

            RuleFor(a => a.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(a => a.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");

            RuleFor(a => a.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.");
        }
    }
}