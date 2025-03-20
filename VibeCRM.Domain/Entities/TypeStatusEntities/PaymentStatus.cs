using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents a payment status in the system, such as Pending, Completed, Failed, etc.
    /// </summary>
    public class PaymentStatus : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentStatus"/> class.
        /// </summary>
        public PaymentStatus()
        {
            Id = Guid.NewGuid();
            Status = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the payment status.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid PaymentStatusId
        {
            get => Id;
            set => Id = value;
        }

        /// <summary>
        /// Gets or sets the name of the payment status (e.g., "Pending", "Completed", "Failed").
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets a detailed description of the payment status.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting payment statuses in listings.
        /// </summary>
        public int OrdinalPosition { get; set; }

        // The Active property is inherited from BaseAuditableEntity<Guid>
    }
}