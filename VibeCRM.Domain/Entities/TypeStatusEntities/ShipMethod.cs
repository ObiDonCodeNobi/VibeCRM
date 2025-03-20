using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents a shipping method in the system, such as Standard, Express, Overnight, etc.
    /// </summary>
    public class ShipMethod : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShipMethod"/> class.
        /// </summary>
        public ShipMethod()
        {
            Id = Guid.NewGuid();
            Method = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the ship method identifier that directly maps to the ShipMethodId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid ShipMethodId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the method name (e.g., "Standard", "Express", "Overnight").
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the description of the shipping method with details about delivery times and costs.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting shipping methods in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        // The Active property is inherited from BaseAuditableEntity<Guid>
    }
}