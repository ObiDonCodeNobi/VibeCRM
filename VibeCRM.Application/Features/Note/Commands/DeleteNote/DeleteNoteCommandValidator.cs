using FluentValidation;

namespace VibeCRM.Application.Features.Note.Commands.DeleteNote
{
    /// <summary>
    /// Validator for the DeleteNoteCommand.
    /// Defines validation rules for note deletion.
    /// </summary>
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteNoteCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public DeleteNoteCommandValidator()
        {
            RuleFor(n => n.NoteId)
                .NotEmpty().WithMessage("Note ID is required.");

            RuleFor(n => n.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}