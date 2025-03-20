using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents an attachment in the CRM system
    /// </summary>
    public class Attachment : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Attachment"/> class.
        /// </summary>
        public Attachment() { Companies = new HashSet<Company_Attachment>(); Persons = new HashSet<Person_Attachment>(); Id = Guid.NewGuid(); Subject = string.Empty; Path = string.Empty; }

        /// <summary>
        /// Gets or sets the attachment identifier that directly maps to the AttachmentId database column
        /// </summary>
        public Guid AttachmentId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the attachment type identifier
        /// </summary>
        public Guid AttachmentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the subject of the attachment
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the file path or storage identifier of the attachment
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets the filename from the path (computed property)
        /// </summary>
        public string Filename => System.IO.Path.GetFileName(Path);

        /// <summary>
        /// Gets or sets the attachment type
        /// </summary>
        public AttachmentType? AttachmentType { get; set; }

        /// <summary>
        /// Gets or sets the collection of companies associated with this attachment
        /// </summary>
        public ICollection<Company_Attachment> Companies { get; set; }

        /// <summary>
        /// Gets or sets the collection of persons associated with this attachment
        /// </summary>
        public ICollection<Person_Attachment> Persons { get; set; }
    }
}