using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Company_EmailAddress junction entities
    /// </summary>
    public interface ICompanyEmailAddressRepository : IJunctionRepository<Company_EmailAddress, Guid, Guid>
    {
        /// <summary>
        /// Gets all company-email address relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-email address relationships for the specified company</returns>
        Task<IEnumerable<Company_EmailAddress>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all company-email address relationships for a specific email address
        /// </summary>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-email address relationships for the specified email address</returns>
        Task<IEnumerable<Company_EmailAddress>> GetByEmailAddressIdAsync(Guid emailAddressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific company-email address relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-email address relationship if found, otherwise null</returns>
        Task<Company_EmailAddress?> GetByCompanyAndEmailAddressIdAsync(Guid companyId, Guid emailAddressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-email address relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-email address relationships for a specific email address
        /// </summary>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByEmailAddressIdAsync(Guid emailAddressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the primary email address for a company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The primary company-email address relationship if found, otherwise null</returns>
        Task<Company_EmailAddress?> GetPrimaryEmailAddressForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets an email address as the primary email address for a company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated company-email address relationship or null if the operation fails</returns>
        Task<Company_EmailAddress?> SetPrimaryEmailAddressAsync(Guid companyId, Guid emailAddressId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets company-email address relationships by email address type
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="emailAddressTypeId">The email address type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-email address relationships with the specified email address type</returns>
        Task<IEnumerable<Company_EmailAddress>> GetByEmailAddressTypeAsync(Guid companyId, Guid emailAddressTypeId, CancellationToken cancellationToken = default);
    }
}