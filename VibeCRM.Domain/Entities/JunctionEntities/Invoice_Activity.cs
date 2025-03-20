using VibeCRM.Domain.Common.Events;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.JunctionEntities
{
    /// <summary>
    /// Represents a relationship between an invoice and an activity in the system.
    /// </summary>
    public class Invoice_Activity : IEntity, IHasDomainEvents, ISoftDelete
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();

        /// <summary>
        /// Gets or sets the unique identifier of the invoice.
        /// </summary>
        public Guid InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the activity.
        /// </summary>
        public Guid ActivityId { get; set; }

        /// <summary>
        /// Gets or sets the invoice
        /// </summary>
        public Invoice? Invoice { get; set; }

        /// <summary>
        /// Gets or sets the activity
        /// </summary>
        public Activity? Activity { get; set; }

        /// <summary>
        /// Gets or sets whether this entity is active (not soft-deleted).
        /// When true, the entity is active and visible in queries.
        /// When false, the entity is considered deleted but remains in the database.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Gets or sets the date this entity was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets the collection of domain events
        /// </summary>
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Gets the composite identifier for this entity
        /// </summary>
        /// <returns>An anonymous object containing the composite key</returns>
        public object GetId() => new { InvoiceId, ActivityId };

        /// <summary>
        /// Gets the type of the composite identifier
        /// </summary>
        /// <returns>The type of the composite identifier</returns>
        public Type GetIdType() => typeof(object);

        /// <summary>
        /// Adds a domain event to this entity
        /// </summary>
        /// <param name="domainEvent">The domain event to add</param>
        public void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Removes a domain event from this entity
        /// </summary>
        /// <param name="domainEvent">The domain event to remove</param>
        public void RemoveDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        /// <summary>
        /// Clears all domain events from this entity
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}