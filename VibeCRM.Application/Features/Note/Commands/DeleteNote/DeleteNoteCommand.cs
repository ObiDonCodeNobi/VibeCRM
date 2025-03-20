using MediatR;

namespace VibeCRM.Application.Features.Note.Commands.DeleteNote
{
    /// <summary>
    /// Command to delete (soft delete) a note in the system.
    /// This is used in the CQRS pattern as the request object for note deletion.
    /// </summary>
    public class DeleteNoteCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the note to delete.
        /// </summary>
        public Guid NoteId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user performing the deletion.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteNoteCommand"/> class.
        /// </summary>
        /// <param name="noteId">The unique identifier of the note to delete.</param>
        /// <param name="modifiedBy">The identifier of the user performing the deletion.</param>
        public DeleteNoteCommand(Guid noteId, Guid modifiedBy)
        {
            NoteId = noteId;
            ModifiedBy = modifiedBy;
        }
    }
}