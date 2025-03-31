using MediatR;
using VibeCRM.Shared.DTOs.Workflow;

namespace VibeCRM.Application.Features.Workflow.Queries.GetAllWorkflows
{
    /// <summary>
    /// Query for retrieving all active workflows in the system.
    /// Implements the CQRS query pattern for workflow collection retrieval.
    /// </summary>
    public class GetAllWorkflowsQuery : IRequest<IEnumerable<WorkflowListDto>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllWorkflowsQuery"/> class.
        /// </summary>
        public GetAllWorkflowsQuery()
        {
        }
    }
}