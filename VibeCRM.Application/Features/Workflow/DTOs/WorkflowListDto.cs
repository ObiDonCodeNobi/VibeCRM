namespace VibeCRM.Application.Features.Workflow.DTOs
{
    /// <summary>
    /// List DTO for transferring workflow data in list views.
    /// Contains a subset of properties optimized for display in lists.
    /// </summary>
    public class WorkflowListDto : WorkflowDto
    {
        /// <summary>
        /// Gets or sets the workflow type name.
        /// </summary>
        public string WorkflowTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the workflow was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the workflow based on dates.
        /// </summary>
        public string Status
        {
            get
            {
                if (CompletedDate.HasValue)
                    return "Completed";
                if (StartDate.HasValue)
                    return "In Progress";
                return "Not Started";
            }
        }
    }
}