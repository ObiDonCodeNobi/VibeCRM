using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents an account status in the system, such as Active, Inactive, Prospect, etc.
    /// Used to track the status of accounts in the CRM system.
    /// </summary>
    public class AccountStatus : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountStatus"/> class.
        /// </summary>
        public AccountStatus()
        {
            Companies = new List<Company>();
            Id = Guid.NewGuid();
            Status = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the account status identifier that directly maps to the AccountStatusId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid AccountStatusId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the status name (e.g., "Active", "Inactive", "Prospect").
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the description of the account status.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting account statuses in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the collection of companies with this status.
        /// </summary>
        public ICollection<Company> Companies { get; set; }
    }
}