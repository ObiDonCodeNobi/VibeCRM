using MediatR;
using VibeCRM.Application.Features.Note.DTOs;

namespace VibeCRM.Application.Features.Note.Commands.CreateNote
{
    /// <summary>
    /// Command to create a new note in the system.
    /// This is used in the CQRS pattern as the request object for note creation.
    /// </summary>
    public class CreateNoteCommand : IRequest<NoteDetailsDto>
    {
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
        /// Gets or sets the identifier of the user creating this note.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user modifying this note.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}