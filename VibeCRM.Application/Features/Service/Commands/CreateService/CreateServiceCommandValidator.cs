using FluentValidation;

namespace VibeCRM.Application.Features.Service.Commands.CreateService
{
    /// <summary>
    /// Validator for the CreateServiceCommand.
    /// Provides validation rules for service creation operations.
    /// </summary>
    public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateServiceCommandValidator"/> class
        /// and defines the validation rules.
        /// </summary>
        public CreateServiceCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Service name is required")
                .MaximumLength(100).WithMessage("Service name must not exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Service description must not exceed 500 characters")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.ServiceTypeId)
                .NotEqual(Guid.Empty).WithMessage("ServiceTypeId is required");

            RuleFor(x => x.CreatedBy)
                .NotEqual(Guid.Empty).WithMessage("CreatedBy is required");

            RuleFor(x => x.ModifiedBy)
                .NotEqual(Guid.Empty).WithMessage("ModifiedBy is required");
        }
    }
}