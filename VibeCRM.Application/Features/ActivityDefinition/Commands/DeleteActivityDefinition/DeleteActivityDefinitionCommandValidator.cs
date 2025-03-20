using FluentValidation;

namespace VibeCRM.Application.Features.ActivityDefinition.Commands.DeleteActivityDefinition
{
    /// <summary>
    /// Validator for the DeleteActivityDefinitionCommand to ensure the command contains valid parameters.
    /// </summary>
    public class DeleteActivityDefinitionCommandValidator : AbstractValidator<DeleteActivityDefinitionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the DeleteActivityDefinitionCommandValidator class with validation rules.
        /// </summary>
        public DeleteActivityDefinitionCommandValidator()
        {
            RuleFor(c => c.ActivityDefinitionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");

            RuleFor(c => c.ModifiedBy)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}