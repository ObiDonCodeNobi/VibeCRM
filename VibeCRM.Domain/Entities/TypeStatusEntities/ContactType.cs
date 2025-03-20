using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents a contact type in the system, such as Customer, Vendor, Partner, etc.
    /// Used to categorize contacts for organization and reporting.
    /// </summary>
    public class ContactType : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactType"/> class.
        /// </summary>
        public ContactType()
        {
            Contacts = new List<Person>();
            Id = Guid.NewGuid();
            Type = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the contact type identifier that directly maps to the ContactTypeId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid ContactTypeId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Customer", "Vendor", "Partner").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the contact type.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting contact types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        // The Active property is inherited from BaseAuditableEntity<Guid>

        /// <summary>
        /// Gets or sets the collection of contacts of this type.
        /// </summary>
        public ICollection<Person> Contacts { get; set; }
    }
}