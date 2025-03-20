using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents the status of a quote in the system, such as Draft, Sent, Accepted, Rejected, etc.
    /// </summary>
    public class QuoteStatus : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteStatus"/> class.
        /// </summary>
        public QuoteStatus()
        {
            Id = Guid.NewGuid();
            Status = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the quote status.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid QuoteStatusId
        {
            get => Id;
            set => Id = value;
        }

        /// <summary>
        /// Gets or sets the name of the quote status (e.g., "Draft", "Sent", "Accepted", "Rejected").
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets a detailed description of the quote status.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting quote statuses in listings.
        /// </summary>
        public int OrdinalPosition { get; set; }

        // The Active property is inherited from BaseAuditableEntity<Guid>
    }
}