using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Payment_Note junction entities
    /// </summary>
    public interface IPaymentNoteRepository : IJunctionRepository<Payment_Note, Guid, Guid>
    {
        /// <summary>
        /// Gets all payment-note relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-note relationships for the specified payment</returns>
        Task<IEnumerable<Payment_Note>> GetByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all payment-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-note relationships for the specified note</returns>
        Task<IEnumerable<Payment_Note>> GetByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific payment-note relationship
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The payment-note relationship if found, otherwise null</returns>
        Task<Payment_Note?> GetByPaymentAndNoteIdAsync(Guid paymentId, Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a payment and a note
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByPaymentAndNoteAsync(Guid paymentId, Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific payment-note relationship
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPaymentAndNoteIdAsync(Guid paymentId, Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all payment-note relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all payment-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default);
    }
}