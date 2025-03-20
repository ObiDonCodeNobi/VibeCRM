using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents a note type in the system, such as General, Follow-up, Meeting Notes, etc.
    /// Used to categorize notes for organization and reporting.
    /// </summary>
    public class NoteType : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteType"/> class.
        /// </summary>
        public NoteType()
        {
            Notes = new List<Note>();
            Id = Guid.NewGuid();
            Type = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the note type identifier that directly maps to the NoteTypeId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid NoteTypeId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the note type name (e.g., "General", "Follow-up", "Meeting Notes").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the note type description with details about when this type of note should be used.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting note types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        // The Active property is inherited from BaseAuditableEntity<Guid>

        /// <summary>
        /// Gets or sets the collection of notes of this type.
        /// </summary>
        public ICollection<Note> Notes { get; set; }
    }
}