using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Person_EmailAddress junction entities
    /// </summary>
    public interface IPersonEmailAddressRepository : IJunctionRepository<Person_EmailAddress, Guid, Guid>
    {
        /// <summary>
        /// Gets all person-email address relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-email address relationships for the specified person</returns>
        Task<IEnumerable<Person_EmailAddress>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all person-email address relationships for a specific email address
        /// </summary>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-email address relationships for the specified email address</returns>
        Task<IEnumerable<Person_EmailAddress>> GetByEmailAddressIdAsync(Guid emailAddressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific person-email address relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person-email address relationship if found, otherwise null</returns>
        Task<Person_EmailAddress?> GetByPersonAndEmailAddressIdAsync(Guid personId, Guid emailAddressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the primary email address for a person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The primary person-email address relationship if found, otherwise null</returns>
        Task<Person_EmailAddress?> GetPrimaryEmailAddressForPersonAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets an email address as the primary email address for a person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated person-email address relationship or null if the operation fails</returns>
        Task<Person_EmailAddress?> SetPrimaryEmailAddressAsync(Guid personId, Guid emailAddressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets person-email address relationships by email address type
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="emailAddressTypeId">The email address type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-email address relationships with the specified email address type</returns>
        Task<IEnumerable<Person_EmailAddress>> GetByEmailAddressTypeAsync(Guid personId, Guid emailAddressTypeId, CancellationToken cancellationToken = default);
    }
}