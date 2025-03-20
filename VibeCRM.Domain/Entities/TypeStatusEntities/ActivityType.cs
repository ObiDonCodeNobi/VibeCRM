using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents an activity type in the system, such as Meeting, Call, Email, etc.
    /// Used to categorize activities for organization and reporting.
    /// </summary>
    public class ActivityType : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityType"/> class.
        /// </summary>
        public ActivityType()
        {
            Activities = new List<Activity>();
            Id = Guid.NewGuid();
            Type = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the activity type identifier that directly maps to the ActivityTypeId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid ActivityTypeId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Meeting", "Call", "Email").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the activity type.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting activity types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the collection of activities of this type.
        /// </summary>
        public ICollection<Activity> Activities { get; set; }
    }
}