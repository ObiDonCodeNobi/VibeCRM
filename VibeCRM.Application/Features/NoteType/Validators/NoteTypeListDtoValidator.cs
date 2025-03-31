using FluentValidation;
using VibeCRM.Shared.DTOs.NoteType;

namespace VibeCRM.Application.Features.NoteType.Validators
{
    /// <summary>
    /// Validator for the NoteTypeListDto class
    /// </summary>
    public class NoteTypeListDtoValidator : AbstractValidator<NoteTypeListDto>
    {
        /// <summary>
        /// Initializes a new instance of the NoteTypeListDtoValidator class with validation rules
        /// </summary>
        public NoteTypeListDtoValidator()
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

            RuleFor(x => x.NoteCount)
                .GreaterThanOrEqualTo(0).WithMessage("Note count must be a non-negative number");
        }
    }
}
