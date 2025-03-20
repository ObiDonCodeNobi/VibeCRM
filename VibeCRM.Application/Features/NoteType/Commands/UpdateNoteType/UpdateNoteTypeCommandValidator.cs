using FluentValidation;

namespace VibeCRM.Application.Features.NoteType.Commands.UpdateNoteType
{
    /// <summary>
    /// Validator for the UpdateNoteTypeCommand
    /// </summary>
    public class UpdateNoteTypeCommandValidator : AbstractValidator<UpdateNoteTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateNoteTypeCommandValidator class with validation rules
        /// </summary>
        public UpdateNoteTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("A valid note type ID is required");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required")
                .MaximumLength(100).WithMessage("Type cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("ModifiedBy is required")
                .MaximumLength(100).WithMessage("ModifiedBy cannot exceed 100 characters");
        }
    }
}