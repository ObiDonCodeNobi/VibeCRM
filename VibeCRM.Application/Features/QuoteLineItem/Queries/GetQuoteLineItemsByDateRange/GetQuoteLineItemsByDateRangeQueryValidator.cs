using FluentValidation;
using VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByDateRange;

namespace VibeCRM.Application.Features.QuoteLineItem.Validators
{
    /// <summary>
    /// Validator for the GetQuoteLineItemsByDateRangeQuery
    /// </summary>
    public class GetQuoteLineItemsByDateRangeQueryValidator : AbstractValidator<GetQuoteLineItemsByDateRangeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteLineItemsByDateRangeQueryValidator"/> class
        /// </summary>
        public GetQuoteLineItemsByDateRangeQueryValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required")
                .LessThanOrEqualTo(x => x.EndDate).WithMessage("Start date must be before or equal to end date");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required")
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be after or equal to start date");
        }
    }
}