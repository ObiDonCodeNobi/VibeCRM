using FluentValidation;
using VibeCRM.Application.Features.AttachmentType.DTOs;

namespace VibeCRM.Application.Features.AttachmentType.Validators
{
    /// <summary>
    /// Validator for the AttachmentTypeDetailsDto class.
    /// Defines validation rules for detailed attachment type data.
    /// </summary>
    public class AttachmentTypeDetailsDtoValidator : AbstractValidator<AttachmentTypeDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentTypeDetailsDtoValidator"/> class.
        /// Sets up validation rules for the AttachmentTypeDetailsDto properties.
        /// </summary>
        public AttachmentTypeDetailsDtoValidator()
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

            RuleFor(x => x.AttachmentCount)
                .GreaterThanOrEqualTo(0).WithMessage("Attachment count must be a non-negative number.");

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required.");
        }
    }
}