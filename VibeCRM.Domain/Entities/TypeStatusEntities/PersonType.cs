using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents a person type in the system
    /// </summary>
    public class PersonType : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the PersonType class
        /// </summary>
        public PersonType() { People = new List<Person>(); Id = Guid.NewGuid(); Type = string.Empty; Description = string.Empty; }

        /// <summary>
        /// Gets or sets the person type identifier that directly maps to the PersonTypeId database column
        /// </summary>
        public Guid PersonTypeId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the person type name
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the person type description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting
        /// </summary>
        public int OrdinalPosition { get; set; }

        // Removed redundant Active property since it's already defined in BaseAuditableEntity<Guid>

        /// <summary>
        /// Gets or sets the collection of people of this type
        /// </summary>
        public ICollection<Person> People { get; set; }
    }
}