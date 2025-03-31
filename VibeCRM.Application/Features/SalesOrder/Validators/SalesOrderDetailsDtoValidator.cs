using FluentValidation;
using VibeCRM.Shared.DTOs.SalesOrder;

namespace VibeCRM.Application.Features.SalesOrder.Validators
{
    /// <summary>
    /// Validator for the SalesOrderDetailsDto class
    /// </summary>
    public class SalesOrderDetailsDtoValidator : AbstractValidator<SalesOrderDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderDetailsDtoValidator"/> class.
        /// </summary>
        public SalesOrderDetailsDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sales order ID is required");

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
                .NotEmpty().WithMessage("Order date is required");

            RuleFor(x => x.SubTotal)
                .GreaterThanOrEqualTo(0).WithMessage("Subtotal must be greater than or equal to 0");

            RuleFor(x => x.TaxAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Tax amount must be greater than or equal to 0");

            RuleFor(x => x.DueAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Due amount must be greater than or equal to 0");

            RuleFor(x => x.SalesOrderStatusName)
                .NotEmpty().WithMessage("Sales order status name is required");

            RuleFor(x => x.ShipMethodName)
                .NotEmpty().WithMessage("Ship method name is required");

            RuleFor(x => x.TaxCodeName)
                .NotEmpty().WithMessage("Tax code name is required");

            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Total amount must be greater than or equal to 0");
        }
    }
}