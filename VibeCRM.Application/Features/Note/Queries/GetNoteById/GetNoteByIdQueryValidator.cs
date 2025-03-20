using FluentValidation;

namespace VibeCRM.Application.Features.Note.Queries.GetNoteById
{
    /// <summary>
    /// Validator for the GetNoteByIdQuery to ensure the query contains valid parameters.
    /// </summary>
    public class GetNoteByIdQueryValidator : AbstractValidator<GetNoteByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetNoteByIdQueryValidator class with validation rules.
        /// </summary>
        public GetNoteByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}