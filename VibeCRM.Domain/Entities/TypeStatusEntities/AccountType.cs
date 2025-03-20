using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents an account type in the system, such as Customer, Vendor, Partner, etc.
    /// Used to categorize accounts for organization and reporting.
    /// </summary>
    public class AccountType : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountType"/> class.
        /// </summary>
        public AccountType()
        {
            Companies = new List<BusinessEntities.Company>();
            Id = Guid.NewGuid();
            Type = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the account type identifier that directly maps to the AccountTypeId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid AccountTypeId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Customer", "Vendor", "Partner").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the account type.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting account types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the collection of companies of this type.
        /// </summary>
        public ICollection<BusinessEntities.Company> Companies { get; set; }
    }
}