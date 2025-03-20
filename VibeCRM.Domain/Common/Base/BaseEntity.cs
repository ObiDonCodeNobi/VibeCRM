using VibeCRM.Domain.Common.Events;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Common.Base
{
    /// <summary>
    /// Base class for all entities with typed identifiers
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier</typeparam>
    public abstract class BaseEntity<TId> : IEntity<TId>, IEntity, IHasDomainEvents
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();

        /// <summary>
        /// Gets or sets the entity's identifier
        /// </summary>
        public TId Id { get; set; }

        /// <summary>
        /// Gets the collection of domain events
        /// </summary>
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Initializes a new instance of the BaseEntity class
        /// </summary>
        public BaseEntity() { Id = (TId)Convert.ChangeType(Guid.NewGuid(), typeof(TId)); }

        /// <summary>
        /// Adds a domain event to the entity
        /// </summary>
        /// <param name="domainEvent">The domain event to add</param>
        public void AddDomainEvent(DomainEvent domainEvent)
        {
            if (domainEvent == null)
            {
                throw new ArgumentNullException(nameof(domainEvent));
            }

            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Removes a domain event from the entity
        /// </summary>
        /// <param name="domainEvent">The domain event to remove</param>
        public void RemoveDomainEvent(DomainEvent domainEvent)
        {
            if (domainEvent != null)
            {
                _domainEvents.Remove(domainEvent);
            }
        }

        /// <summary>
        /// Clears all domain events from the entity
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        /// <summary>
        /// Gets the entity's identifier as an object
        /// </summary>
        /// <returns>The entity's identifier</returns>
        public object GetId() => Id!;

        /// <summary>
        /// Gets the type of the entity's identifier
        /// </summary>
        /// <returns>The type of the entity's identifier</returns>
        public Type GetIdType() => typeof(TId);
    }
}