using FluentValidation;

namespace VibeCRM.Application.Features.Quote.Queries.GetQuoteById
{
    /// <summary>
    /// Validator for the GetQuoteByIdQuery
    /// </summary>
    public class GetQuoteByIdQueryValidator : AbstractValidator<GetQuoteByIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteByIdQueryValidator"/> class.
        /// </summary>
        public GetQuoteByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Quote ID is required.");
        }
    }
}