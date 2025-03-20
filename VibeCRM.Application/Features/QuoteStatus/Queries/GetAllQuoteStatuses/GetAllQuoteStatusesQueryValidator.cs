using FluentValidation;

namespace VibeCRM.Application.Features.QuoteStatus.Queries.GetAllQuoteStatuses
{
    /// <summary>
    /// Validator for the GetAllQuoteStatusesQuery class.
    /// Defines validation rules for retrieving all quote statuses.
    /// </summary>
    public class GetAllQuoteStatusesQueryValidator : AbstractValidator<GetAllQuoteStatusesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllQuoteStatusesQueryValidator"/> class.
        /// No specific validation rules are needed for this query as it doesn't have any parameters.
        /// </summary>
        public GetAllQuoteStatusesQueryValidator()
        {
            // No specific validation rules are needed for this query
            // as it doesn't have any parameters
        }
    }
}
