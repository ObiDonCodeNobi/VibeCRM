using FluentValidation;

namespace VibeCRM.Application.Features.Workflow.Queries.GetAllWorkflows
{
    /// <summary>
    /// Validator for the GetAllWorkflowsQuery.
    /// Defines validation rules for retrieving all workflows.
    /// </summary>
    public class GetAllWorkflowsQueryValidator : AbstractValidator<GetAllWorkflowsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllWorkflowsQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetAllWorkflowsQueryValidator()
        {
            // No specific validation rules needed for this query
            // as it doesn't have any parameters
        }
    }
}