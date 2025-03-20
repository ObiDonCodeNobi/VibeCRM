using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Company_Person junction entities
    /// </summary>
    public interface ICompanyPersonRepository : IJunctionRepository<Company_Person, Guid, Guid>
    {
        /// <summary>
        /// Gets all company-person relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-person relationships for the specified company</returns>
        Task<IEnumerable<Company_Person>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all company-person relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-person relationships for the specified person</returns>
        Task<IEnumerable<Company_Person>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific company-person relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-person relationship if found, otherwise null</returns>
        Task<Company_Person?> GetByCompanyAndPersonIdAsync(Guid companyId, Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-person relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-person relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the relationship type between a company and a person
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="personId">The person identifier</param>
        /// <param name="relationshipType">The new relationship type</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated company-person relationship or null if the relationship doesn't exist</returns>
        Task<Company_Person?> UpdateRelationshipTypeAsync(Guid companyId, Guid personId, string relationshipType, CancellationToken cancellationToken = default);
    }
}