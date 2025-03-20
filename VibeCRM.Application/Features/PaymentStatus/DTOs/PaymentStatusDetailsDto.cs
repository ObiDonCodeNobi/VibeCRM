using System;

namespace VibeCRM.Application.Features.PaymentStatus.DTOs
{
    /// <summary>
    /// Detailed Data Transfer Object for PaymentStatus entity.
    /// Used for transferring detailed payment status data including related information.
    /// </summary>
    public class PaymentStatusDetailsDto
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

        /// <summary>
        /// Gets or sets the date when the payment status was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the payment status.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date when the payment status was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the payment status.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
