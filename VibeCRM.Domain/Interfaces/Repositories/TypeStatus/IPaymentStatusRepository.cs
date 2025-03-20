using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing PaymentStatus entities
    /// </summary>
    public interface IPaymentStatusRepository : IRepository<PaymentStatus, Guid>
    {
        /// <summary>
        /// Gets a payment status by its name
        /// </summary>
        /// <param name="status">The name of the payment status</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The payment status if found; otherwise, null</returns>
        Task<PaymentStatus?> GetByNameAsync(string status, CancellationToken cancellationToken = default);
    }
}