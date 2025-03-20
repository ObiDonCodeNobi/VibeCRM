using MediatR;
using VibeCRM.Application.Features.Workflow.DTOs;

namespace VibeCRM.Application.Features.Workflow.Commands.CreateWorkflow
{
    /// <summary>
    /// Command for creating a new workflow in the system.
    /// Implements the CQRS command pattern for workflow creation.
    /// </summary>
    public class CreateWorkflowCommand : IRequest<WorkflowDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the workflow.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

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
        /// Gets or sets the ID of the user who created the workflow.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who last modified the workflow.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}