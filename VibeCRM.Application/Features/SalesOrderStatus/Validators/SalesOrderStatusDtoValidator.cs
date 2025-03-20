using FluentValidation;
using VibeCRM.Application.Features.SalesOrderStatus.DTOs;

namespace VibeCRM.Application.Features.SalesOrderStatus.Validators
{
    /// <summary>
    /// Validator for the SalesOrderStatusDto class.
    /// Defines validation rules for sales order status data.
    /// </summary>
    public class SalesOrderStatusDtoValidator : AbstractValidator<SalesOrderStatusDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderStatusDtoValidator"/> class.
        /// Configures validation rules for SalesOrderStatusDto properties.
        /// </summary>
        public SalesOrderStatusDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sales order status ID is required.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status name is required.")
                .MaximumLength(50)
                .WithMessage("Status name cannot exceed 50 characters.");

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