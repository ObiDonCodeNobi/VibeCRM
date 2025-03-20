using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for accessing and managing shipping method entities
    /// </summary>
    public interface IShipMethodRepository : IRepository<ShipMethod, Guid>
    {
        /// <summary>
        /// Gets shipping methods ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of shipping methods ordered by their ordinal position</returns>
        Task<IEnumerable<ShipMethod>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets shipping methods by method name
        /// </summary>
        /// <param name="method">The method name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of shipping methods with the specified method name</returns>
        Task<IEnumerable<ShipMethod>> GetByMethodAsync(string method, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default shipping method
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default shipping method, or null if no shipping methods exist</returns>
        Task<ShipMethod?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}