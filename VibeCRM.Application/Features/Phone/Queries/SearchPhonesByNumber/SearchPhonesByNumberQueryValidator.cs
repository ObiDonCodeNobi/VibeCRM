using FluentValidation;

namespace VibeCRM.Application.Features.Phone.Queries.SearchPhonesByNumber
{
    /// <summary>
    /// Validator for the SearchPhonesByNumberQuery
    /// </summary>
    public class SearchPhonesByNumberQueryValidator : AbstractValidator<SearchPhonesByNumberQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPhonesByNumberQueryValidator"/> class
        /// </summary>
        public SearchPhonesByNumberQueryValidator()
        {
            RuleFor(p => p.SearchTerm)
                .NotEmpty().WithMessage("Search term is required")
                .MinimumLength(3).WithMessage("Search term must be at least 3 characters long")
                .MaximumLength(20).WithMessage("Search term cannot exceed 20 characters")
                .Matches(@"^[0-9\-\(\)\s]+$").WithMessage("Search term must contain only digits, hyphens, parentheses, and spaces");
        }
    }
}