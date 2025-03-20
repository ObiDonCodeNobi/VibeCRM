using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents an address type in the system, such as Home, Work, Billing, Shipping, etc.
    /// </summary>
    public class AddressType : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressType"/> class.
        /// </summary>
        public AddressType()
        {
            Addresses = new List<Address>();
            Id = Guid.NewGuid();
            Type = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the address type identifier that directly maps to the AddressTypeId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid AddressTypeId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the address type name (e.g., "Home", "Work", "Billing", "Shipping").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the address type description with details about when this address type should be used.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting address types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        // The Active property is inherited from BaseAuditableEntity<Guid>

        /// <summary>
        /// Gets or sets the collection of addresses of this type.
        /// </summary>
        public ICollection<Address> Addresses { get; set; }
    }
}