using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Commands.DeleteQuoteStatus
{
    /// <summary>
    /// Validator for the DeleteQuoteStatusCommand class.
    /// Defines validation rules for deleting an existing quote status.
    /// </summary>
    public class DeleteQuoteStatusCommandValidator : AbstractValidator<DeleteQuoteStatusCommand>
    {
        private readonly IQuoteStatusRepository _quoteStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteQuoteStatusCommandValidator"/> class.
        /// </summary>
        /// <param name="quoteStatusRepository">The quote status repository for validating business rules.</param>
        public DeleteQuoteStatusCommandValidator(IQuoteStatusRepository quoteStatusRepository)
        {
            _quoteStatusRepository = quoteStatusRepository ?? throw new ArgumentNullException(nameof(quoteStatusRepository));

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Quote status ID is required.")
                .MustAsync(async (id, cancellation) =>
                {
                    var quoteStatus = await _quoteStatusRepository.GetByIdAsync(id, cancellation);
                    return quoteStatus != null;
                })
                .WithMessage("Quote status with the specified ID does not exist.");
        }
    }
}
