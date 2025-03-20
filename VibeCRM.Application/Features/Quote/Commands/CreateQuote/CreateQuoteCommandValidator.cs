using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Quote.Commands.CreateQuote
{
    /// <summary>
    /// Validator for the CreateQuoteCommand
    /// </summary>
    public class CreateQuoteCommandValidator : AbstractValidator<CreateQuoteCommand>
    {
        private readonly IQuoteRepository _quoteRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateQuoteCommandValidator"/> class.
        /// </summary>
        /// <param name="quoteRepository">The quote repository for validation checks</param>
        public CreateQuoteCommandValidator(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Quote number is required.")
                .MaximumLength(50).WithMessage("Quote number cannot exceed 50 characters.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required.");
        }
    }
}