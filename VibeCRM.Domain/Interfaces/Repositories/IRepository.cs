namespace VibeCRM.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Defines the standard repository operations for entity type
    /// </summary>
    /// <typeparam name="TEntity">The entity type this repository operates on</typeparam>
    /// <typeparam name="TId">The type of the entity's primary key</typeparam>
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        /// <summary>
        /// Gets all entities from the repository
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of all entities in the repository</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets an entity by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the entity</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The entity if found, otherwise null</returns>
        Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added entity with any system-generated values (like IDs) populated</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an entity by its unique identifier (soft delete)
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the entity was successfully deleted, otherwise false</returns>
        Task<bool> DeleteAsync(TId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if an entity with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if an entity with the specified ID exists, otherwise false</returns>
        Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default);
    }
}