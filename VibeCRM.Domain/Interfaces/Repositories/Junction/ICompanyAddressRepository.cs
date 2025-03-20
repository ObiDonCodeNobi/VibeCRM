using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Company_Address junction entities
    /// </summary>
    public interface ICompanyAddressRepository : IJunctionRepository<Company_Address, Guid, Guid>
    {
        /// <summary>
        /// Gets all company-address relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-address relationships for the specified company</returns>
        Task<IEnumerable<Company_Address>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all company-address relationships for a specific address
        /// </summary>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-address relationships for the specified address</returns>
        Task<IEnumerable<Company_Address>> GetByAddressIdAsync(Guid addressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific company-address relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-address relationship if found, otherwise null</returns>
        Task<Company_Address?> GetByCompanyAndAddressIdAsync(Guid companyId, Guid addressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a company and an address
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByCompanyAndAddressAsync(Guid companyId, Guid addressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific company-address relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyAndAddressIdAsync(Guid companyId, Guid addressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-address relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-address relationships for a specific address
        /// </summary>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByAddressIdAsync(Guid addressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the primary address for a company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The primary company-address relationship if found, otherwise null</returns>
        Task<Company_Address?> GetPrimaryAddressForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets an address as the primary address for a company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated company-address relationship, or null if the relationship doesn't exist</returns>
        Task<Company_Address?> SetPrimaryAddressAsync(Guid companyId, Guid addressId, CancellationToken cancellationToken = default);
    }
}