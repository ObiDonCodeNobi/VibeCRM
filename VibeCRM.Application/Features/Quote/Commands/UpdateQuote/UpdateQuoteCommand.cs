using MediatR;
using VibeCRM.Shared.DTOs.Quote;

namespace VibeCRM.Application.Features.Quote.Commands.UpdateQuote
{
    /// <summary>
    /// Command for updating an existing quote
    /// </summary>
    public class UpdateQuoteCommand : IRequest<QuoteDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quote to update
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the quote number
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quote status identifier
        /// </summary>
        public Guid? QuoteStatusId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the quote
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}