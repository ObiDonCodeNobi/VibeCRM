using FluentValidation;
using VibeCRM.Application.Features.AttachmentType.DTOs;

namespace VibeCRM.Application.Features.AttachmentType.Validators
{
    /// <summary>
    /// Validator for the AttachmentTypeDto class.
    /// Defines validation rules for attachment type data.
    /// </summary>
    public class AttachmentTypeDtoValidator : AbstractValidator<AttachmentTypeDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentTypeDtoValidator"/> class.
        /// Sets up validation rules for the AttachmentTypeDto properties.
        /// </summary>
        public AttachmentTypeDtoValidator()
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
        }
    }
}