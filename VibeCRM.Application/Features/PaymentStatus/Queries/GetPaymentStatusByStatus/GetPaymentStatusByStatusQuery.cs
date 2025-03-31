using MediatR;
using VibeCRM.Shared.DTOs.PaymentStatus;

namespace VibeCRM.Application.Features.PaymentStatus.Queries.GetPaymentStatusByStatus
{
    /// <summary>
    /// Query for retrieving a payment status by its status name.
    /// Implements the CQRS query pattern for payment status retrieval by status name.
    /// </summary>
    public class GetPaymentStatusByStatusQuery : IRequest<PaymentStatusDetailsDto>
    {
        /// <summary>
        /// Gets or sets the status name of the payment status to retrieve.
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}