using MediatR;
using VibeCRM.Application.Features.Payment.DTOs;

namespace VibeCRM.Application.Features.Payment.Queries.GetPaymentById
{
    /// <summary>
    /// Query to retrieve a payment by its unique identifier.
    /// This is used in the CQRS pattern as the request object for fetching a specific payment.
    /// </summary>
    public class GetPaymentByIdQuery : IRequest<PaymentDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the payment to retrieve.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentByIdQuery"/> class.
        /// </summary>
        public GetPaymentByIdQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentByIdQuery"/> class with a specified payment ID.
        /// </summary>
        /// <param name="id">The unique identifier of the payment to retrieve.</param>
        public GetPaymentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}