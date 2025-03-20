using FluentValidation;

namespace VibeCRM.Application.Features.ServiceType.Commands.UpdateServiceType
{
    /// <summary>
    /// Validator for the UpdateServiceTypeCommand.
    /// Defines validation rules for updating an existing service type.
    /// </summary>
    public class UpdateServiceTypeCommandValidator : AbstractValidator<UpdateServiceTypeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateServiceTypeCommandValidator"/> class.
        /// Configures validation rules for the UpdateServiceTypeCommand properties.
        /// </summary>
        public UpdateServiceTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Service type ID is required.");

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