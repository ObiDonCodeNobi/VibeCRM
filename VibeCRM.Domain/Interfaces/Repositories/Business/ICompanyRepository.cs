using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Company entities
    /// </summary>
    public interface ICompanyRepository : IRepository<Company, Guid>
    {
        /// <summary>
        /// Gets companies by their type
        /// </summary>
        /// <param name="accountTypeId">The account type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies with the specified type</returns>
        Task<IEnumerable<Company>> GetByAccountTypeAsync(Guid accountTypeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets companies by their status
        /// </summary>
        /// <param name="accountStatusId">The account status identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies with the specified status</returns>
        Task<IEnumerable<Company>> GetByAccountStatusAsync(Guid accountStatusId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets companies that have a specific person associated with them
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies associated with the specified person</returns>
        Task<IEnumerable<Company>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Searches companies by name
        /// </summary>
        /// <param name="searchName">The name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies matching the search criteria</returns>
        Task<IEnumerable<Company>> SearchByNameAsync(string searchName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets companies by website
        /// </summary>
        /// <param name="website">The website to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies with the specified website</returns>
        Task<IEnumerable<Company>> GetByWebsiteAsync(string website, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets companies by industry
        /// </summary>
        /// <param name="industry">The industry to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies in the specified industry</returns>
        Task<IEnumerable<Company>> GetByIndustryAsync(string industry, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets companies created within a date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies created within the specified date range</returns>
        Task<IEnumerable<Company>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets companies by annual revenue range
        /// </summary>
        /// <param name="minRevenue">The minimum annual revenue</param>
        /// <param name="maxRevenue">The maximum annual revenue</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies with annual revenue in the specified range</returns>
        Task<IEnumerable<Company>> GetByAnnualRevenueRangeAsync(decimal minRevenue, decimal maxRevenue, CancellationToken cancellationToken = default);
    }
}