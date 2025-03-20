using FluentValidation;
using VibeCRM.Application.Features.QuoteLineItem.DTOs;

namespace VibeCRM.Application.Features.QuoteLineItem.Validators
{
    /// <summary>
    /// Validator for the QuoteLineItemDto
    /// </summary>
    public class QuoteLineItemDtoValidator : AbstractValidator<QuoteLineItemDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteLineItemDtoValidator"/> class
        /// </summary>
        public QuoteLineItemDtoValidator()
        {
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
                .GreaterThanOrEqualTo(0).WithMessage("Discount percentage must be greater than or equal to zero")
                .LessThanOrEqualTo(100).WithMessage("Discount percentage cannot exceed 100%");

            RuleFor(x => x.DiscountAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount amount must be greater than or equal to zero");

            RuleFor(x => x.TaxPercentage)
                .GreaterThanOrEqualTo(0).WithMessage("Tax percentage must be greater than or equal to zero");

            RuleFor(x => x.LineNumber)
                .GreaterThan(0).WithMessage("Line number must be greater than zero");

            RuleFor(x => x)
                .Must(x => x.ProductId.HasValue || x.ServiceId.HasValue)
                .WithMessage("Either Product ID or Service ID must be provided");
        }
    }
}