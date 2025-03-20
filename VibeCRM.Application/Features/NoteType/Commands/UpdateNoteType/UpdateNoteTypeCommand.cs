using System;
using MediatR;
using VibeCRM.Application.Features.NoteType.DTOs;

namespace VibeCRM.Application.Features.NoteType.Commands.UpdateNoteType
{
    /// <summary>
    /// Command to update an existing note type
    /// </summary>
    public class UpdateNoteTypeCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the note type to update
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the note type (e.g., "General", "Follow-up", "Meeting Notes")
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the note type
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting note types in listings and dropdowns
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who modified the note type
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
