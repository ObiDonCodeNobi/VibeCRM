using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Person_Payment junction entities
    /// </summary>
    public interface IPersonPaymentRepository : IJunctionRepository<Person_Payment, Guid, Guid>
    {
        /// <summary>
        /// Gets all person-payment relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-payment relationships for the specified person</returns>
        Task<IEnumerable<Person_Payment>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all person-payment relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-payment relationships for the specified payment</returns>
        Task<IEnumerable<Person_Payment>> GetByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default);
    }
}