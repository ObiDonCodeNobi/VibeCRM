using FluentValidation;

namespace VibeCRM.Application.Features.Quote.Queries.GetAllQuotes
{
    /// <summary>
    /// Validator for the GetAllQuotesQuery
    /// </summary>
    public class GetAllQuotesQueryValidator : AbstractValidator<GetAllQuotesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllQuotesQueryValidator"/> class.
        /// </summary>
        public GetAllQuotesQueryValidator()
        {
            // No validation rules needed as this query has no parameters
        }
    }
}