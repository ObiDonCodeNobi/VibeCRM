using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents the status of an invoice in the system, such as Draft, Sent, Paid, etc.
    /// </summary>
    public class InvoiceStatus : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceStatus"/> class.
        /// </summary>
        public InvoiceStatus()
        {
            Id = Guid.NewGuid();
            Status = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the invoice status.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid InvoiceStatusId
        {
            get => Id;
            set => Id = value;
        }

        /// <summary>
        /// Gets or sets the name of the invoice status (e.g., "Draft", "Sent", "Paid", "Overdue").
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets a detailed description of the invoice status.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting invoice statuses in listings.
        /// </summary>
        public int OrdinalPosition { get; set; }

        // The Active property is inherited from BaseAuditableEntity<Guid>
    }
}