using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Workflow.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Workflow.Commands.UpdateWorkflow
{
    /// <summary>
    /// Handler for processing UpdateWorkflowCommand requests.
    /// Implements the CQRS command handler pattern for updating workflow entities.
    /// </summary>
    public class UpdateWorkflowCommandHandler : IRequestHandler<UpdateWorkflowCommand, WorkflowDto>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateWorkflowCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateWorkflowCommandHandler"/> class.
        /// </summary>
        /// <param name="workflowRepository">The workflow repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public UpdateWorkflowCommandHandler(
            IWorkflowRepository workflowRepository,
            IMapper mapper,
            ILogger<UpdateWorkflowCommandHandler> logger)
        {
            _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateWorkflowCommand by updating an existing workflow entity in the database.
        /// </summary>
        /// <param name="request">The UpdateWorkflowCommand containing the workflow data to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A WorkflowDto representing the updated workflow.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the workflow ID is empty.</exception>
        public async Task<WorkflowDto> Handle(UpdateWorkflowCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Workflow ID cannot be empty", nameof(request.Id));

            try
            {
                _logger.LogInformation("Updating workflow with ID: {WorkflowId}", request.Id);

                // Get the existing workflow
                var existingWorkflow = await _workflowRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingWorkflow == null)
                {
                    _logger.LogWarning("Workflow with ID {WorkflowId} not found or is inactive", request.Id);
                    throw new InvalidOperationException($"Workflow with ID {request.Id} not found or is inactive");
                }

                // Map the updated properties
                _mapper.Map(request, existingWorkflow);

                // Update audit fields
                existingWorkflow.ModifiedDate = DateTime.UtcNow;
                existingWorkflow.ModifiedBy = request.ModifiedBy;

                // Save to database
                var updatedWorkflow = await _workflowRepository.UpdateAsync(existingWorkflow, cancellationToken);

                _logger.LogInformation("Successfully updated workflow with ID: {WorkflowId}", updatedWorkflow.WorkflowId);

                // Map the entity back to a DTO for the response
                return _mapper.Map<WorkflowDto>(updatedWorkflow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating workflow with ID {WorkflowId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}