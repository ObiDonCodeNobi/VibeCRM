using MediatR;
using VibeCRM.Application.Features.Invoice.DTOs;

namespace VibeCRM.Application.Features.Invoice.Queries.GetInvoicesBySalesOrderId
{
    /// <summary>
    /// Query for retrieving invoices by their associated sales order ID
    /// </summary>
    public class GetInvoicesBySalesOrderIdQuery : IRequest<IEnumerable<InvoiceListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sales order to retrieve invoices for
        /// </summary>
        public Guid SalesOrderId { get; set; }
    }
}