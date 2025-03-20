using System;

namespace VibeCRM.Application.Features.NoteType.DTOs
{
    /// <summary>
    /// Data transfer object for note type information in list views, including note count
    /// </summary>
    public class NoteTypeListDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the note type
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
        /// Gets or sets the count of notes with this type
        /// </summary>
        public int NoteCount { get; set; }
    }
}
