using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Call entities
    /// </summary>
    public interface ICallRepository : IRepository<Call, Guid>
    {
        /// <summary>
        /// Gets calls by their type
        /// </summary>
        /// <param name="callTypeId">The call type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of calls with the specified type</returns>
        Task<IEnumerable<Call>> GetByCallTypeAsync(Guid callTypeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets calls for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of calls associated with the specified company</returns>
        Task<IEnumerable<Call>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets calls for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of calls associated with the specified person</returns>
        Task<IEnumerable<Call>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets calls by duration range
        /// </summary>
        /// <param name="minDuration">The minimum duration in seconds</param>
        /// <param name="maxDuration">The maximum duration in seconds</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of calls with a duration within the specified range</returns>
        Task<IEnumerable<Call>> GetByDurationRangeAsync(int minDuration, int maxDuration, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets calls by schedule date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of calls scheduled within the specified date range</returns>
        Task<IEnumerable<Call>> GetByScheduleDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets calls by completion date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of calls completed within the specified date range</returns>
        Task<IEnumerable<Call>> GetByCompletionDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}