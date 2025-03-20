using FluentValidation;

namespace VibeCRM.Application.Features.NoteType.Queries.GetAllNoteTypes
{
    /// <summary>
    /// Validator for the GetAllNoteTypesQuery
    /// </summary>
    public class GetAllNoteTypesQueryValidator : AbstractValidator<GetAllNoteTypesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the GetAllNoteTypesQueryValidator class
        /// </summary>
        /// <remarks>
        /// This validator doesn't have any specific validation rules since the query doesn't have any parameters,
        /// but it's included for consistency with other query validators in the system.
        /// </remarks>
        public GetAllNoteTypesQueryValidator()
        {
            // No validation rules needed as this query doesn't have any parameters
        }
    }
}
