using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Workflow.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Workflow.Commands.CreateWorkflow
{
    /// <summary>
    /// Handler for processing CreateWorkflowCommand requests.
    /// Implements the CQRS command handler pattern for creating workflow entities.
    /// </summary>
    public class CreateWorkflowCommandHandler : IRequestHandler<CreateWorkflowCommand, WorkflowDto>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateWorkflowCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateWorkflowCommandHandler"/> class.
        /// </summary>
        /// <param name="workflowRepository">The workflow repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public CreateWorkflowCommandHandler(
            IWorkflowRepository workflowRepository,
            IMapper mapper,
            ILogger<CreateWorkflowCommandHandler> logger)
        {
            _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateWorkflowCommand by creating a new workflow entity in the database.
        /// </summary>
        /// <param name="request">The CreateWorkflowCommand containing the workflow data to create.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A WorkflowDto representing the newly created workflow.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<WorkflowDto> Handle(CreateWorkflowCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Creating new workflow with ID: {WorkflowId}", request.Id);

                // Map the command to an entity
                var workflowEntity = _mapper.Map<Domain.Entities.BusinessEntities.Workflow>(request);

                // Set audit fields
                workflowEntity.CreatedDate = DateTime.UtcNow;
                workflowEntity.ModifiedDate = DateTime.UtcNow;
                workflowEntity.Active = true;

                // Save to database
                var createdWorkflow = await _workflowRepository.AddAsync(workflowEntity, cancellationToken);

                _logger.LogInformation("Successfully created workflow with ID: {WorkflowId}", createdWorkflow.WorkflowId);

                // Map the entity back to a DTO for the response
                return _mapper.Map<WorkflowDto>(createdWorkflow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating workflow with ID {WorkflowId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}