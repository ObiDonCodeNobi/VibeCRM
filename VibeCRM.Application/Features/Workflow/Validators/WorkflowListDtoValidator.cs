using FluentValidation;
using VibeCRM.Shared.DTOs.Workflow;

namespace VibeCRM.Application.Features.Workflow.Validators
{
    /// <summary>
    /// Validator for the WorkflowListDto.
    /// Defines validation rules for workflow list data transfer objects.
    /// </summary>
    public class WorkflowListDtoValidator : AbstractValidator<WorkflowListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowListDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public WorkflowListDtoValidator()
        {
            // Include all rules from the base validator
            Include(new WorkflowDtoValidator());

            // Add rules specific to WorkflowListDto
            RuleFor(dto => dto.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(dto => dto.WorkflowTypeName)
                .MaximumLength(100).WithMessage("Workflow type name must not exceed 100 characters.")
                .When(dto => !string.IsNullOrEmpty(dto.WorkflowTypeName));
        }
    }
}