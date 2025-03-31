using MediatR;
using VibeCRM.Shared.DTOs.InvoiceStatus;

namespace VibeCRM.Application.Features.InvoiceStatus.Queries.GetInvoiceStatusByStatus
{
    /// <summary>
    /// Query to retrieve an invoice status by its status name
    /// </summary>
    public class GetInvoiceStatusByStatusQuery : IRequest<InvoiceStatusDto>
    {
        /// <summary>
        /// Gets or sets the status name to search for
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}