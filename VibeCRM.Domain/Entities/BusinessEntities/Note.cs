using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a note in the CRM system
    /// </summary>
    public class Note : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Note"/> class.
        /// </summary>
        public Note()
        { Companies = new HashSet<Company_Note>(); Persons = new HashSet<Person_Note>(); Id = Guid.NewGuid(); Subject = string.Empty; NoteText = string.Empty; }

        /// <summary>
        /// Gets or sets the note identifier that directly maps to the NoteId database column
        /// </summary>
        public Guid NoteId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the note type identifier
        /// </summary>
        public Guid NoteTypeId { get; set; }

        /// <summary>
        /// Gets or sets the subject of the note
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the text content of the note
        /// </summary>
        public string NoteText { get; set; }

        /// <summary>
        /// Gets or sets the note type
        /// </summary>
        public NoteType? NoteType { get; set; }

        /// <summary>
        /// Gets or sets the collection of companies associated with this note
        /// </summary>
        public ICollection<Company_Note> Companies { get; set; }

        /// <summary>
        /// Gets or sets the collection of persons associated with this note
        /// </summary>
        public ICollection<Person_Note> Persons { get; set; }
    }
}