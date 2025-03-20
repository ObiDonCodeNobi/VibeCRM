using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents an activity status in the system, such as Scheduled, In Progress, Completed, etc.
    /// Used to track the status of activities in the CRM system.
    /// </summary>
    public class ActivityStatus : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityStatus"/> class.
        /// </summary>
        public ActivityStatus()
        {
            Activities = new List<Activity>();
            Id = Guid.NewGuid();
            Status = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the activity status identifier that directly maps to the ActivityStatusId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid ActivityStatusId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the status name (e.g., "Scheduled", "In Progress", "Completed").
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the description of the activity status.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting activity statuses in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the collection of activities with this status.
        /// </summary>
        public ICollection<Activity> Activities { get; set; }
    }
}