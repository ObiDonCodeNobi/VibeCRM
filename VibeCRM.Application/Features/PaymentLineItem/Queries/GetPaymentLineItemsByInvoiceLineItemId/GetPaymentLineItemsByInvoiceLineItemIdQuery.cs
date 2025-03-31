using MediatR;
using VibeCRM.Shared.DTOs.PaymentLineItem;

namespace VibeCRM.Application.Features.PaymentLineItem.Queries.GetPaymentLineItemsByInvoiceLineItemId
{
    /// <summary>
    /// Query for retrieving payment line items associated with a specific invoice line item.
    /// </summary>
    /// <remarks>
    /// This query is used to retrieve all active payment line items for a given invoice line item ID.
    /// </remarks>
    public class GetPaymentLineItemsByInvoiceLineItemIdQuery : IRequest<IEnumerable<PaymentLineItemListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the invoice line item to retrieve payment line items for.
        /// </summary>
        public Guid InvoiceLineItemId { get; set; }
    }
}