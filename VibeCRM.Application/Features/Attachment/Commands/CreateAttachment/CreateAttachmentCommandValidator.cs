using FluentValidation;

namespace VibeCRM.Application.Features.Attachment.Commands.CreateAttachment
{
    /// <summary>
    /// Validator for the CreateAttachmentCommand.
    /// Defines validation rules for attachment creation.
    /// </summary>
    public class CreateAttachmentCommandValidator : AbstractValidator<CreateAttachmentCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAttachmentCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public CreateAttachmentCommandValidator()
        {
            RuleFor(a => a.AttachmentTypeId)
                .NotEmpty().WithMessage("Attachment type is required.");

            RuleFor(a => a.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(255).WithMessage("Subject must not exceed 255 characters.");

            RuleFor(a => a.Path)
                .NotEmpty().WithMessage("Path is required.")
                .MaximumLength(1000).WithMessage("Path must not exceed 1000 characters.");

            RuleFor(a => a.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required.");

            RuleFor(a => a.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}