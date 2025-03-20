using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface for repositories handling junction entities with composite keys
    /// </summary>
    /// <typeparam name="TEntity">The type of entity managed by the repository</typeparam>
    /// <typeparam name="TFirstId">The type of the first part of the composite key</typeparam>
    /// <typeparam name="TSecondId">The type of the second part of the composite key</typeparam>
    public interface IJunctionRepository<TEntity, TFirstId, TSecondId>
        where TEntity : class, IEntity, ISoftDelete
    {
        /// <summary>
        /// Adds a new junction entity to the repository
        /// </summary>
        /// <param name="entity">The junction entity to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added junction entity with any system-generated values populated</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing junction entity in the repository
        /// </summary>
        /// <param name="entity">The junction entity to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated junction entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a junction entity by its composite key
        /// </summary>
        /// <param name="firstId">The first part of the composite key</param>
        /// <param name="secondId">The second part of the composite key</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The junction entity if found, otherwise null</returns>
        Task<TEntity?> GetByIdAsync(TFirstId firstId, TSecondId secondId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all junction entities associated with a specific first ID
        /// </summary>
        /// <param name="firstId">The first part of the composite key to filter by</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of junction entities with the specified first ID</returns>
        Task<IEnumerable<TEntity>> GetByFirstIdAsync(TFirstId firstId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all junction entities associated with a specific second ID
        /// </summary>
        /// <param name="secondId">The second part of the composite key to filter by</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of junction entities with the specified second ID</returns>
        Task<IEnumerable<TEntity>> GetBySecondIdAsync(TSecondId secondId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all junction entities in the repository
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of all junction entities</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a junction entity by its composite key (soft delete)
        /// </summary>
        /// <param name="firstId">The first part of the composite key</param>
        /// <param name="secondId">The second part of the composite key</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the junction entity was successfully deleted, otherwise false</returns>
        Task<bool> DeleteAsync(TFirstId firstId, TSecondId secondId, CancellationToken cancellationToken = default);
    }
}