using MediatR;

namespace VibeCRM.Application.Features.Quote.Commands.DeleteQuote
{
    /// <summary>
    /// Command for soft-deleting an existing quote
    /// </summary>
    public class DeleteQuoteCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quote to delete
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who is performing the delete operation
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}