using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents a type of service offered by the organization, such as Consulting, Implementation, Training, etc.
    /// ServiceType is used to categorize services in the system.
    /// </summary>
    public class ServiceType : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceType"/> class.
        /// </summary>
        public ServiceType()
        {
            Services = new List<Service>();
            Id = Guid.NewGuid();
            Type = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the service type identifier that directly maps to the ServiceTypeId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid ServiceTypeId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the type name of the service (e.g., "Consulting", "Implementation", "Training").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the detailed description of the service type with information about
        /// the purpose and features of services in this category.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting service types in listings and dropdowns.
        /// Used to control the ordering of service types in UI components.
        /// </summary>
        public int OrdinalPosition { get; set; }

        // The Active property is inherited from BaseAuditableEntity<Guid>

        /// <summary>
        /// Gets or sets the collection of services of this type.
        /// </summary>
        public ICollection<Service> Services { get; set; }
    }
}