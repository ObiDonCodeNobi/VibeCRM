using MediatR;
using VibeCRM.Application.Features.Payment.DTOs;

namespace VibeCRM.Application.Features.Payment.Queries.GetPaymentsByMethod
{
    /// <summary>
    /// Query to retrieve all payments with a specific payment method.
    /// This is used in the CQRS pattern as the request object for fetching payments by method.
    /// </summary>
    public class GetPaymentsByMethodQuery : IRequest<IEnumerable<PaymentListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the payment method to retrieve payments for.
        /// </summary>
        public Guid PaymentMethodId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByMethodQuery"/> class.
        /// </summary>
        public GetPaymentsByMethodQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByMethodQuery"/> class with a specified payment method ID.
        /// </summary>
        /// <param name="paymentMethodId">The unique identifier of the payment method to retrieve payments for.</param>
        public GetPaymentsByMethodQuery(Guid paymentMethodId)
        {
            PaymentMethodId = paymentMethodId;
        }
    }
}