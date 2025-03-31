using MediatR;
using VibeCRM.Shared.DTOs.Note;

namespace VibeCRM.Application.Features.Note.Queries.GetAllNotes
{
    /// <summary>
    /// Query to retrieve all notes with pagination
    /// </summary>
    public class GetAllNotesQuery : IRequest<IEnumerable<NoteDto>>
    {
        /// <summary>
        /// Gets or sets the page number for pagination (1-based)
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the page size for pagination
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}