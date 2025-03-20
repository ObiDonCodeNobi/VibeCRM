using FluentValidation;
using VibeCRM.Application.Features.Note.DTOs;

namespace VibeCRM.Application.Features.Note.Validators
{
    /// <summary>
    /// Validator for the NoteDto.
    /// Defines validation rules for note data transfer objects.
    /// </summary>
    public class NoteDtoValidator : AbstractValidator<NoteDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public NoteDtoValidator()
        {
            RuleFor(n => n.Id)
                .NotEmpty().WithMessage("Note ID is required.");

            RuleFor(n => n.NoteTypeId)
                .NotEmpty().WithMessage("Note type is required.");

            RuleFor(n => n.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(200).WithMessage("Subject must not exceed 200 characters.");

            RuleFor(n => n.NoteText)
                .NotEmpty().WithMessage("Note text is required.")
                .MaximumLength(4000).WithMessage("Note text must not exceed 4000 characters.");
        }
    }
}