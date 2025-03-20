using FluentValidation;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByProduct;

namespace VibeCRM.Application.Features.QuoteLineItem.Validators
{
    /// <summary>
    /// Validator for the GetQuoteLineItemsByProductQuery
    /// </summary>
    public class GetQuoteLineItemsByProductQueryValidator : AbstractValidator<GetQuoteLineItemsByProductQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteLineItemsByProductQueryValidator"/> class
        /// </summary>
        public GetQuoteLineItemsByProductQueryValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product ID is required");
        }
    }
}