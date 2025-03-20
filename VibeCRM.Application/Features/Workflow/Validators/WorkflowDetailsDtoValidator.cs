using FluentValidation;
using VibeCRM.Application.Features.Workflow.DTOs;

namespace VibeCRM.Application.Features.Workflow.Validators
{
    /// <summary>
    /// Validator for the WorkflowDetailsDto.
    /// Defines validation rules for detailed workflow data transfer objects.
    /// </summary>
    public class WorkflowDetailsDtoValidator : AbstractValidator<WorkflowDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowDetailsDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public WorkflowDetailsDtoValidator()
        {
            // Include all rules from the base validator
            Include(new WorkflowDtoValidator());

            // Add rules specific to WorkflowDetailsDto
            RuleFor(dto => dto.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required.");

            RuleFor(dto => dto.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(dto => dto.ModifiedBy)
                .NotEmpty().WithMessage("Modified by user ID is required.");

            RuleFor(dto => dto.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.");
        }
    }
}