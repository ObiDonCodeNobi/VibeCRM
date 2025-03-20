using System;
using MediatR;

namespace VibeCRM.Application.Features.NoteType.Commands.DeleteNoteType
{
    /// <summary>
    /// Command to soft delete a note type
    /// </summary>
    public class DeleteNoteTypeCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the note type to delete
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user performing the delete operation
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
