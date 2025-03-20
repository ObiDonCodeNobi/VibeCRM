using MediatR;

namespace VibeCRM.Application.Features.QuoteStatus.Commands.DeleteQuoteStatus
{
    /// <summary>
    /// Command for soft-deleting an existing quote status.
    /// </summary>
    public class DeleteQuoteStatusCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the quote status to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}