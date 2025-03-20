using FluentValidation;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Commands.UpdateQuoteStatus
{
    /// <summary>
    /// Validator for the UpdateQuoteStatusCommand class.
    /// Defines validation rules for updating an existing quote status.
    /// </summary>
    public class UpdateQuoteStatusCommandValidator : AbstractValidator<UpdateQuoteStatusCommand>
    {
        private readonly IQuoteStatusRepository _quoteStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateQuoteStatusCommandValidator"/> class.
        /// </summary>
        /// <param name="quoteStatusRepository">The quote status repository for validating business rules.</param>
        public UpdateQuoteStatusCommandValidator(IQuoteStatusRepository quoteStatusRepository)
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

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status name is required.")
                .MaximumLength(50)
                .WithMessage("Status name cannot exceed 50 characters.")
                .MustAsync(async (command, status, cancellation) =>
                {
                    var existingStatuses = await _quoteStatusRepository.GetByStatusAsync(status, cancellation);
                    return !existingStatuses.Any(x => x.Id != command.Id);
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
