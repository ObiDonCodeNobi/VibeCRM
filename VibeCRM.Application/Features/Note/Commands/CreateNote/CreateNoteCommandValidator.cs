using FluentValidation;

namespace VibeCRM.Application.Features.Note.Commands.CreateNote
{
    /// <summary>
    /// Validator for the CreateNoteCommand.
    /// Defines validation rules for note creation.
    /// </summary>
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNoteCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public CreateNoteCommandValidator()
        {
            RuleFor(n => n.NoteTypeId)
                .NotEmpty().WithMessage("Note type is required.");

            RuleFor(n => n.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(200).WithMessage("Subject must not exceed 200 characters.");

            RuleFor(n => n.NoteText)
                .NotEmpty().WithMessage("Note text is required.")
                .MaximumLength(4000).WithMessage("Note text must not exceed 4000 characters.");

            RuleFor(n => n.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required.");

            RuleFor(n => n.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}