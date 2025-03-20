using MediatR;
using VibeCRM.Application.Features.InvoiceStatus.DTOs;

namespace VibeCRM.Application.Features.InvoiceStatus.Queries.GetAllInvoiceStatuses
{
    /// <summary>
    /// Query to retrieve all invoice statuses
    /// </summary>
    public class GetAllInvoiceStatusesQuery : IRequest<IEnumerable<InvoiceStatusListDto>>
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include invoice counts in the results
        /// </summary>
        public bool IncludeInvoiceCounts { get; set; } = false;
    }
}
