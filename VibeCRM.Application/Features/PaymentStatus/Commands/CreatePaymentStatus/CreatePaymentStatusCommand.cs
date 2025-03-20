using MediatR;
using VibeCRM.Application.Features.PaymentStatus.DTOs;

namespace VibeCRM.Application.Features.PaymentStatus.Commands.CreatePaymentStatus
{
    /// <summary>
    /// Command for creating a new payment status in the system.
    /// Implements the CQRS command pattern for payment status creation.
    /// </summary>
    public class CreatePaymentStatusCommand : IRequest<PaymentStatusDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the payment status.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

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
        /// Gets or sets the ID of the user who created the payment status.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who last modified the payment status.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
