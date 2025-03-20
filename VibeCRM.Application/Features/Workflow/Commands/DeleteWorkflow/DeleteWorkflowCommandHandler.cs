using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Workflow.Commands.DeleteWorkflow
{
    /// <summary>
    /// Handler for processing DeleteWorkflowCommand requests.
    /// Implements the CQRS command handler pattern for deleting workflow entities.
    /// </summary>
    public class DeleteWorkflowCommandHandler : IRequestHandler<DeleteWorkflowCommand, bool>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly ILogger<DeleteWorkflowCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteWorkflowCommandHandler"/> class.
        /// </summary>
        /// <param name="workflowRepository">The workflow repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public DeleteWorkflowCommandHandler(
            IWorkflowRepository workflowRepository,
            ILogger<DeleteWorkflowCommandHandler> logger)
        {
            _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteWorkflowCommand by soft-deleting a workflow entity in the database.
        /// </summary>
        /// <param name="request">The DeleteWorkflowCommand containing the workflow ID to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the workflow ID is empty.</exception>
        public async Task<bool> Handle(DeleteWorkflowCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Workflow ID cannot be empty", nameof(request.Id));

            try
            {
                _logger.LogInformation("Deleting workflow with ID: {WorkflowId}", request.Id);

                // Get the existing workflow
                var existingWorkflow = await _workflowRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingWorkflow == null)
                {
                    _logger.LogWarning("Workflow with ID {WorkflowId} not found or is already inactive", request.Id);
                    return false;
                }

                // Update the modified by field before deletion
                existingWorkflow.ModifiedBy = request.ModifiedBy;
                existingWorkflow.ModifiedDate = DateTime.UtcNow;

                // Perform soft delete by setting Active = 0
                existingWorkflow.Active = false;
                var result = await _workflowRepository.UpdateAsync(existingWorkflow, cancellationToken);

                if (result != null)
                {
                    _logger.LogInformation("Successfully deleted workflow with ID: {WorkflowId}", request.Id);
                    return true;
                }
                else
                {
                    _logger.LogWarning("Failed to delete workflow with ID: {WorkflowId}", request.Id);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting workflow with ID {WorkflowId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}