using System;
using FluentValidation;

namespace VibeCRM.Application.Features.ShipMethod.Commands.UpdateShipMethod
{
    /// <summary>
    /// Validator for the UpdateShipMethodCommand.
    /// Defines validation rules for updating an existing shipping method.
    /// </summary>
    public class UpdateShipMethodCommandValidator : AbstractValidator<UpdateShipMethodCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateShipMethodCommandValidator"/> class.
        /// Configures validation rules for the UpdateShipMethodCommand properties.
        /// </summary>
        public UpdateShipMethodCommandValidator()
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
