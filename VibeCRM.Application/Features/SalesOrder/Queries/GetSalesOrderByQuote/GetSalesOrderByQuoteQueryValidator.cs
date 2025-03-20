using FluentValidation;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByQuote
{
    /// <summary>
    /// Validator for the GetSalesOrderByQuoteQuery class
    /// </summary>
    public class GetSalesOrderByQuoteQueryValidator : AbstractValidator<GetSalesOrderByQuoteQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderByQuoteQueryValidator"/> class.
        /// </summary>
        public GetSalesOrderByQuoteQueryValidator()
        {
            RuleFor(x => x.QuoteId)
                .NotEmpty().WithMessage("Quote ID is required");
        }
    }
}