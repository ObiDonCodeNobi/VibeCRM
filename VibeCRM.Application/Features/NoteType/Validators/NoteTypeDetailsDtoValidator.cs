using FluentValidation;
using VibeCRM.Shared.DTOs.NoteType;

namespace VibeCRM.Application.Features.NoteType.Validators
{
    /// <summary>
    /// Validator for the NoteTypeDetailsDto class
    /// </summary>
    public class NoteTypeDetailsDtoValidator : AbstractValidator<NoteTypeDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the NoteTypeDetailsDtoValidator class with validation rules
        /// </summary>
        public NoteTypeDetailsDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required")
                .MaximumLength(100).WithMessage("Type cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number");

            // Audit fields validation
            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Created date is required");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by is required")
                .MaximumLength(100).WithMessage("Created by cannot exceed 100 characters");

            RuleFor(x => x.ModifiedBy)
                .MaximumLength(100).WithMessage("Modified by cannot exceed 100 characters");
        }
    }
}
