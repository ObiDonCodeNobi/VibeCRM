using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a business workflow in the system.
    /// </summary>
    public class Workflow : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Workflow"/> class.
        /// </summary>
        public Workflow()
        {
            Id = Guid.NewGuid();
            Subject = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the workflow identifier that directly maps to the WorkflowId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid WorkflowId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the unique identifier of the workflow type.
        /// </summary>
        public Guid WorkflowTypeId { get; set; }

        /// <summary>
        /// Gets or sets the subject or title of the workflow.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the detailed description of the workflow.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the date when the workflow was started.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the workflow was completed.
        /// </summary>
        public DateTime? CompletedDate { get; set; }
    }
}