using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents an email address type in the system, such as Personal, Work, etc.
    /// Used to categorize email addresses for organization and communication preferences.
    /// </summary>
    public class EmailAddressType : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddressType"/> class.
        /// </summary>
        public EmailAddressType()
        {
            EmailAddresses = new List<EmailAddress>();
            Id = Guid.NewGuid();
            Type = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the email address type identifier that directly maps to the EmailAddressTypeId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid EmailAddressTypeId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Personal", "Work").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the email address type.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting email address types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the collection of email addresses of this type.
        /// </summary>
        public ICollection<EmailAddress> EmailAddresses { get; set; }
    }
}