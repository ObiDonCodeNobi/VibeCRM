using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents an attachment type in the system, such as Document, Image, Contract, etc.
    /// Used to categorize attachments for organization and reporting.
    /// </summary>
    public class AttachmentType : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentType"/> class.
        /// </summary>
        public AttachmentType()
        {
            Attachments = new List<Attachment>();
            Id = Guid.NewGuid();
            Type = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the attachment type identifier that directly maps to the AttachmentTypeId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid AttachmentTypeId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Document", "Image", "Contract").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the attachment type.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting attachment types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the collection of attachments of this type.
        /// </summary>
        public ICollection<Attachment> Attachments { get; set; }
    }
}