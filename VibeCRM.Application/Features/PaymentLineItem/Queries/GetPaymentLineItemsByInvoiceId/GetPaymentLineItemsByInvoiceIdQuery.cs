using MediatR;
using VibeCRM.Application.Features.PaymentLineItem.DTOs;

namespace VibeCRM.Application.Features.PaymentLineItem.Queries.GetPaymentLineItemsByInvoiceId
{
    /// <summary>
    /// Query for retrieving payment line items associated with a specific invoice.
    /// </summary>
    /// <remarks>
    /// This query is used to retrieve all active payment line items for a given invoice ID.
    /// </remarks>
    public class GetPaymentLineItemsByInvoiceIdQuery : IRequest<IEnumerable<PaymentLineItemListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the invoice to retrieve payment line items for.
        /// </summary>
        public Guid InvoiceId { get; set; }
    }
}