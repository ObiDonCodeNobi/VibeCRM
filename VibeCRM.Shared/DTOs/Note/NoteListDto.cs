namespace VibeCRM.Shared.DTOs.Note
{
    /// <summary>
    /// Data Transfer Object for displaying notes in lists.
    /// Contains properties optimized for list views.
    /// </summary>
    public class NoteListDto : NoteDto
    {
        /// <summary>
        /// Gets or sets the name of the note type.
        /// </summary>
        public string NoteTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the note was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}