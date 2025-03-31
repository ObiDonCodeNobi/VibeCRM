using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.Workflow;

namespace VibeCRM.Application.Features.Workflow.Queries.GetWorkflowsByWorkflowType
{
    /// <summary>
    /// Handler for processing GetWorkflowsByWorkflowTypeQuery requests.
    /// Implements the CQRS query handler pattern for retrieving workflows by workflow type.
    /// </summary>
    public class GetWorkflowsByWorkflowTypeQueryHandler : IRequestHandler<GetWorkflowsByWorkflowTypeQuery, IEnumerable<WorkflowListDto>>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IWorkflowTypeRepository _workflowTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetWorkflowsByWorkflowTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetWorkflowsByWorkflowTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="workflowRepository">The workflow repository for database operations.</param>
        /// <param name="workflowTypeRepository">The workflow type repository for retrieving workflow type information.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetWorkflowsByWorkflowTypeQueryHandler(
            IWorkflowRepository workflowRepository,
            IWorkflowTypeRepository workflowTypeRepository,
            IMapper mapper,
            ILogger<GetWorkflowsByWorkflowTypeQueryHandler> logger)
        {
            _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
            _workflowTypeRepository = workflowTypeRepository ?? throw new ArgumentNullException(nameof(workflowTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetWorkflowsByWorkflowTypeQuery by retrieving workflow entities filtered by workflow type.
        /// </summary>
        /// <param name="request">The GetWorkflowsByWorkflowTypeQuery containing the workflow type ID to filter by.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of WorkflowListDto objects representing workflows of the specified type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the workflow type ID is empty.</exception>
        public async Task<IEnumerable<WorkflowListDto>> Handle(GetWorkflowsByWorkflowTypeQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.WorkflowTypeId == Guid.Empty) throw new ArgumentException("Workflow Type ID cannot be empty", nameof(request.WorkflowTypeId));

            try
            {
                _logger.LogInformation("Retrieving workflows by workflow type ID: {WorkflowTypeId}", request.WorkflowTypeId);

                // Get the workflow type to verify it exists and get its name
                var workflowType = await _workflowTypeRepository.GetByIdAsync(request.WorkflowTypeId, cancellationToken);
                if (workflowType == null)
                {
                    _logger.LogWarning("Workflow type with ID {WorkflowTypeId} not found", request.WorkflowTypeId);
                    return new List<WorkflowListDto>();
                }

                // Get workflows by workflow type from the repository (Active=1 filter is applied in the repository)
                var workflows = await _workflowRepository.GetByWorkflowTypeAsync(request.WorkflowTypeId, cancellationToken);

                // Map the entities to DTOs
                var workflowDtos = _mapper.Map<IEnumerable<WorkflowListDto>>(workflows);
                var workflowList = workflowDtos.ToList();

                // Set the workflow type name for all DTOs
                foreach (var dto in workflowList)
                {
                    dto.WorkflowTypeName = workflowType.Type;
                }

                _logger.LogInformation("Successfully retrieved {Count} workflows for workflow type ID: {WorkflowTypeId}",
                    workflowList.Count, request.WorkflowTypeId);

                return workflowList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving workflows for workflow type ID {WorkflowTypeId}: {ErrorMessage}",
                    request.WorkflowTypeId, ex.Message);
                throw;
            }
        }
    }
}