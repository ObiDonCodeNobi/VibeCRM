using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents a person status in the system, such as Active, Inactive, Prospect, etc.
    /// Used to track the status of individuals in the CRM system.
    /// </summary>
    public class PersonStatus : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonStatus"/> class.
        /// </summary>
        public PersonStatus()
        {
            People = new List<Person>();
            Id = Guid.NewGuid();
            Status = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the person status identifier that directly maps to the PersonStatusId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid PersonStatusId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the status name (e.g., "Active", "Inactive", "Prospect").
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the description of the person status.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting person statuses in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the collection of people with this status.
        /// </summary>
        public ICollection<Person> People { get; set; }
    }
}