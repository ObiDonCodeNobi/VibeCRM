using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Payment_Activity junction entities
    /// </summary>
    public interface IPaymentActivityRepository : IJunctionRepository<Payment_Activity, Guid, Guid>
    {
        /// <summary>
        /// Gets all payment-activity relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-activity relationships for the specified payment</returns>
        Task<IEnumerable<Payment_Activity>> GetByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all payment-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-activity relationships for the specified activity</returns>
        Task<IEnumerable<Payment_Activity>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);
    }
}