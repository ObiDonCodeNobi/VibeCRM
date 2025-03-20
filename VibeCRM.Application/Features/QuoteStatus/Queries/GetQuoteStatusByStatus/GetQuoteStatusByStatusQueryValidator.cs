using FluentValidation;

namespace VibeCRM.Application.Features.QuoteStatus.Queries.GetQuoteStatusByStatus
{
    /// <summary>
    /// Validator for the GetQuoteStatusByStatusQuery class.
    /// Defines validation rules for retrieving quote statuses by status name.
    /// </summary>
    public class GetQuoteStatusByStatusQueryValidator : AbstractValidator<GetQuoteStatusByStatusQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteStatusByStatusQueryValidator"/> class.
        /// Configures validation rules for GetQuoteStatusByStatusQuery properties.
        /// </summary>
        public GetQuoteStatusByStatusQueryValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status name is required.")
                .MaximumLength(50)
                .WithMessage("Status name cannot exceed 50 characters.");
        }
    }
}