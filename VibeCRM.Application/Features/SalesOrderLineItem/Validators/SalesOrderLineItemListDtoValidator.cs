using FluentValidation;
using VibeCRM.Application.Features.SalesOrderLineItem.DTOs;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Validators
{
    /// <summary>
    /// Validator for the SalesOrderLineItemListDto
    /// </summary>
    public class SalesOrderLineItemListDtoValidator : AbstractValidator<SalesOrderLineItemListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderLineItemListDtoValidator"/> class
        /// </summary>
        public SalesOrderLineItemListDtoValidator()
        {
            // Id is required
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sales Order Line Item ID is required");

            // SalesOrderId is required
            RuleFor(x => x.SalesOrderId)
                .NotEmpty()
                .WithMessage("Sales Order ID is required");

            // Either ProductId or ServiceId must be provided
            RuleFor(x => x)
                .Must(x => x.ProductId.HasValue || x.ServiceId.HasValue)
                .WithMessage("Either Product ID or Service ID must be provided");

            // Description is required
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters");

            // Quantity must be positive
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero");

            // UnitPrice must be non-negative
            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Unit Price must be greater than or equal to zero");

            // ExtendedPrice must be non-negative
            RuleFor(x => x.ExtendedPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Extended Price must be greater than or equal to zero");
        }
    }
}