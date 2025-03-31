using MediatR;
using VibeCRM.Shared.DTOs.Note;

namespace VibeCRM.Application.Features.Note.Queries.GetNotesByNoteType
{
    /// <summary>
    /// Query to retrieve all notes of a specific note type.
    /// This is used in the CQRS pattern as the request object for fetching notes by note type.
    /// </summary>
    public class GetNotesByNoteTypeQuery : IRequest<IEnumerable<NoteListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the note type to retrieve notes for.
        /// </summary>
        public Guid NoteTypeId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNotesByNoteTypeQuery"/> class.
        /// </summary>
        /// <param name="noteTypeId">The unique identifier of the note type to retrieve notes for.</param>
        public GetNotesByNoteTypeQuery(Guid noteTypeId)
        {
            NoteTypeId = noteTypeId;
        }
    }
}