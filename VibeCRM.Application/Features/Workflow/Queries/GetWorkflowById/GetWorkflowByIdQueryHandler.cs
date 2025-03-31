using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.Workflow;

namespace VibeCRM.Application.Features.Workflow.Queries.GetWorkflowById
{
    /// <summary>
    /// Handler for processing GetWorkflowByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific workflow.
    /// </summary>
    public class GetWorkflowByIdQueryHandler : IRequestHandler<GetWorkflowByIdQuery, WorkflowDetailsDto>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IWorkflowTypeRepository _workflowTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetWorkflowByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetWorkflowByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="workflowRepository">The workflow repository for database operations.</param>
        /// <param name="workflowTypeRepository">The workflow type repository for retrieving workflow type information.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetWorkflowByIdQueryHandler(
            IWorkflowRepository workflowRepository,
            IWorkflowTypeRepository workflowTypeRepository,
            IMapper mapper,
            ILogger<GetWorkflowByIdQueryHandler> logger)
        {
            _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
            _workflowTypeRepository = workflowTypeRepository ?? throw new ArgumentNullException(nameof(workflowTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetWorkflowByIdQuery by retrieving a specific workflow entity from the database.
        /// </summary>
        /// <param name="request">The GetWorkflowByIdQuery containing the workflow ID to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A WorkflowDetailsDto containing the requested workflow's data, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the workflow ID is empty.</exception>
        public async Task<WorkflowDetailsDto> Handle(GetWorkflowByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Workflow ID cannot be empty", nameof(request.Id));

            try
            {
                _logger.LogInformation("Retrieving workflow with ID: {WorkflowId}", request.Id);

                // Get the workflow from the repository (Active=1 filter is applied in the repository)
                var workflow = await _workflowRepository.GetByIdAsync(request.Id, cancellationToken);

                if (workflow == null)
                {
                    _logger.LogWarning("Workflow with ID {WorkflowId} not found or is inactive", request.Id);
                    return new WorkflowDetailsDto();
                }

                // Map the entity to a DTO
                var workflowDto = _mapper.Map<WorkflowDetailsDto>(workflow);

                // Get and map the workflow type information
                if (workflow.WorkflowTypeId != Guid.Empty)
                {
                    var workflowType = await _workflowTypeRepository.GetByIdAsync(workflow.WorkflowTypeId, cancellationToken);
                    if (workflowType != null)
                    {
                        workflowDto.WorkflowTypeName = workflowType.Type;
                    }
                }

                _logger.LogInformation("Successfully retrieved workflow with ID: {WorkflowId}", request.Id);

                return workflowDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving workflow with ID {WorkflowId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}