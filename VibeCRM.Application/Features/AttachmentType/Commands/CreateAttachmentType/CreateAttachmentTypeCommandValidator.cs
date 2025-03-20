using FluentValidation;

namespace VibeCRM.Application.Features.AttachmentType.Commands.CreateAttachmentType
{
    /// <summary>
    /// Validator for the CreateAttachmentTypeCommand.
    /// Defines validation rules for creating a new attachment type.
    /// </summary>
    public class CreateAttachmentTypeCommandValidator : AbstractValidator<CreateAttachmentTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAttachmentTypeCommandValidator"/> class.
        /// Sets up validation rules for the CreateAttachmentTypeCommand properties.
        /// </summary>
        public CreateAttachmentTypeCommandValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(100).WithMessage("Type must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by is required.");
        }
    }
}