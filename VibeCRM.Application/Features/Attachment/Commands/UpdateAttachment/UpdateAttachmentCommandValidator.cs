using FluentValidation;

namespace VibeCRM.Application.Features.Attachment.Commands.UpdateAttachment
{
    /// <summary>
    /// Validator for the UpdateAttachmentCommand.
    /// Defines validation rules for attachment updates.
    /// </summary>
    public class UpdateAttachmentCommandValidator : AbstractValidator<UpdateAttachmentCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAttachmentCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public UpdateAttachmentCommandValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage("Attachment ID is required.");

            RuleFor(a => a.AttachmentTypeId)
                .NotEmpty().WithMessage("Attachment type is required.");

            RuleFor(a => a.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(255).WithMessage("Subject must not exceed 255 characters.");

            RuleFor(a => a.Path)
                .NotEmpty().WithMessage("Path is required.")
                .MaximumLength(1000).WithMessage("Path must not exceed 1000 characters.");

            RuleFor(a => a.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}