using FluentValidation;

namespace VibeCRM.Application.Features.ShipMethod.Commands.CreateShipMethod
{
    /// <summary>
    /// Validator for the CreateShipMethodCommand.
    /// Defines validation rules for creating a new shipping method.
    /// </summary>
    public class CreateShipMethodCommandValidator : AbstractValidator<CreateShipMethodCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateShipMethodCommandValidator"/> class.
        /// Configures validation rules for the CreateShipMethodCommand properties.
        /// </summary>
        public CreateShipMethodCommandValidator()
        {
            RuleFor(x => x.Method)
                .NotEmpty()
                .WithMessage("Shipping method name is required.")
                .MaximumLength(50)
                .WithMessage("Shipping method name cannot exceed 50 characters.");

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
