using FluentValidation;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByQuote;

namespace VibeCRM.Application.Features.QuoteLineItem.Validators
{
    /// <summary>
    /// Validator for the GetQuoteLineItemsByQuoteQuery
    /// </summary>
    public class GetQuoteLineItemsByQuoteQueryValidator : AbstractValidator<GetQuoteLineItemsByQuoteQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteLineItemsByQuoteQueryValidator"/> class
        /// </summary>
        public GetQuoteLineItemsByQuoteQueryValidator()
        {
            RuleFor(x => x.QuoteId)
                .NotEmpty().WithMessage("Quote ID is required");
        }
    }
}