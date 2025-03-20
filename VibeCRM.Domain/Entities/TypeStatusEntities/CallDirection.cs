using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents a call direction in the system, such as Inbound, Outbound, Missed, etc.
    /// Used to categorize call activities for tracking and reporting purposes.
    /// </summary>
    public class CallDirection : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallDirection"/> class.
        /// </summary>
        public CallDirection()
        {
            Activities = new List<Activity>();
            Id = Guid.NewGuid();
            Direction = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the call direction identifier that directly maps to the CallDirectionId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid CallDirectionId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the direction name (e.g., "Inbound", "Outbound", "Missed").
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// Gets or sets the description of the call direction with additional details about this type of call.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting call directions in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        // The Active property is inherited from BaseAuditableEntity<Guid>

        /// <summary>
        /// Gets or sets the collection of activities with this call direction.
        /// </summary>
        public ICollection<Activity> Activities { get; set; }
    }
}