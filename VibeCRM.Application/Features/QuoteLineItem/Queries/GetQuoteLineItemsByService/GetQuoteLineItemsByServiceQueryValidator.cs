using FluentValidation;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByService;

namespace VibeCRM.Application.Features.QuoteLineItem.Validators
{
    /// <summary>
    /// Validator for the GetQuoteLineItemsByServiceQuery
    /// </summary>
    public class GetQuoteLineItemsByServiceQueryValidator : AbstractValidator<GetQuoteLineItemsByServiceQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteLineItemsByServiceQueryValidator"/> class
        /// </summary>
        public GetQuoteLineItemsByServiceQueryValidator()
        {
            RuleFor(x => x.ServiceId)
                .NotEmpty().WithMessage("Service ID is required");
        }
    }
}