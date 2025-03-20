using FluentValidation;

namespace VibeCRM.Application.Features.NoteType.Commands.CreateNoteType
{
    /// <summary>
    /// Validator for the CreateNoteTypeCommand
    /// </summary>
    public class CreateNoteTypeCommandValidator : AbstractValidator<CreateNoteTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateNoteTypeCommandValidator class with validation rules
        /// </summary>
        public CreateNoteTypeCommandValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required")
                .MaximumLength(100).WithMessage("Type cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0).WithMessage("Ordinal position must be a non-negative number");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("CreatedBy is required")
                .MaximumLength(100).WithMessage("CreatedBy cannot exceed 100 characters");
        }
    }
}