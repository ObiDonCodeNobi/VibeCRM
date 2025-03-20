using FluentValidation;

namespace VibeCRM.Application.Features.Workflow.Commands.DeleteWorkflow
{
    /// <summary>
    /// Validator for the DeleteWorkflowCommand.
    /// Defines validation rules for workflow deletion commands.
    /// </summary>
    public class DeleteWorkflowCommandValidator : AbstractValidator<DeleteWorkflowCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteWorkflowCommandValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public DeleteWorkflowCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Workflow ID is required.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");
        }
    }
}