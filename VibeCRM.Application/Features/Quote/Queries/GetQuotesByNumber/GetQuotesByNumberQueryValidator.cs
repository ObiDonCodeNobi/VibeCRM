using FluentValidation;

namespace VibeCRM.Application.Features.Quote.Queries.GetQuotesByNumber
{
    /// <summary>
    /// Validator for the GetQuotesByNumberQuery
    /// </summary>
    public class GetQuotesByNumberQueryValidator : AbstractValidator<GetQuotesByNumberQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuotesByNumberQueryValidator"/> class.
        /// </summary>
        public GetQuotesByNumberQueryValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Quote number is required.")
                .MaximumLength(50).WithMessage("Quote number cannot exceed 50 characters.");
        }
    }
}