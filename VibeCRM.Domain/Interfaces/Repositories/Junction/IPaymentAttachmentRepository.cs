using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Payment_Attachment junction entities
    /// </summary>
    public interface IPaymentAttachmentRepository : IJunctionRepository<Payment_Attachment, Guid, Guid>
    {
        /// <summary>
        /// Gets all payment-attachment relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-attachment relationships for the specified payment</returns>
        Task<IEnumerable<Payment_Attachment>> GetByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all payment-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-attachment relationships for the specified attachment</returns>
        Task<IEnumerable<Payment_Attachment>> GetByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific payment-attachment relationship
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The payment-attachment relationship if found, otherwise null</returns>
        Task<Payment_Attachment?> GetByPaymentAndAttachmentIdAsync(Guid paymentId, Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a payment and an attachment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByPaymentAndAttachmentAsync(Guid paymentId, Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific payment-attachment relationship
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPaymentAndAttachmentIdAsync(Guid paymentId, Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all payment-attachment relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all payment-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default);
    }
}