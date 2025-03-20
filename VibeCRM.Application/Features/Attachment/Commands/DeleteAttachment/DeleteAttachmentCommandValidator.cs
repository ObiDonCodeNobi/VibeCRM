using FluentValidation;

namespace VibeCRM.Application.Features.Attachment.Commands.DeleteAttachment
{
    /// <summary>
    /// Validator for the DeleteAttachmentCommand.
    /// Defines validation rules for attachment deletion.
    /// </summary>
    public class DeleteAttachmentCommandValidator : AbstractValidator<DeleteAttachmentCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAttachmentCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public DeleteAttachmentCommandValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage("Attachment ID is required.");

            RuleFor(a => a.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}