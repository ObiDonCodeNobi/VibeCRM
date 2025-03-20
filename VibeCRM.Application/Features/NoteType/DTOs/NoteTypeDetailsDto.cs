namespace VibeCRM.Application.Features.NoteType.DTOs
{
    /// <summary>
    /// Data transfer object for detailed note type information including audit fields
    /// </summary>
    public class NoteTypeDetailsDto
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
        /// Gets or sets the date and time when the note type was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the note type
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the note type was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the note type
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the note type is active
        /// </summary>
        public bool Active { get; set; }
    }
}