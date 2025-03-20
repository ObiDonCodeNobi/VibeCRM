using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Company_Payment junction entities
    /// </summary>
    public interface ICompanyPaymentRepository : IJunctionRepository<Company_Payment, Guid, Guid>
    {
        /// <summary>
        /// Gets all company-payment relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-payment relationships for the specified company</returns>
        Task<IEnumerable<Company_Payment>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all company-payment relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-payment relationships for the specified payment</returns>
        Task<IEnumerable<Company_Payment>> GetByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific company-payment relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-payment relationship if found, otherwise null</returns>
        Task<Company_Payment?> GetByCompanyAndPaymentIdAsync(Guid companyId, Guid paymentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a company and a payment
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByCompanyAndPaymentAsync(Guid companyId, Guid paymentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific company-payment relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyAndPaymentIdAsync(Guid companyId, Guid paymentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-payment relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-payment relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default);
    }
}