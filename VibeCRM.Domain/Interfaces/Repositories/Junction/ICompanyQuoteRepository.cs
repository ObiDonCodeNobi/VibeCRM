using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Company_Quote junction entities
    /// </summary>
    public interface ICompanyQuoteRepository : IJunctionRepository<Company_Quote, Guid, Guid>
    {
        /// <summary>
        /// Gets all company-quote relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-quote relationships for the specified company</returns>
        Task<IEnumerable<Company_Quote>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all company-quote relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-quote relationships for the specified quote</returns>
        Task<IEnumerable<Company_Quote>> GetByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific company-quote relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-quote relationship if found, otherwise null</returns>
        Task<Company_Quote?> GetByCompanyAndQuoteIdAsync(Guid companyId, Guid quoteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-quote relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-quote relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets company-quote relationships by date range
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-quote relationships within the specified date range</returns>
        Task<IEnumerable<Company_Quote>> GetByDateRangeAsync(Guid companyId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}