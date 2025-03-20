using FluentValidation;

namespace VibeCRM.Application.Features.Note.Queries.GetAllNotes
{
    /// <summary>
    /// Validator for the GetAllNotesQuery to ensure the query contains valid pagination parameters.
    /// </summary>
    public class GetAllNotesQueryValidator : AbstractValidator<GetAllNotesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllNotesQueryValidator class with validation rules.
        /// </summary>
        public GetAllNotesQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.");

            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must not exceed 100.");
        }
    }
}