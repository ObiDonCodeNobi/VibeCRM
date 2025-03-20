using MediatR;

namespace VibeCRM.Application.Features.Workflow.Commands.DeleteWorkflow
{
    /// <summary>
    /// Command for deleting a workflow from the system.
    /// Implements the CQRS command pattern for workflow deletion.
    /// </summary>
    public class DeleteWorkflowCommand : IRequest<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteWorkflowCommand"/> class.
        /// </summary>
        public DeleteWorkflowCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteWorkflowCommand"/> class with a specified workflow ID.
        /// </summary>
        /// <param name="id">The unique identifier of the workflow to delete.</param>
        /// <param name="modifiedBy">The ID of the user performing the deletion.</param>
        public DeleteWorkflowCommand(Guid id, Guid modifiedBy)
        {
            Id = id;
            ModifiedBy = modifiedBy;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the workflow to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who is performing the deletion.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}