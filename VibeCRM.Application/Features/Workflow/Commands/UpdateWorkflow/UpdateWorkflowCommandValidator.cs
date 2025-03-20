using FluentValidation;

namespace VibeCRM.Application.Features.Workflow.Commands.UpdateWorkflow
{
    /// <summary>
    /// Validator for the UpdateWorkflowCommand.
    /// Defines validation rules for workflow update commands.
    /// </summary>
    public class UpdateWorkflowCommandValidator : AbstractValidator<UpdateWorkflowCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateWorkflowCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public UpdateWorkflowCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Workflow ID is required.");

            RuleFor(c => c.WorkflowTypeId)
                .NotEmpty().WithMessage("Workflow type is required.");

            RuleFor(c => c.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(200).WithMessage("Subject must not exceed 200 characters.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(c => c.CompletedDate)
                .GreaterThanOrEqualTo(c => c.StartDate)
                .When(c => c.StartDate.HasValue && c.CompletedDate.HasValue)
                .WithMessage("Completed date must be after or equal to the start date.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}