using MediatR;
using VibeCRM.Shared.DTOs.QuoteStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Queries.GetQuoteStatusByStatus
{
    /// <summary>
    /// Query for retrieving quote statuses by their status name.
    /// </summary>
    public class GetQuoteStatusByStatusQuery : IRequest<IEnumerable<QuoteStatusDto>>
    {
        /// <summary>
        /// Gets or sets the status name to retrieve quote statuses by.
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}