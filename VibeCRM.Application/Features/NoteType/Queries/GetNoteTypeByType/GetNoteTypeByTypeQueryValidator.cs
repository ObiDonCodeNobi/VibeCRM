using FluentValidation;

namespace VibeCRM.Application.Features.NoteType.Queries.GetNoteTypeByType
{
    /// <summary>
    /// Validator for the GetNoteTypeByTypeQuery
    /// </summary>
    public class GetNoteTypeByTypeQueryValidator : AbstractValidator<GetNoteTypeByTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetNoteTypeByTypeQueryValidator class with validation rules
        /// </summary>
        public GetNoteTypeByTypeQueryValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required")
                .MaximumLength(100).WithMessage("Type cannot exceed 100 characters");
        }
    }
}
