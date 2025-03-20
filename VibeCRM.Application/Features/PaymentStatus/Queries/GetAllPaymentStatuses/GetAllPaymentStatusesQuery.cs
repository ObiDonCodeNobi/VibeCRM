using MediatR;
using VibeCRM.Application.Features.PaymentStatus.DTOs;

namespace VibeCRM.Application.Features.PaymentStatus.Queries.GetAllPaymentStatuses
{
    /// <summary>
    /// Query for retrieving all payment statuses in the system.
    /// Implements the CQRS query pattern for retrieving a collection of payment statuses.
    /// </summary>
    public class GetAllPaymentStatusesQuery : IRequest<IEnumerable<PaymentStatusListDto>>
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include inactive (soft-deleted) payment statuses.
        /// </summary>
        public bool IncludeInactive { get; set; } = false;
    }
}