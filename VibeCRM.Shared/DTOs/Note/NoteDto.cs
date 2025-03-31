namespace VibeCRM.Shared.DTOs.Note
{
    /// <summary>
    /// Data Transfer Object for Note entities.
    /// Contains the basic properties of a note for data transfer between layers.
    /// </summary>
    public class NoteDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the note.
        /// </summary>
        public Guid Id { get; set; }

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
        /// Gets or sets a value indicating whether the note is active.
        /// Used for soft delete functionality.
        /// </summary>
        public bool Active { get; set; }
    }
}