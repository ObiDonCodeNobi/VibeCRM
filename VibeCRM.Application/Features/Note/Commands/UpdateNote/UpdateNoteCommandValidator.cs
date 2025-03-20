using FluentValidation;

namespace VibeCRM.Application.Features.Note.Commands.UpdateNote
{
    /// <summary>
    /// Validator for the UpdateNoteCommand.
    /// Defines validation rules for note updates.
    /// </summary>
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNoteCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public UpdateNoteCommandValidator()
        {
            RuleFor(n => n.NoteId)
                .NotEmpty().WithMessage("Note ID is required.");

            RuleFor(n => n.NoteTypeId)
                .NotEmpty().WithMessage("Note type is required.");

            RuleFor(n => n.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(200).WithMessage("Subject must not exceed 200 characters.");

            RuleFor(n => n.NoteText)
                .NotEmpty().WithMessage("Note text is required.")
                .MaximumLength(4000).WithMessage("Note text must not exceed 4000 characters.");

            RuleFor(n => n.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}