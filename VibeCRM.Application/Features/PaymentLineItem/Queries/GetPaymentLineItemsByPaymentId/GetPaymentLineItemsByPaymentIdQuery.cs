using MediatR;
using VibeCRM.Application.Features.PaymentLineItem.DTOs;

namespace VibeCRM.Application.Features.PaymentLineItem.Queries.GetPaymentLineItemsByPaymentId
{
    /// <summary>
    /// Query for retrieving payment line items associated with a specific payment.
    /// </summary>
    /// <remarks>
    /// This query is used to retrieve all active payment line items for a given payment ID.
    /// </remarks>
    public class GetPaymentLineItemsByPaymentIdQuery : IRequest<IEnumerable<PaymentLineItemListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the payment to retrieve line items for.
        /// </summary>
        public Guid PaymentId { get; set; }
    }
}