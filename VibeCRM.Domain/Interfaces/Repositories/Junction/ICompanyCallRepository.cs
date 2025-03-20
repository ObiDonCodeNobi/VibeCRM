using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Company_Call junction entities
    /// </summary>
    public interface ICompanyCallRepository : IJunctionRepository<Company_Call, Guid, Guid>
    {
        /// <summary>
        /// Gets all company-call relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-call relationships for the specified company</returns>
        Task<IEnumerable<Company_Call>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all company-call relationships for a specific call
        /// </summary>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-call relationships for the specified call</returns>
        Task<IEnumerable<Company_Call>> GetByCallIdAsync(Guid callId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific company-call relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-call relationship if found, otherwise null</returns>
        Task<Company_Call?> GetByCompanyAndCallIdAsync(Guid companyId, Guid callId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-call relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-call relationships for a specific call
        /// </summary>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCallIdAsync(Guid callId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets company-call relationships by call type
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="callTypeId">The call type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-call relationships with the specified call type</returns>
        Task<IEnumerable<Company_Call>> GetByCallTypeAsync(Guid companyId, Guid callTypeId, CancellationToken cancellationToken = default);
    }
}