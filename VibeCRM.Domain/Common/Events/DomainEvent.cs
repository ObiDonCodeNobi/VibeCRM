namespace VibeCRM.Domain.Common.Events
{
    /// <summary>
    /// Base class for all domain events
    /// </summary>
    public abstract class DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the DomainEvent class
        /// </summary>
        protected DomainEvent()
        {
            EventId = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the unique identifier for this event
        /// </summary>
        public Guid EventId { get; }

        /// <summary>
        /// Gets the date and time when this event occurred
        /// </summary>
        public DateTime OccurredOn { get; }
    }
}