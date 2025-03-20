using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Quote.Commands.UpdateQuote
{
    /// <summary>
    /// Validator for the UpdateQuoteCommand
    /// </summary>
    public class UpdateQuoteCommandValidator : AbstractValidator<UpdateQuoteCommand>
    {
        private readonly IQuoteRepository _quoteRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateQuoteCommandValidator"/> class.
        /// </summary>
        /// <param name="quoteRepository">The quote repository for validation checks</param>
        public UpdateQuoteCommandValidator(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Quote ID is required.");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Quote number is required.")
                .MaximumLength(50).WithMessage("Quote number cannot exceed 50 characters.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required.");

            RuleFor(x => x)
                .MustAsync(async (command, cancellationToken) =>
                {
                    var quote = await _quoteRepository.GetByIdAsync(command.Id, cancellationToken);
                    return quote != null && quote.Active;
                }).WithMessage("Quote not found or inactive.");
        }
    }
}