using FluentValidation;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetTotalForQuote;

namespace VibeCRM.Application.Features.QuoteLineItem.Validators
{
    /// <summary>
    /// Validator for the GetTotalForQuoteQuery
    /// </summary>
    public class GetTotalForQuoteQueryValidator : AbstractValidator<GetTotalForQuoteQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTotalForQuoteQueryValidator"/> class
        /// </summary>
        public GetTotalForQuoteQueryValidator()
        {
            RuleFor(x => x.QuoteId)
                .NotEmpty().WithMessage("Quote ID is required");
        }
    }
}