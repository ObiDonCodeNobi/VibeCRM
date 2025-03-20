using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Person_Address junction entities
    /// </summary>
    public interface IPersonAddressRepository : IJunctionRepository<Person_Address, Guid, Guid>
    {
        /// <summary>
        /// Gets all person-address relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-address relationships for the specified person</returns>
        Task<IEnumerable<Person_Address>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all person-address relationships for a specific address
        /// </summary>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-address relationships for the specified address</returns>
        Task<IEnumerable<Person_Address>> GetByAddressIdAsync(Guid addressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific person-address relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person-address relationship if found, otherwise null</returns>
        Task<Person_Address?> GetByPersonAndAddressIdAsync(Guid personId, Guid addressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all person-address relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all person-address relationships for a specific address
        /// </summary>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByAddressIdAsync(Guid addressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the primary address for a person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The primary person-address relationship if found, otherwise null</returns>
        Task<Person_Address?> GetPrimaryAddressForPersonAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets an address as the primary address for a person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated person-address relationship if successful, otherwise null</returns>
        Task<Person_Address?> SetPrimaryAddressAsync(Guid personId, Guid addressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets person-address relationships by address type
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="addressTypeId">The address type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-address relationships with the specified address type</returns>
        Task<IEnumerable<Person_Address>> GetByAddressTypeAsync(Guid personId, Guid addressTypeId, CancellationToken cancellationToken = default);
    }
}