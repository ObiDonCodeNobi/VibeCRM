using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Phone entities
    /// </summary>
    public interface IPhoneRepository : IRepository<Phone, Guid>
    {
        /// <summary>
        /// Gets phones by their type
        /// </summary>
        /// <param name="phoneTypeId">The phone type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phones with the specified type</returns>
        Task<IEnumerable<Phone>> GetByPhoneTypeAsync(Guid phoneTypeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets phones for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phones associated with the specified company</returns>
        Task<IEnumerable<Phone>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets phones for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phones associated with the specified person</returns>
        Task<IEnumerable<Phone>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a phone by the phone number
        /// </summary>
        /// <param name="phoneNumber">The phone number to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The phone entity with the specified phone number if found, otherwise null</returns>
        Task<Phone?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets phones by country code
        /// </summary>
        /// <param name="countryCode">The country code to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phones with the specified country code</returns>
        Task<IEnumerable<Phone>> GetByCountryCodeAsync(string countryCode, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a phone number is unique
        /// </summary>
        /// <param name="phoneNumber">The phone number to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the phone number is unique, otherwise false</returns>
        Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a phone by ID with all related details
        /// </summary>
        /// <param name="id">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The phone entity with all related details if found, otherwise null</returns>
        Task<Phone?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a phone is associated with a company
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the phone is associated with the company, otherwise false</returns>
        Task<bool> IsPhoneAssociatedWithCompanyAsync(Guid phoneId, Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a phone is associated with a person
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the phone is associated with the person, otherwise false</returns>
        Task<bool> IsPhoneAssociatedWithPersonAsync(Guid phoneId, Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Associates a phone with a company
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the association was successful, otherwise false</returns>
        Task<bool> AddPhoneToCompanyAsync(Guid phoneId, Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Associates a phone with a person
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the association was successful, otherwise false</returns>
        Task<bool> AddPhoneToPersonAsync(Guid phoneId, Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes an association between a phone and a company
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the association was successfully removed, otherwise false</returns>
        Task<bool> RemovePhoneFromCompanyAsync(Guid phoneId, Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes an association between a phone and a person
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the association was successfully removed, otherwise false</returns>
        Task<bool> RemovePhoneFromPersonAsync(Guid phoneId, Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Searches for phones by a partial or complete phone number
        /// </summary>
        /// <param name="searchTerm">The search term to match against phone numbers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phones that match the search criteria</returns>
        Task<IEnumerable<Phone>> SearchByPhoneNumberAsync(string searchTerm, CancellationToken cancellationToken = default);
    }
}