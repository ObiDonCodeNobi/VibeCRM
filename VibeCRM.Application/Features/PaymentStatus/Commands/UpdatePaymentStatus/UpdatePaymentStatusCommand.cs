using MediatR;
using VibeCRM.Shared.DTOs.PaymentStatus;

namespace VibeCRM.Application.Features.PaymentStatus.Commands.UpdatePaymentStatus
{
    /// <summary>
    /// Command for updating an existing payment status in the system.
    /// Implements the CQRS command pattern for payment status updates.
    /// </summary>
    public class UpdatePaymentStatusCommand : IRequest<PaymentStatusDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the payment status to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the status name (e.g., "Paid", "Pending", "Overdue").
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the payment status.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting payment statuses in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who last modified the payment status.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}