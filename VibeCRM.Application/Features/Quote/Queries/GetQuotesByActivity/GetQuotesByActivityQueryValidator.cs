using FluentValidation;

namespace VibeCRM.Application.Features.Quote.Queries.GetQuotesByActivity
{
    /// <summary>
    /// Validator for the GetQuotesByActivityQuery
    /// </summary>
    public class GetQuotesByActivityQueryValidator : AbstractValidator<GetQuotesByActivityQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuotesByActivityQueryValidator"/> class.
        /// </summary>
        public GetQuotesByActivityQueryValidator()
        {
            RuleFor(x => x.ActivityId)
                .NotEmpty().WithMessage("Activity ID is required.");
        }
    }
}