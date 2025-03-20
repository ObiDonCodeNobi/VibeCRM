using FluentValidation;

namespace VibeCRM.Application.Features.NoteType.Queries.GetNoteTypeByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetNoteTypeByOrdinalPositionQuery
    /// </summary>
    public class GetNoteTypeByOrdinalPositionQueryValidator : AbstractValidator<GetNoteTypeByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetNoteTypeByOrdinalPositionQueryValidator class
        /// </summary>
        /// <remarks>
        /// This validator doesn't have any specific validation rules since the query doesn't have any parameters,
        /// but it's included for consistency with other query validators in the system.
        /// </remarks>
        public GetNoteTypeByOrdinalPositionQueryValidator()
        {
            // No validation rules needed as this query doesn't have any parameters
        }
    }
}
