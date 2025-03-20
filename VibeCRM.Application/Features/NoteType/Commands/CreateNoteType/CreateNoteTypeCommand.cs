using MediatR;
using VibeCRM.Application.Features.NoteType.DTOs;

namespace VibeCRM.Application.Features.NoteType.Commands.CreateNoteType
{
    /// <summary>
    /// Command to create a new note type
    /// </summary>
    public class CreateNoteTypeCommand : IRequest<NoteTypeDetailsDto>
    {
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
        /// Gets or sets the identifier of the user creating the note type
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
    }
}