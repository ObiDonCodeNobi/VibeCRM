using System;
using FluentValidation;
using VibeCRM.Application.Features.ShipMethod.DTOs;

namespace VibeCRM.Application.Features.ShipMethod.Validators
{
    /// <summary>
    /// Validator for the ShipMethodDetailsDto class.
    /// Defines validation rules for detailed shipping method data.
    /// </summary>
    public class ShipMethodDetailsDtoValidator : AbstractValidator<ShipMethodDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShipMethodDetailsDtoValidator"/> class.
        /// Configures validation rules for ShipMethodDetailsDto properties.
        /// </summary>
        public ShipMethodDetailsDtoValidator()
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

            RuleFor(x => x.CreatedDate)
                .NotEmpty()
                .WithMessage("Created date is required.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithMessage("Created by user ID is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty()
                .WithMessage("Modified date is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty()
                .WithMessage("Modified by user ID is required.");
        }
    }
}
