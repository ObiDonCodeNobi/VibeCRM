using FluentValidation;
using VibeCRM.Shared.DTOs.Quote;

namespace VibeCRM.Application.Features.Quote.Validators
{
    /// <summary>
    /// Validator for the QuoteDto class
    /// </summary>
    public class QuoteDtoValidator : AbstractValidator<QuoteDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteDtoValidator"/> class.
        /// </summary>
        public QuoteDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Quote ID is required.");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Quote number is required.")
                .MaximumLength(50).WithMessage("Quote number cannot exceed 50 characters.");
        }
    }
}