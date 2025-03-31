using FluentValidation;
using VibeCRM.Shared.DTOs.Note;

namespace VibeCRM.Application.Features.Note.Validators
{
    /// <summary>
    /// Validator for the NoteListDto.
    /// Extends the base NoteDtoValidator with additional validation rules for list display note information.
    /// </summary>
    public class NoteListDtoValidator : AbstractValidator<NoteListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteListDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public NoteListDtoValidator()
        {
            // Include all validation rules from the base NoteDto validator
            Include(new NoteDtoValidator());

            RuleFor(n => n.NoteTypeName)
                .MaximumLength(100).WithMessage("Note type name must not exceed 100 characters.");

            RuleFor(n => n.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");
        }
    }
}