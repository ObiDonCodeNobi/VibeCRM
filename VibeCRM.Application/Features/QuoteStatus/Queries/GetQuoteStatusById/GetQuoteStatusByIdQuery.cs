using MediatR;
using VibeCRM.Application.Features.QuoteStatus.DTOs;

namespace VibeCRM.Application.Features.QuoteStatus.Queries.GetQuoteStatusById
{
    /// <summary>
    /// Query for retrieving a quote status by its ID.
    /// </summary>
    public class GetQuoteStatusByIdQuery : IRequest<QuoteStatusDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quote status to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}