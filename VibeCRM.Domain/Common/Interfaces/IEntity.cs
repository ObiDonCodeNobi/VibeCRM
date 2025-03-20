namespace VibeCRM.Domain.Common.Interfaces
{
    /// <summary>
    /// Base interface for all entities with a typed identifier
    /// </summary>
    /// <typeparam name="TId">The type of the entity's identifier</typeparam>
    public interface IEntity<TId>
    {
        /// <summary>
        /// Gets or sets the entity's identifier
        /// </summary>
        TId Id { get; set; }
    }

    /// <summary>
    /// Non-generic interface for entity operations that don't require knowing the specific ID type
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets the entity's identifier as an object
        /// </summary>
        /// <returns>The entity's identifier</returns>
        object GetId();

        /// <summary>
        /// Gets the type of the entity's identifier
        /// </summary>
        /// <returns>The type of the entity's identifier</returns>
        Type GetIdType();
    }
}