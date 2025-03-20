using FluentValidation;

namespace VibeCRM.Application.Features.Service.Commands.UpdateService
{
    /// <summary>
    /// Validator for the UpdateServiceCommand.
    /// Provides validation rules for service update operations.
    /// </summary>
    public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateServiceCommandValidator"/> class
        /// and defines the validation rules.
        /// </summary>
        public UpdateServiceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Service ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Service name is required")
                .MaximumLength(100).WithMessage("Service name must not exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Service description must not exceed 500 characters")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.ServiceTypeId)
                .NotEqual(Guid.Empty).WithMessage("ServiceTypeId is required");

            RuleFor(x => x.ModifiedBy)
                .NotEqual(Guid.Empty).WithMessage("ModifiedBy is required");
        }
    }
}