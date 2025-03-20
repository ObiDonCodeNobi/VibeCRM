using FluentValidation;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetAllQuoteLineItems;

namespace VibeCRM.Application.Features.QuoteLineItem.Validators
{
    /// <summary>
    /// Validator for the GetAllQuoteLineItemsQuery
    /// </summary>
    public class GetAllQuoteLineItemsQueryValidator : AbstractValidator<GetAllQuoteLineItemsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllQuoteLineItemsQueryValidator"/> class
        /// </summary>
        public GetAllQuoteLineItemsQueryValidator()
        {
            // No specific validation rules needed as the query doesn't have any parameters
        }
    }
}