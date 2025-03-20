using FluentValidation;

namespace VibeCRM.Application.Features.QuoteStatus.Queries.GetQuoteStatusByOrdinalPosition
{
    /// <summary>
    /// Validator for the GetQuoteStatusByOrdinalPositionQuery class.
    /// Defines validation rules for retrieving quote statuses by ordinal position.
    /// </summary>
    public class GetQuoteStatusByOrdinalPositionQueryValidator : AbstractValidator<GetQuoteStatusByOrdinalPositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteStatusByOrdinalPositionQueryValidator"/> class.
        /// Configures validation rules for GetQuoteStatusByOrdinalPositionQuery properties.
        /// </summary>
        public GetQuoteStatusByOrdinalPositionQueryValidator()
        {
            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}