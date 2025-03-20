using MediatR;
using VibeCRM.Application.Features.PaymentStatus.DTOs;

namespace VibeCRM.Application.Features.PaymentStatus.Queries.GetPaymentStatusById
{
    /// <summary>
    /// Query for retrieving a payment status by its unique identifier.
    /// Implements the CQRS query pattern for payment status retrieval by ID.
    /// </summary>
    public class GetPaymentStatusByIdQuery : IRequest<PaymentStatusDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the payment status to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}
