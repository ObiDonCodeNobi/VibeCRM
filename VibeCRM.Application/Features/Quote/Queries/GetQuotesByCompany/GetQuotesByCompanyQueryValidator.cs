using FluentValidation;

namespace VibeCRM.Application.Features.Quote.Queries.GetQuotesByCompany
{
    /// <summary>
    /// Validator for the GetQuotesByCompanyQuery
    /// </summary>
    public class GetQuotesByCompanyQueryValidator : AbstractValidator<GetQuotesByCompanyQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuotesByCompanyQueryValidator"/> class.
        /// </summary>
        public GetQuotesByCompanyQueryValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("Company ID is required.");
        }
    }
}