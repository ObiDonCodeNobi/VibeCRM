using FluentValidation;
using VibeCRM.Application.Features.NoteType.DTOs;

namespace VibeCRM.Application.Features.NoteType.Validators
{
    /// <summary>
    /// Validator for the NoteTypeDto class
    /// </summary>
    public class NoteTypeDtoValidator : AbstractValidator<NoteTypeDto>
    {
        /// <summary>
        /// Initializes a new instance of the NoteTypeDtoValidator class with validation rules
        /// </summary>
        public NoteTypeDtoValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required")
                .MaximumLength(100).WithMessage("Type cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number");
        }
    }
}