using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrder.Commands.CreateSalesOrder
{
    /// <summary>
    /// Validator for the CreateSalesOrderCommand class
    /// </summary>
    public class CreateSalesOrderCommandValidator : AbstractValidator<CreateSalesOrderCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSalesOrderCommandValidator"/> class.
        /// </summary>
        public CreateSalesOrderCommandValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Sales order number is required")
                .MaximumLength(50).WithMessage("Sales order number cannot exceed 50 characters");

            RuleFor(x => x.SalesOrderStatusId)
                .NotEmpty().WithMessage("Sales order status ID is required");

            RuleFor(x => x.ShipMethodId)
                .NotEmpty().WithMessage("Ship method ID is required");

            RuleFor(x => x.BillToAddressId)
                .NotEmpty().WithMessage("Bill to address ID is required");

            RuleFor(x => x.ShipToAddressId)
                .NotEmpty().WithMessage("Ship to address ID is required");

            RuleFor(x => x.TaxCodeId)
                .NotEmpty().WithMessage("Tax code ID is required");

            RuleFor(x => x.OrderDate)
                .NotEmpty().WithMessage("Order date is required")
                .Must(date => date <= DateTime.UtcNow)
                .WithMessage("Order date cannot be in the future");

            RuleFor(x => x.SubTotal)
                .GreaterThanOrEqualTo(0).WithMessage("Subtotal must be greater than or equal to 0");

            RuleFor(x => x.TaxAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Tax amount must be greater than or equal to 0");

            RuleFor(x => x.DueAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Due amount must be greater than or equal to 0");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required");
        }
    }
}