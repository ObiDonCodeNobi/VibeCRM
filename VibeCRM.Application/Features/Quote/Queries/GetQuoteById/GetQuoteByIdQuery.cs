using MediatR;
using VibeCRM.Application.Features.Quote.DTOs;

namespace VibeCRM.Application.Features.Quote.Queries.GetQuoteById
{
    /// <summary>
    /// Query for retrieving a specific quote by its unique identifier
    /// </summary>
    public class GetQuoteByIdQuery : IRequest<QuoteDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quote to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}