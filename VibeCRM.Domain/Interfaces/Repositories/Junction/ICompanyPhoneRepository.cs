using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Company_Phone junction entities
    /// </summary>
    public interface ICompanyPhoneRepository : IJunctionRepository<Company_Phone, Guid, Guid>
    {
        /// <summary>
        /// Gets all company-phone relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-phone relationships for the specified company</returns>
        Task<IEnumerable<Company_Phone>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all company-phone relationships for a specific phone
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-phone relationships for the specified phone</returns>
        Task<IEnumerable<Company_Phone>> GetByPhoneIdAsync(Guid phoneId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific company-phone relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-phone relationship if found, otherwise null</returns>
        Task<Company_Phone?> GetByCompanyAndPhoneIdAsync(Guid companyId, Guid phoneId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a company and a phone
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByCompanyAndPhoneAsync(Guid companyId, Guid phoneId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific company-phone relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyAndPhoneIdAsync(Guid companyId, Guid phoneId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-phone relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-phone relationships for a specific phone
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPhoneIdAsync(Guid phoneId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the primary phone for a company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The primary company-phone relationship if found, otherwise null</returns>
        Task<Company_Phone?> GetPrimaryPhoneForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets a phone as the primary phone for a company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated company-phone relationship or null if the operation fails</returns>
        Task<Company_Phone?> SetPrimaryPhoneAsync(Guid companyId, Guid phoneId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets company-phone relationships by phone type
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="phoneTypeId">The phone type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-phone relationships with the specified phone type</returns>
        Task<IEnumerable<Company_Phone>> GetByPhoneTypeAsync(Guid companyId, Guid phoneTypeId, CancellationToken cancellationToken = default);
    }
}