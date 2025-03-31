using MediatR;
using VibeCRM.Shared.DTOs.PaymentLineItem;

namespace VibeCRM.Application.Features.PaymentLineItem.Queries.GetPaymentLineItemById
{
    /// <summary>
    /// Query for retrieving a specific payment line item by its unique identifier.
    /// </summary>
    /// <remarks>
    /// This query encapsulates the data needed to retrieve a specific payment line item.
    /// </remarks>
    public class GetPaymentLineItemByIdQuery : IRequest<PaymentLineItemDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the payment line item to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}