using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Person_Phone junction entities
    /// </summary>
    public interface IPersonPhoneRepository : IJunctionRepository<Person_Phone, Guid, Guid>
    {
        /// <summary>
        /// Gets all person-phone relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-phone relationships for the specified person</returns>
        Task<IEnumerable<Person_Phone>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all person-phone relationships for a specific phone
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-phone relationships for the specified phone</returns>
        Task<IEnumerable<Person_Phone>> GetByPhoneIdAsync(Guid phoneId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship between the specified person and phone exists
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a relationship exists, otherwise false</returns>
        Task<bool> ExistsByPersonAndPhoneAsync(Guid personId, Guid phoneId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all person-phone relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all person-phone relationships for a specific phone
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPhoneIdAsync(Guid phoneId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific relationship between a person and a phone
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was deleted, otherwise false</returns>
        Task<bool> DeleteByPersonAndPhoneAsync(Guid personId, Guid phoneId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the primary phone for a person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The primary person-phone relationship if found, otherwise null</returns>
        Task<Person_Phone?> GetPrimaryPhoneForPersonAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets a phone as the primary phone for a person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated person-phone relationship if successful, otherwise null</returns>
        Task<Person_Phone?> SetPrimaryPhoneAsync(Guid personId, Guid phoneId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets person-phone relationships by phone type
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="phoneTypeId">The phone type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-phone relationships with the specified phone type</returns>
        Task<IEnumerable<Person_Phone>> GetByPhoneTypeAsync(Guid personId, Guid phoneTypeId, CancellationToken cancellationToken = default);
    }
}