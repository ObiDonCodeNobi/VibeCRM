using FluentValidation;

namespace VibeCRM.Application.Features.ServiceType.Commands.CreateServiceType
{
    /// <summary>
    /// Validator for the CreateServiceTypeCommand.
    /// Defines validation rules for creating a new service type.
    /// </summary>
    public class CreateServiceTypeCommandValidator : AbstractValidator<CreateServiceTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateServiceTypeCommandValidator"/> class.
        /// Configures validation rules for the CreateServiceTypeCommand properties.
        /// </summary>
        public CreateServiceTypeCommandValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Service type name is required.")
                .MaximumLength(50)
                .WithMessage("Service type name cannot exceed 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}
