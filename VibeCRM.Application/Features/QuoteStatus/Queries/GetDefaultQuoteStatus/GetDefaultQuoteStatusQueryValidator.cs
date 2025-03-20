using FluentValidation;

namespace VibeCRM.Application.Features.QuoteStatus.Queries.GetDefaultQuoteStatus
{
    /// <summary>
    /// Validator for the GetDefaultQuoteStatusQuery class.
    /// Defines validation rules for retrieving the default quote status.
    /// </summary>
    public class GetDefaultQuoteStatusQueryValidator : AbstractValidator<GetDefaultQuoteStatusQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultQuoteStatusQueryValidator"/> class.
        /// No specific validation rules are needed for this query as it doesn't have any parameters.
        /// </summary>
        public GetDefaultQuoteStatusQueryValidator()
        {
            // No specific validation rules are needed for this query
            // as it doesn't have any parameters
        }
    }
}