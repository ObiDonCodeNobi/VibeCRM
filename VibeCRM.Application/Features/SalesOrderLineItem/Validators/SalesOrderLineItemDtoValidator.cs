using FluentValidation;
using VibeCRM.Application.Features.SalesOrderLineItem.DTOs;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Validators
{
    /// <summary>
    /// Validator for the SalesOrderLineItemDto
    /// </summary>
    public class SalesOrderLineItemDtoValidator : AbstractValidator<SalesOrderLineItemDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderLineItemDtoValidator"/> class
        /// </summary>
        public SalesOrderLineItemDtoValidator()
        {
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

            // DiscountPercentage must be between 0 and 100 if provided
            When(x => x.DiscountPercentage.HasValue, () =>
            {
                RuleFor(x => x.DiscountPercentage ?? 0)
                    .InclusiveBetween(0, 100)
                    .WithMessage("Discount Percentage must be between 0 and 100");
            });

            // DiscountAmount must be non-negative if provided
            When(x => x.DiscountAmount.HasValue, () =>
            {
                RuleFor(x => x.DiscountAmount ?? 0)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Discount Amount must be greater than or equal to zero");
            });

            // TaxRate must be non-negative if provided and item is taxable
            When(x => x.IsTaxable && x.TaxRate.HasValue, () =>
            {
                RuleFor(x => x.TaxRate ?? 0)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Tax Rate must be greater than or equal to zero");
            });

            // Notes cannot exceed 1000 characters if provided
            When(x => !string.IsNullOrEmpty(x.Notes), () =>
            {
                RuleFor(x => x.Notes)
                    .MaximumLength(1000)
                    .WithMessage("Notes cannot exceed 1000 characters");
            });
        }
    }
}