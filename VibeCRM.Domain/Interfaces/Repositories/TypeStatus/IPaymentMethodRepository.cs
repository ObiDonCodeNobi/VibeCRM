using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing PaymentMethod entities
    /// </summary>
    public interface IPaymentMethodRepository : IRepository<PaymentMethod, Guid>
    {
        /// <summary>
        /// Gets payment methods ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment methods ordered by their ordinal position</returns>
        Task<IEnumerable<PaymentMethod>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets payment methods by method name
        /// </summary>
        /// <param name="method">The method name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment methods with the specified method name</returns>
        Task<IEnumerable<PaymentMethod>> GetByMethodAsync(string method, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default payment method
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default payment method, or null if no payment methods exist</returns>
        Task<PaymentMethod?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}