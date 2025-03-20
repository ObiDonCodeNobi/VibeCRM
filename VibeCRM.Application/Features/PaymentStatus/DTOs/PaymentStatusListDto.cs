using System;

namespace VibeCRM.Application.Features.PaymentStatus.DTOs
{
    /// <summary>
    /// List Data Transfer Object for PaymentStatus entity.
    /// Used for transferring payment status data in list views.
    /// </summary>
    public class PaymentStatusListDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the payment status.
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
        /// Gets or sets the count of payments with this status.
        /// </summary>
        public int PaymentCount { get; set; }
    }
}
