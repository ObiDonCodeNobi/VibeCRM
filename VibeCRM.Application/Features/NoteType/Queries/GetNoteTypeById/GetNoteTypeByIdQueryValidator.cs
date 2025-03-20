using System;
using FluentValidation;

namespace VibeCRM.Application.Features.NoteType.Queries.GetNoteTypeById
{
    /// <summary>
    /// Validator for the GetNoteTypeByIdQuery
    /// </summary>
    public class GetNoteTypeByIdQueryValidator : AbstractValidator<GetNoteTypeByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetNoteTypeByIdQueryValidator class with validation rules
        /// </summary>
        public GetNoteTypeByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("A valid note type ID is required");
        }
    }
}
