using FluentValidation;
using VibeCRM.Shared.DTOs.ShipMethod;

namespace VibeCRM.Application.Features.ShipMethod.Validators
{
    /// <summary>
    /// Validator for the ShipMethodListDto class.
    /// Defines validation rules for shipping method list data.
    /// </summary>
    public class ShipMethodListDtoValidator : AbstractValidator<ShipMethodListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShipMethodListDtoValidator"/> class.
        /// Configures validation rules for ShipMethodListDto properties.
        /// </summary>
        public ShipMethodListDtoValidator()
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

            RuleFor(x => x.OrderCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Order count must be a non-negative number.");
        }
    }
}