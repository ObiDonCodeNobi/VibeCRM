using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Common.Base
{
    /// <summary>
    /// Base class for entities that require audit information
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier</typeparam>
    public abstract class BaseAuditableEntity<TId> : BaseEntity<TId>, ISoftDelete
    {
        /// <summary>
        /// Gets or sets the user identifier who created the entity
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user identifier who last modified the entity
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets whether this entity is active
        /// </summary>
        public bool Active { get; set; } = true;
    }
}