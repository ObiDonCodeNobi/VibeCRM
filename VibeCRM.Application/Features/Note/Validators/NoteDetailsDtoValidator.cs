using FluentValidation;
using VibeCRM.Application.Features.Note.DTOs;

namespace VibeCRM.Application.Features.Note.Validators
{
    /// <summary>
    /// Validator for the NoteDetailsDto.
    /// Extends the base NoteDtoValidator with additional validation rules for detailed note information.
    /// </summary>
    public class NoteDetailsDtoValidator : AbstractValidator<NoteDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteDetailsDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public NoteDetailsDtoValidator()
        {
            // Include all validation rules from the base NoteDto validator
            Include(new NoteDtoValidator());

            RuleFor(n => n.NoteTypeName)
                .MaximumLength(100).WithMessage("Note type name must not exceed 100 characters.");

            RuleFor(n => n.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(n => n.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required.");

            RuleFor(n => n.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.");

            RuleFor(n => n.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}