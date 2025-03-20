using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Payment_PaymentLineItem junction entities
    /// </summary>
    public interface IPaymentPaymentLineItemRepository : IJunctionRepository<Payment_PaymentLineItem, Guid, Guid>
    {
        /// <summary>
        /// Gets all payment-paymentLineItem relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-paymentLineItem relationships for the specified payment</returns>
        Task<IEnumerable<Payment_PaymentLineItem>> GetByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all payment-paymentLineItem relationships for a specific payment line item
        /// </summary>
        /// <param name="paymentLineItemId">The payment line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-paymentLineItem relationships for the specified payment line item</returns>
        Task<IEnumerable<Payment_PaymentLineItem>> GetByPaymentLineItemIdAsync(Guid paymentLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific payment-paymentLineItem relationship
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="paymentLineItemId">The payment line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The payment-paymentLineItem relationship if found, otherwise null</returns>
        Task<Payment_PaymentLineItem?> GetByPaymentAndPaymentLineItemIdAsync(Guid paymentId, Guid paymentLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a payment and a payment line item
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="paymentLineItemId">The payment line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByPaymentAndPaymentLineItemAsync(Guid paymentId, Guid paymentLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific payment-paymentLineItem relationship
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="paymentLineItemId">The payment line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPaymentAndPaymentLineItemIdAsync(Guid paymentId, Guid paymentLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all payment-paymentLineItem relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all payment-paymentLineItem relationships for a specific payment line item
        /// </summary>
        /// <param name="paymentLineItemId">The payment line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPaymentLineItemIdAsync(Guid paymentLineItemId, CancellationToken cancellationToken = default);
    }
}