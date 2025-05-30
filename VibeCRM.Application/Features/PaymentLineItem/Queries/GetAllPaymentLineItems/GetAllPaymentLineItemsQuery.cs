using MediatR;
using VibeCRM.Shared.DTOs.PaymentLineItem;

namespace VibeCRM.Application.Features.PaymentLineItem.Queries.GetAllPaymentLineItems
{
    /// <summary>
    /// Query to retrieve all payment line items with pagination support.
    /// This is used in the CQRS pattern as the request object for fetching payment line items.
    /// </summary>
    public class GetAllPaymentLineItemsQuery : IRequest<List<PaymentLineItemListDto>>
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