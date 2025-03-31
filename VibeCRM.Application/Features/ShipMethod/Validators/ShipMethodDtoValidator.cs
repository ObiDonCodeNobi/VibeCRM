using FluentValidation;
using VibeCRM.Shared.DTOs.ShipMethod;

namespace VibeCRM.Application.Features.ShipMethod.Validators
{
    /// <summary>
    /// Validator for the ShipMethodDto class.
    /// Defines validation rules for shipping method data.
    /// </summary>
    public class ShipMethodDtoValidator : AbstractValidator<ShipMethodDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShipMethodDtoValidator"/> class.
        /// Configures validation rules for ShipMethodDto properties.
        /// </summary>
        public ShipMethodDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Shipping method ID is required.");

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