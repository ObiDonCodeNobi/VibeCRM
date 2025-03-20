using FluentValidation;

namespace VibeCRM.Application.Features.AttachmentType.Commands.UpdateAttachmentType
{
    /// <summary>
    /// Validator for the UpdateAttachmentTypeCommand.
    /// Defines validation rules for updating an existing attachment type.
    /// </summary>
    public class UpdateAttachmentTypeCommandValidator : AbstractValidator<UpdateAttachmentTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAttachmentTypeCommandValidator"/> class.
        /// Sets up validation rules for the UpdateAttachmentTypeCommand properties.
        /// </summary>
        public UpdateAttachmentTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Attachment type ID is required.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(100).WithMessage("Type must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required.");
        }
    }
}