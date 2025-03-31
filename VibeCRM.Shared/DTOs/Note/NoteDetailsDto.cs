namespace VibeCRM.Shared.DTOs.Note
{
    /// <summary>
    /// Data Transfer Object for detailed Note information.
    /// Extends the base NoteDto with additional properties for detailed views.
    /// </summary>
    public class NoteDetailsDto : NoteDto
    {
        /// <summary>
        /// Gets or sets the name of the note type.
        /// </summary>
        public string NoteTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the note was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the note.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the note was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the note.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}