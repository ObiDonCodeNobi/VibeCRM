using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents a sales order status (e.g., New, Processing, Completed) in the CRM system.
    /// </summary>
    public class SalesOrderStatus : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderStatus"/> class.
        /// </summary>
        public SalesOrderStatus()
        {
            Id = Guid.NewGuid();
            Status = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the sales order status identifier that directly maps to the SalesOrderStatusId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid SalesOrderStatusId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the status name (e.g., New, Processing, Completed).
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the description of the sales order status.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for display ordering.
        /// </summary>
        public int OrdinalPosition { get; set; }

        // The Active property is inherited from BaseAuditableEntity<Guid>
    }
}