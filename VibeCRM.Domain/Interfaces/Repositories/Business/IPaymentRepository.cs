using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Payment entities
    /// </summary>
    public interface IPaymentRepository : IRepository<Payment, Guid>
    {
        /// <summary>
        /// Gets payments associated with a specific invoice
        /// </summary>
        /// <param name="invoiceId">The unique identifier of the invoice</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payments associated with the specified invoice</returns>
        Task<IEnumerable<Payment>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets payments made by a specific company
        /// </summary>
        /// <param name="companyId">The unique identifier of the company</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payments made by the specified company</returns>
        Task<IEnumerable<Payment>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets payments made by a specific person
        /// </summary>
        /// <param name="personId">The unique identifier of the person</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payments made by the specified person</returns>
        Task<IEnumerable<Payment>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets payments by their payment status
        /// </summary>
        /// <param name="paymentStatusId">The payment status identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payments with the specified status</returns>
        Task<IEnumerable<Payment>> GetByPaymentStatusAsync(Guid paymentStatusId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets payments made using a specific payment method
        /// </summary>
        /// <param name="paymentMethodId">The unique identifier of the payment method</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payments made using the specified payment method</returns>
        Task<IEnumerable<Payment>> GetByPaymentMethodIdAsync(Guid paymentMethodId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets payments made within a specific date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payments made within the specified date range</returns>
        Task<IEnumerable<Payment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}