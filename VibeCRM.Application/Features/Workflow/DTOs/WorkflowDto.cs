namespace VibeCRM.Application.Features.Workflow.DTOs
{
    /// <summary>
    /// Base DTO for transferring workflow data.
    /// Contains the core properties that represent a workflow.
    /// </summary>
    public class WorkflowDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the workflow.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the workflow type identifier.
        /// </summary>
        public Guid WorkflowTypeId { get; set; }

        /// <summary>
        /// Gets or sets the subject of the workflow.
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the workflow.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the start date of the workflow.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the completed date of the workflow.
        /// </summary>
        public DateTime? CompletedDate { get; set; }
    }
}