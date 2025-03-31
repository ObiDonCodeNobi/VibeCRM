using FluentValidation;
using VibeCRM.Shared.DTOs.Workflow;

namespace VibeCRM.Application.Features.Workflow.Validators
{
    /// <summary>
    /// Validator for the WorkflowDto.
    /// Defines validation rules for workflow data transfer objects.
    /// </summary>
    public class WorkflowDtoValidator : AbstractValidator<WorkflowDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowDtoValidator"/> class
        /// and configures the validation rules.
        /// </summary>
        public WorkflowDtoValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage("Workflow ID is required.");

            RuleFor(dto => dto.WorkflowTypeId)
                .NotEmpty().WithMessage("Workflow type is required.");

            RuleFor(dto => dto.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(200).WithMessage("Subject must not exceed 200 characters.");

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(dto => dto.CompletedDate)
                .GreaterThanOrEqualTo(dto => dto.StartDate)
                .When(dto => dto.StartDate.HasValue && dto.CompletedDate.HasValue)
                .WithMessage("Completed date must be after or equal to the start date.");
        }
    }
}