using FluentValidation;
using VibeCRM.Shared.DTOs.Quote;

namespace VibeCRM.Application.Features.Quote.Validators
{
    /// <summary>
    /// Validator for the QuoteListDto class
    /// </summary>
    public class QuoteListDtoValidator : AbstractValidator<QuoteListDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteListDtoValidator"/> class.
        /// </summary>
        public QuoteListDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Quote ID is required.");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Quote number is required.")
                .MaximumLength(50).WithMessage("Quote number cannot exceed 50 characters.");

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.");
        }
    }
}