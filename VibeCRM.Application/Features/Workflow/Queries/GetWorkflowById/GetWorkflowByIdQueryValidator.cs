using FluentValidation;

namespace VibeCRM.Application.Features.Workflow.Queries.GetWorkflowById
{
    /// <summary>
    /// Validator for the GetWorkflowByIdQuery.
    /// Defines validation rules for workflow retrieval by ID queries.
    /// </summary>
    public class GetWorkflowByIdQueryValidator : AbstractValidator<GetWorkflowByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetWorkflowByIdQueryValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public GetWorkflowByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("Workflow ID is required.");
        }
    }
}