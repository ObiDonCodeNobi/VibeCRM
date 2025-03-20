using FluentValidation;
using VibeCRM.Application.Features.AttachmentType.DTOs;

namespace VibeCRM.Application.Features.AttachmentType.Validators
{
    /// <summary>
    /// Validator for the AttachmentTypeListDto class.
    /// Defines validation rules for attachment type list data.
    /// </summary>
    public class AttachmentTypeListDtoValidator : AbstractValidator<AttachmentTypeListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentTypeListDtoValidator"/> class.
        /// Sets up validation rules for the AttachmentTypeListDto properties.
        /// </summary>
        public AttachmentTypeListDtoValidator()
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
        }
    }
}