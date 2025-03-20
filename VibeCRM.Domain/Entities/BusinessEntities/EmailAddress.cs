using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents an email address in the CRM system
    /// </summary>
    public class EmailAddress : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress"/> class.
        /// </summary>
        public EmailAddress() { Companies = new HashSet<Company_EmailAddress>(); Persons = new HashSet<Person_EmailAddress>(); Id = Guid.NewGuid(); Address = string.Empty; }

        /// <summary>
        /// Gets or sets the email address identifier that directly maps to the EmailAddressId database column
        /// </summary>
        public Guid EmailAddressId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the email address type identifier
        /// </summary>
        public Guid EmailAddressTypeId { get; set; }

        /// <summary>
        /// Gets or sets the email address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the email address type
        /// </summary>
        public EmailAddressType? EmailAddressType { get; set; }

        /// <summary>
        /// Gets or sets the collection of companies associated with this email address
        /// </summary>
        public ICollection<Company_EmailAddress> Companies { get; set; }

        /// <summary>
        /// Gets or sets the collection of persons associated with this email address
        /// </summary>
        public ICollection<Person_EmailAddress> Persons { get; set; }
    }
}