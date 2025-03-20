namespace VibeCRM.Application.Features.Workflow.DTOs
{
    /// <summary>
    /// Detailed DTO for transferring comprehensive workflow data.
    /// Extends the base WorkflowDto with additional properties.
    /// </summary>
    public class WorkflowDetailsDto : WorkflowDto
    {
        /// <summary>
        /// Gets or sets the workflow type name.
        /// </summary>
        public string WorkflowTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ID of the user who created the workflow.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the workflow was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who last modified the workflow.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the workflow was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the workflow is active.
        /// </summary>
        public bool Active { get; set; }
    }
}