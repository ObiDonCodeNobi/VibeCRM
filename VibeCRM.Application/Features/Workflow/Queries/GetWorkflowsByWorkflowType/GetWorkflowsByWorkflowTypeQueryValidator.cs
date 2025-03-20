using FluentValidation;

namespace VibeCRM.Application.Features.Workflow.Queries.GetWorkflowsByWorkflowType
{
    /// <summary>
    /// Validator for the GetWorkflowsByWorkflowTypeQuery.
    /// Defines validation rules for retrieving workflows by workflow type.
    /// </summary>
    public class GetWorkflowsByWorkflowTypeQueryValidator : AbstractValidator<GetWorkflowsByWorkflowTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetWorkflowsByWorkflowTypeQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetWorkflowsByWorkflowTypeQueryValidator()
        {
            RuleFor(q => q.WorkflowTypeId)
                .NotEmpty().WithMessage("Workflow Type ID is required.");
        }
    }
}