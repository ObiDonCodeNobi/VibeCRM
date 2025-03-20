using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Company_Activity junction entities
    /// </summary>
    public interface ICompanyActivityRepository : IJunctionRepository<Company_Activity, Guid, Guid>
    {
        /// <summary>
        /// Gets all company-activity relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-activity relationships for the specified company</returns>
        Task<IEnumerable<Company_Activity>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all company-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-activity relationships for the specified activity</returns>
        Task<IEnumerable<Company_Activity>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific company-activity relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-activity relationship if found, otherwise null</returns>
        Task<Company_Activity?> GetByCompanyAndActivityIdAsync(Guid companyId, Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a company and an activity
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByCompanyAndActivityAsync(Guid companyId, Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific company-activity relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyAndActivityIdAsync(Guid companyId, Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-activity relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);
    }
}