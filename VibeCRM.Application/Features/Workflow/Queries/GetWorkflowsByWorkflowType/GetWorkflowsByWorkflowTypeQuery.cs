using MediatR;
using VibeCRM.Shared.DTOs.Workflow;

namespace VibeCRM.Application.Features.Workflow.Queries.GetWorkflowsByWorkflowType
{
    /// <summary>
    /// Query for retrieving workflows by their workflow type.
    /// Implements the CQRS query pattern for filtered workflow retrieval.
    /// </summary>
    public class GetWorkflowsByWorkflowTypeQuery : IRequest<IEnumerable<WorkflowListDto>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetWorkflowsByWorkflowTypeQuery"/> class.
        /// </summary>
        public GetWorkflowsByWorkflowTypeQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetWorkflowsByWorkflowTypeQuery"/> class with a specified workflow type ID.
        /// </summary>
        /// <param name="workflowTypeId">The workflow type ID to filter by.</param>
        public GetWorkflowsByWorkflowTypeQuery(Guid workflowTypeId)
        {
            WorkflowTypeId = workflowTypeId;
        }

        /// <summary>
        /// Gets or sets the workflow type ID to filter by.
        /// </summary>
        public Guid WorkflowTypeId { get; set; }
    }
}