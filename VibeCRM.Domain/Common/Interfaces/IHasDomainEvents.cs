using VibeCRM.Domain.Common.Events;

namespace VibeCRM.Domain.Common.Interfaces
{
    /// <summary>
    /// Interface for entities that support domain events
    /// </summary>
    public interface IHasDomainEvents
    {
        /// <summary>
        /// Gets the collection of domain events
        /// </summary>
        IReadOnlyCollection<DomainEvent> DomainEvents { get; }

        /// <summary>
        /// Adds a domain event to the entity
        /// </summary>
        /// <param name="domainEvent">The domain event to add</param>
        void AddDomainEvent(DomainEvent domainEvent);

        /// <summary>
        /// Removes a domain event from the entity
        /// </summary>
        /// <param name="domainEvent">The domain event to remove</param>
        void RemoveDomainEvent(DomainEvent domainEvent);

        /// <summary>
        /// Clears all domain events from the entity
        /// </summary>
        void ClearDomainEvents();
    }
}