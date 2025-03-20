using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents an address in the CRM system
    /// </summary>
    public class Address : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        public Address()
        { Companies = new HashSet<Company_Address>(); Persons = new HashSet<Person_Address>(); Id = Guid.NewGuid(); Line1 = string.Empty; City = string.Empty; Zip = string.Empty; }

        /// <summary>
        /// Gets or sets the address identifier that directly maps to the AddressId database column
        /// </summary>
        public Guid AddressId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the address type identifier
        /// </summary>
        public Guid AddressTypeId { get; set; }

        /// <summary>
        /// Gets or sets the first line of the address
        /// </summary>
        public string Line1 { get; set; }

        /// <summary>
        /// Gets or sets the second line of the address
        /// </summary>
        public string? Line2 { get; set; }

        /// <summary>
        /// Gets or sets the city of the address
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state identifier
        /// </summary>
        public Guid StateId { get; set; }

        /// <summary>
        /// Gets or sets the zip code
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Gets the full address as a formatted string (computed property)
        /// </summary>
        public string FullAddress => $"{Line1}, {(string.IsNullOrEmpty(Line2) ? "" : Line2 + ", ")}{City}, {Zip}";

        /// <summary>
        /// Gets or sets the address type
        /// </summary>
        public AddressType? AddressType { get; set; }

        /// <summary>
        /// Gets or sets the state associated with this address
        /// </summary>
        public State? State { get; set; }

        /// <summary>
        /// Gets or sets the collection of companies associated with this address
        /// </summary>
        public ICollection<Company_Address> Companies { get; set; }

        /// <summary>
        /// Gets or sets the collection of persons associated with this address
        /// </summary>
        public ICollection<Person_Address> Persons { get; set; }
    }
}