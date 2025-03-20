using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a predefined activity definition that can be used to create new activities.
    /// </summary>
    public class ActivityDefinition : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityDefinition"/> class.
        /// </summary>
        public ActivityDefinition()
        {
            Id = Guid.NewGuid();
            Subject = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the activity definition identifier that directly maps to the ActivityDefinitionId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid ActivityDefinitionId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the unique identifier of the activity type.
        /// </summary>
        public Guid ActivityTypeId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the activity status.
        /// </summary>
        public Guid ActivityStatusId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user assigned to this activity definition.
        /// </summary>
        public Guid AssignedUserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the team assigned to this activity definition.
        /// </summary>
        public Guid AssignedTeamId { get; set; }

        /// <summary>
        /// Gets or sets the subject or title of the activity definition.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the detailed description of the activity definition.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the number of days from the creation date when the activity will be due.
        /// </summary>
        public int DueDateOffset { get; set; }
    }
}