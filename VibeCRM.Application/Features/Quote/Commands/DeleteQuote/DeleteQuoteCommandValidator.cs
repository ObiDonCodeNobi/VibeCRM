using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Quote.Commands.DeleteQuote
{
    /// <summary>
    /// Validator for the DeleteQuoteCommand
    /// </summary>
    public class DeleteQuoteCommandValidator : AbstractValidator<DeleteQuoteCommand>
    {
        private readonly IQuoteRepository _quoteRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteQuoteCommandValidator"/> class.
        /// </summary>
        /// <param name="quoteRepository">The quote repository for validation checks</param>
        public DeleteQuoteCommandValidator(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Quote ID is required.");

            RuleFor(x => x.ModifiedBy)
                .NotEmpty().WithMessage("Modified by is required.");

            RuleFor(x => x)
                .MustAsync(async (command, cancellationToken) =>
                {
                    var quote = await _quoteRepository.GetByIdAsync(command.Id, cancellationToken);
                    return quote != null && quote.Active;
                }).WithMessage("Quote not found or already deleted.");
        }
    }
}