using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Person_Call junction entities
    /// </summary>
    public interface IPersonCallRepository : IJunctionRepository<Person_Call, Guid, Guid>
    {
        /// <summary>
        /// Gets all person-call relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-call relationships for the specified person</returns>
        Task<IEnumerable<Person_Call>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all person-call relationships for a specific call
        /// </summary>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-call relationships for the specified call</returns>
        Task<IEnumerable<Person_Call>> GetByCallIdAsync(Guid callId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific person-call relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person-call relationship if found, otherwise null</returns>
        Task<Person_Call?> GetByPersonAndCallIdAsync(Guid personId, Guid callId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all person-call relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all person-call relationships for a specific call
        /// </summary>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCallIdAsync(Guid callId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets person-call relationships by call type
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="callTypeId">The call type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-call relationships with the specified call type</returns>
        Task<IEnumerable<Person_Call>> GetByCallTypeAsync(Guid personId, Guid callTypeId, CancellationToken cancellationToken = default);
    }
}