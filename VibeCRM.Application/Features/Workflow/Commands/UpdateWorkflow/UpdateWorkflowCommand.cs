using MediatR;
using VibeCRM.Application.Features.Workflow.DTOs;

namespace VibeCRM.Application.Features.Workflow.Commands.UpdateWorkflow
{
    /// <summary>
    /// Command for updating an existing workflow in the system.
    /// Implements the CQRS command pattern for workflow updates.
    /// </summary>
    public class UpdateWorkflowCommand : IRequest<WorkflowDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the workflow to update.
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

        /// <summary>
        /// Gets or sets the ID of the user who last modified the workflow.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}