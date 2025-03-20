using MediatR;
using VibeCRM.Application.Features.Invoice.DTOs;

namespace VibeCRM.Application.Features.Invoice.Queries.GetAllInvoices
{
    /// <summary>
    /// Query to retrieve all invoices with pagination support.
    /// This is used in the CQRS pattern as the request object for fetching invoices.
    /// </summary>
    public class GetAllInvoicesQuery : IRequest<List<InvoiceListDto>>
    {
        /// <summary>
        /// Gets or sets the page number for pagination (1-based)
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the page size for pagination
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}