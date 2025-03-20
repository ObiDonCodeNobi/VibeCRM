using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Queries.GetQuoteStatusById
{
    /// <summary>
    /// Validator for the GetQuoteStatusByIdQuery class.
    /// Defines validation rules for retrieving a quote status by ID.
    /// </summary>
    public class GetQuoteStatusByIdQueryValidator : AbstractValidator<GetQuoteStatusByIdQuery>
    {
        private readonly IQuoteStatusRepository _quoteStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteStatusByIdQueryValidator"/> class.
        /// </summary>
        /// <param name="quoteStatusRepository">The quote status repository for validating business rules.</param>
        public GetQuoteStatusByIdQueryValidator(IQuoteStatusRepository quoteStatusRepository)
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
