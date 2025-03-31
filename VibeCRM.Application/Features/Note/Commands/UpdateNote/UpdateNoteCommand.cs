using MediatR;
using VibeCRM.Shared.DTOs.Note;

namespace VibeCRM.Application.Features.Note.Commands.UpdateNote
{
    /// <summary>
    /// Command to update an existing note in the system.
    /// This is used in the CQRS pattern as the request object for note updates.
    /// </summary>
    public class UpdateNoteCommand : IRequest<NoteDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the note to update.
        /// </summary>
        public Guid NoteId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the note type.
        /// </summary>
        public Guid NoteTypeId { get; set; }

        /// <summary>
        /// Gets or sets the subject of the note.
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the text content of the note.
        /// </summary>
        public string NoteText { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identifier of the user modifying this note.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}