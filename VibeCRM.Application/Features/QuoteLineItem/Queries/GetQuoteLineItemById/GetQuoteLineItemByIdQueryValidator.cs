using FluentValidation;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemById;

namespace VibeCRM.Application.Features.QuoteLineItem.Validators
{
    /// <summary>
    /// Validator for the GetQuoteLineItemByIdQuery
    /// </summary>
    public class GetQuoteLineItemByIdQueryValidator : AbstractValidator<GetQuoteLineItemByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteLineItemByIdQueryValidator"/> class
        /// </summary>
        public GetQuoteLineItemByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Quote line item ID is required");
        }
    }
}