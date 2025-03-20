using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Address entities
    /// </summary>
    public interface IAddressRepository : IRepository<Address, Guid>
    {
        /// <summary>
        /// Gets addresses by their type
        /// </summary>
        /// <param name="addressTypeId">The address type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses with the specified type</returns>
        Task<IEnumerable<Address>> GetByAddressTypeAsync(Guid addressTypeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets addresses for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses associated with the specified company</returns>
        Task<IEnumerable<Address>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets addresses for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses associated with the specified person</returns>
        Task<IEnumerable<Address>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets addresses by city
        /// </summary>
        /// <param name="city">The city name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses in the specified city</returns>
        Task<IEnumerable<Address>> GetByCityAsync(string city, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets addresses by state or province
        /// </summary>
        /// <param name="stateOrProvince">The state or province to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses in the specified state or province</returns>
        Task<IEnumerable<Address>> GetByStateOrProvinceAsync(string stateOrProvince, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets addresses by country
        /// </summary>
        /// <param name="country">The country to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses in the specified country</returns>
        Task<IEnumerable<Address>> GetByCountryAsync(string country, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets addresses by postal code
        /// </summary>
        /// <param name="postalCode">The postal code to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses with the specified postal code</returns>
        Task<IEnumerable<Address>> GetByPostalCodeAsync(string postalCode, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets addresses by state ID
        /// </summary>
        /// <param name="stateId">The state identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses in the specified state</returns>
        Task<IEnumerable<Address>> GetByStateIdAsync(Guid stateId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the state entity for an address
        /// </summary>
        /// <param name="address">The address for which to load the state</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadStateAsync(Address address, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the address type entity for an address
        /// </summary>
        /// <param name="address">The address for which to load the address type</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadAddressTypeAsync(Address address, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets an address by ID with all related entities loaded
        /// </summary>
        /// <param name="id">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The address with all related entities loaded, or null if not found</returns>
        Task<Address?> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default);
    }
}