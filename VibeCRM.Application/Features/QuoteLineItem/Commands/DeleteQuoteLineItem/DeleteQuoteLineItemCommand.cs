using MediatR;

namespace VibeCRM.Application.Features.QuoteLineItem.Commands.DeleteQuoteLineItem
{
    /// <summary>
    /// Command for deleting a quote line item
    /// </summary>
    public class DeleteQuoteLineItemCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quote line item to delete
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user performing the delete operation
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}