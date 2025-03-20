using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents an activity in the CRM system
    /// </summary>
    public class Activity : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Activity"/> class.
        /// </summary>
        public Activity() 
        { 
            Companies = new HashSet<Company_Activity>(); 
            Persons = new HashSet<Person_Activity>(); 
            Id = Guid.NewGuid(); 
            Subject = string.Empty; 
            Description = string.Empty; 
        }

        /// <summary>
        /// Gets or sets the activity identifier that directly maps to the ActivityId database column
        /// </summary>
        public Guid ActivityId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the activity type identifier
        /// </summary>
        public Guid ActivityTypeId { get; set; }

        /// <summary>
        /// Gets or sets the activity status identifier
        /// </summary>
        public Guid ActivityStatusId { get; set; }

        /// <summary>
        /// Gets or sets the assigned user identifier
        /// </summary>
        public Guid? AssignedUserId { get; set; }

        /// <summary>
        /// Gets or sets the assigned team identifier
        /// </summary>
        public Guid? AssignedTeamId { get; set; }

        /// <summary>
        /// Gets or sets the subject of the activity
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the description of the activity
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the due date of the activity
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the start date of the activity
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the completed date of the activity
        /// </summary>
        public DateTime? CompletedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who completed the activity
        /// </summary>
        public Guid? CompletedBy { get; set; }

        /// <summary>
        /// Gets a value indicating whether the activity is completed (computed property)
        /// </summary>
        public bool IsCompleted => CompletedDate.HasValue;

        /// <summary>
        /// Gets or sets the type of the activity
        /// </summary>
        public ActivityType? ActivityType { get; set; }

        /// <summary>
        /// Gets or sets the status of the activity
        /// </summary>
        public ActivityStatus? ActivityStatus { get; set; }

        /// <summary>
        /// Gets or sets the collection of companies associated with this activity
        /// </summary>
        public ICollection<Company_Activity> Companies { get; set; }

        /// <summary>
        /// Gets or sets the collection of persons associated with this activity
        /// </summary>
        public ICollection<Person_Activity> Persons { get; set; }
    }
}