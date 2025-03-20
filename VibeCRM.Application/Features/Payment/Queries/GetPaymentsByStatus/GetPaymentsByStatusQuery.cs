using MediatR;
using VibeCRM.Application.Features.Payment.DTOs;

namespace VibeCRM.Application.Features.Payment.Queries.GetPaymentsByStatus
{
    /// <summary>
    /// Query to retrieve all payments with a specific payment status.
    /// This is used in the CQRS pattern as the request object for fetching payments by status.
    /// </summary>
    public class GetPaymentsByStatusQuery : IRequest<IEnumerable<PaymentListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the payment status to retrieve payments for.
        /// </summary>
        public Guid PaymentStatusId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByStatusQuery"/> class.
        /// </summary>
        public GetPaymentsByStatusQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByStatusQuery"/> class with a specified payment status ID.
        /// </summary>
        /// <param name="paymentStatusId">The unique identifier of the payment status to retrieve payments for.</param>
        public GetPaymentsByStatusQuery(Guid paymentStatusId)
        {
            PaymentStatusId = paymentStatusId;
        }
    }
}