using FluentValidation;

namespace VibeCRM.Application.Features.QuoteLineItem.Commands.UpdateQuoteLineItem
{
    /// <summary>
    /// Validator for the UpdateQuoteLineItemCommand
    /// </summary>
    public class UpdateQuoteLineItemCommandValidator : AbstractValidator<UpdateQuoteLineItemCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateQuoteLineItemCommandValidator"/> class
        /// </summary>
        public UpdateQuoteLineItemCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Quote line item ID is required");

            RuleFor(x => x.QuoteId)
                .NotEmpty().WithMessage("Quote ID is required");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than or equal to zero");

            RuleFor(x => x.DiscountPercentage)
                .GreaterThanOrEqualTo(0).When(x => x.DiscountPercentage.HasValue)
                .WithMessage("Discount percentage must be greater than or equal to zero")
                .LessThanOrEqualTo(100).When(x => x.DiscountPercentage.HasValue)
                .WithMessage("Discount percentage cannot exceed 100%");

            RuleFor(x => x.DiscountAmount)
                .GreaterThanOrEqualTo(0).When(x => x.DiscountAmount.HasValue)
                .WithMessage("Discount amount must be greater than or equal to zero");

            RuleFor(x => x.TaxPercentage)
                .GreaterThanOrEqualTo(0).When(x => x.TaxPercentage.HasValue)
                .WithMessage("Tax percentage must be greater than or equal to zero");

            RuleFor(x => x.LineNumber)
                .GreaterThan(0).WithMessage("Line number must be greater than zero");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modifier ID is required");

            // Ensure at least one of ProductId or ServiceId is provided
            RuleFor(x => new { x.ProductId, x.ServiceId })
                .Must(x => x.ProductId.HasValue || x.ServiceId.HasValue)
                .WithMessage("Either Product ID or Service ID must be provided");

            // Ensure that both discount types are not provided simultaneously
            RuleFor(x => new { x.DiscountPercentage, x.DiscountAmount })
                .Must(x => !x.DiscountPercentage.HasValue || !x.DiscountAmount.HasValue ||
                           (x.DiscountPercentage.Value == 0 || x.DiscountAmount.Value == 0))
                .WithMessage("Cannot apply both discount percentage and discount amount simultaneously");
        }
    }
}