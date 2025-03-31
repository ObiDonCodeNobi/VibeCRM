using FluentValidation;
using VibeCRM.Shared.DTOs.Quote;

namespace VibeCRM.Application.Features.Quote.Validators
{
    /// <summary>
    /// Validator for the QuoteDetailsDto class
    /// </summary>
    public class QuoteDetailsDtoValidator : AbstractValidator<QuoteDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteDetailsDtoValidator"/> class.
        /// </summary>
        public QuoteDetailsDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Quote ID is required.");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Quote number is required.")
                .MaximumLength(50).WithMessage("Quote number cannot exceed 50 characters.");

            // QuoteStatus validation
            RuleFor(x => x.QuoteStatusName)
                .MaximumLength(100).WithMessage("Quote status name cannot exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.QuoteStatusName));

            // LineItems validation
            RuleForEach(x => x.LineItems)
                .SetValidator(new QuoteLineItemDtoValidator());

            // SalesOrders validation - we don't need to validate these in detail as they are read-only in this context
            RuleFor(x => x.SalesOrders)
                .NotNull().WithMessage("Sales orders collection cannot be null.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by is required.");

            RuleFor(x => x.CreatedDate)
                .NotEmpty().WithMessage("Created date is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required.");

            RuleFor(x => x.ModifiedDate)
                .NotEmpty().WithMessage("Modified date is required.");
        }
    }
}