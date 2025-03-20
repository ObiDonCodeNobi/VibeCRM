using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Commands.CreateQuoteStatus
{
    /// <summary>
    /// Validator for the CreateQuoteStatusCommand class.
    /// Defines validation rules for creating a new quote status.
    /// </summary>
    public class CreateQuoteStatusCommandValidator : AbstractValidator<CreateQuoteStatusCommand>
    {
        private readonly IQuoteStatusRepository _quoteStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateQuoteStatusCommandValidator"/> class.
        /// </summary>
        /// <param name="quoteStatusRepository">The quote status repository for validating business rules.</param>
        public CreateQuoteStatusCommandValidator(IQuoteStatusRepository quoteStatusRepository)
        {
            _quoteStatusRepository = quoteStatusRepository ?? throw new ArgumentNullException(nameof(quoteStatusRepository));

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status name is required.")
                .MaximumLength(50)
                .WithMessage("Status name cannot exceed 50 characters.")
                .MustAsync(async (status, cancellation) =>
                {
                    var existingStatuses = await _quoteStatusRepository.GetByStatusAsync(status, cancellation);
                    return !existingStatuses.Any();
                })
                .WithMessage("A quote status with this name already exists.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.OrdinalPosition)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ordinal position must be a non-negative number.");
        }
    }
}