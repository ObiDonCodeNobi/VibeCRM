using FluentValidation;
using VibeCRM.Application.Features.SalesOrder.DTOs;

namespace VibeCRM.Application.Features.SalesOrder.Validators
{
    /// <summary>
    /// Validator for the SalesOrderListDto class
    /// </summary>
    public class SalesOrderListDtoValidator : AbstractValidator<SalesOrderListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderListDtoValidator"/> class.
        /// </summary>
        public SalesOrderListDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sales order ID is required");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Sales order number is required")
                .MaximumLength(50).WithMessage("Sales order number cannot exceed 50 characters");

            RuleFor(x => x.SalesOrderStatusName)
                .NotEmpty().WithMessage("Sales order status name is required");

            RuleFor(x => x.OrderDate)
                .NotEmpty().WithMessage("Order date is required");

            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Total amount must be greater than or equal to 0");

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Created date is required");

            RuleFor(x => x.CreatedByName)
                .NotEmpty().WithMessage("Created by name is required");
        }
    }
}