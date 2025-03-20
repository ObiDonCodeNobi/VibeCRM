using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Workflow.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.Workflow.Queries.GetAllWorkflows
{
    /// <summary>
    /// Handler for processing GetAllWorkflowsQuery requests.
    /// Implements the CQRS query handler pattern for retrieving all workflows.
    /// </summary>
    public class GetAllWorkflowsQueryHandler : IRequestHandler<GetAllWorkflowsQuery, IEnumerable<WorkflowListDto>>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IWorkflowTypeRepository _workflowTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllWorkflowsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllWorkflowsQueryHandler"/> class.
        /// </summary>
        /// <param name="workflowRepository">The workflow repository for database operations.</param>
        /// <param name="workflowTypeRepository">The workflow type repository for retrieving workflow type information.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetAllWorkflowsQueryHandler(
            IWorkflowRepository workflowRepository,
            IWorkflowTypeRepository workflowTypeRepository,
            IMapper mapper,
            ILogger<GetAllWorkflowsQueryHandler> logger)
        {
            _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
            _workflowTypeRepository = workflowTypeRepository ?? throw new ArgumentNullException(nameof(workflowTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllWorkflowsQuery by retrieving all active workflow entities from the database.
        /// </summary>
        /// <param name="request">The GetAllWorkflowsQuery request.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of WorkflowListDto objects representing all active workflows.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<WorkflowListDto>> Handle(GetAllWorkflowsQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving all active workflows");

                // Get all workflows from the repository (Active=1 filter is applied in the repository)
                var workflows = await _workflowRepository.GetAllAsync(cancellationToken);

                // Map the entities to DTOs
                var workflowDtos = _mapper.Map<IEnumerable<WorkflowListDto>>(workflows);
                var workflowList = workflowDtos.ToList();

                // Get all workflow types to populate the type names
                var workflowTypeIds = workflows.Select(w => w.WorkflowTypeId).Distinct().ToList();
                var workflowTypes = new Dictionary<Guid, string>();

                foreach (var typeId in workflowTypeIds)
                {
                    if (typeId != Guid.Empty)
                    {
                        var workflowType = await _workflowTypeRepository.GetByIdAsync(typeId, cancellationToken);
                        if (workflowType != null)
                        {
                            workflowTypes[typeId] = workflowType.Type;
                        }
                    }
                }

                // Populate the workflow type names in the DTOs
                foreach (var dto in workflowList)
                {
                    var workflow = workflows.FirstOrDefault(w => w.WorkflowId == dto.Id);
                    if (workflow != null && workflowTypes.ContainsKey(workflow.WorkflowTypeId))
                    {
                        dto.WorkflowTypeName = workflowTypes[workflow.WorkflowTypeId];
                    }
                }

                _logger.LogInformation("Successfully retrieved {Count} active workflows", workflowList.Count);

                return workflowList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all workflows: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}