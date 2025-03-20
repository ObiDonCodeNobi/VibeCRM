using FluentValidation;

namespace VibeCRM.Application.Features.AttachmentType.Commands.DeleteAttachmentType
{
    /// <summary>
    /// Validator for the DeleteAttachmentTypeCommand.
    /// Defines validation rules for soft deleting an existing attachment type.
    /// </summary>
    public class DeleteAttachmentTypeCommandValidator : AbstractValidator<DeleteAttachmentTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAttachmentTypeCommandValidator"/> class.
        /// Sets up validation rules for the DeleteAttachmentTypeCommand properties.
        /// </summary>
        public DeleteAttachmentTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Attachment type ID is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required.");
        }
    }
}