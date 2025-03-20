namespace VibeCRM.Domain.Common.Interfaces
{
    /// <summary>
    /// Interface for entities that support soft delete functionality.
    /// When an entity is "deleted", the Active property is set to false rather than
    /// removing the record from the database.
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// Gets or sets whether this entity is active (not soft-deleted).
        /// When true, the entity is active and visible in queries.
        /// When false, the entity is considered deleted but remains in the database.
        /// </summary>
        bool Active { get; set; }
    }
}