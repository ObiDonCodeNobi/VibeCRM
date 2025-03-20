using FluentValidation;
using VibeCRM.Application.Features.QuoteLineItem.DTOs;

namespace VibeCRM.Application.Features.Quote.Validators
{
    /// <summary>
    /// Validator for the QuoteLineItemDto class
    /// </summary>
    public class QuoteLineItemDtoValidator : AbstractValidator<QuoteLineItemDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteLineItemDtoValidator"/> class.
        /// </summary>
        public QuoteLineItemDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Quote line item ID is required.");

            RuleFor(x => x.QuoteId)
                .NotEmpty().WithMessage("Quote ID is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than or equal to zero.");

            RuleFor(x => x.DiscountPercentage)
                .InclusiveBetween(0, 100).WithMessage("Discount percentage must be between 0 and 100.")
                .When(x => x.DiscountPercentage.HasValue);

            RuleFor(x => x.DiscountAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount amount must be greater than or equal to zero.")
                .When(x => x.DiscountAmount.HasValue);

            RuleFor(x => x.TaxPercentage)
                .InclusiveBetween(0, 100).WithMessage("Tax percentage must be between 0 and 100.")
                .When(x => x.TaxPercentage.HasValue);

            RuleFor(x => x.LineNumber)
                .GreaterThan(0).WithMessage("Line number must be greater than zero.");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters.")
                .When(x => !string.IsNullOrEmpty(x.Notes));

            // Validate that at least one of ProductId or ServiceId is provided
            RuleFor(x => new { x.ProductId, x.ServiceId })
                .Must(x => x.ProductId.HasValue || x.ServiceId.HasValue)
                .WithMessage("Either Product ID or Service ID must be provided.");

            // Product name validation
            RuleFor(x => x.ProductName)
                .MaximumLength(200).WithMessage("Product name cannot exceed 200 characters.")
                .When(x => !string.IsNullOrEmpty(x.ProductName));

            // Service name validation
            RuleFor(x => x.ServiceName)
                .MaximumLength(200).WithMessage("Service name cannot exceed 200 characters.")
                .When(x => !string.IsNullOrEmpty(x.ServiceName));
        }
    }
}
