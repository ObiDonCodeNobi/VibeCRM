using System;
using FluentValidation;

namespace VibeCRM.Application.Features.NoteType.Commands.DeleteNoteType
{
    /// <summary>
    /// Validator for the DeleteNoteTypeCommand
    /// </summary>
    public class DeleteNoteTypeCommandValidator : AbstractValidator<DeleteNoteTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the DeleteNoteTypeCommandValidator class with validation rules
        /// </summary>
        public DeleteNoteTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("A valid note type ID is required");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("ModifiedBy is required")
                .MaximumLength(100).WithMessage("ModifiedBy cannot exceed 100 characters");
        }
    }
}
