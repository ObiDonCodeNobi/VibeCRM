using MediatR;
using VibeCRM.Application.Features.Workflow.DTOs;

namespace VibeCRM.Application.Features.Workflow.Queries.GetWorkflowById
{
    /// <summary>
    /// Query for retrieving a specific workflow by its unique identifier.
    /// Implements the CQRS query pattern for workflow retrieval.
    /// </summary>
    public class GetWorkflowByIdQuery : IRequest<WorkflowDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetWorkflowByIdQuery"/> class.
        /// </summary>
        public GetWorkflowByIdQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetWorkflowByIdQuery"/> class with a specified workflow ID.
        /// </summary>
        /// <param name="id">The unique identifier of the workflow to retrieve.</param>
        public GetWorkflowByIdQuery(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the workflow to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}