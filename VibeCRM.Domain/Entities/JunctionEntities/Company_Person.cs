using VibeCRM.Domain.Common.Events;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.JunctionEntities
{
    /// <summary>
    /// Represents a many-to-many relationship between companies and people
    /// </summary>
    public class Company_Person : IEntity, IHasDomainEvents, ISoftDelete
    {
        private List<DomainEvent> _domainEvents = new List<DomainEvent>();

        /// <summary>
        /// Gets or sets the company identifier
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the person identifier
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the company. May be null if the relationship is being loaded lazily.
        /// </summary>
        public Company? Company { get; set; }

        /// <summary>
        /// Gets or sets the person. May be null if the relationship is being loaded lazily.
        /// </summary>
        public Person? Person { get; set; }

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
        public object GetId() => new { CompanyId, PersonId };

        /// <summary>
        /// Gets the type of the composite identifier
        /// </summary>
        /// <returns>The type of the composite identifier</returns>
        public Type GetIdType() => new { CompanyId, PersonId }.GetType();

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