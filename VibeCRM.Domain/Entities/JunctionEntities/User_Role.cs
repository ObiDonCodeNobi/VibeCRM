using VibeCRM.Domain.Common.Events;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.JunctionEntities
{
    /// <summary>
    /// Represents a many-to-many relationship between users and roles
    /// </summary>
    public class User_Role : IEntity, IHasDomainEvents, ISoftDelete
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();

        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the user
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Gets or sets the role
        /// </summary>
        public Role? Role { get; set; }

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
        public object GetId() => new { UserId, RoleId };

        /// <summary>
        /// Gets the type of the composite identifier
        /// </summary>
        /// <returns>The type of the composite identifier</returns>
        public Type GetIdType() => typeof(object);

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
    }
}