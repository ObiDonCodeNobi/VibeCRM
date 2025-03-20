using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Company_Note junction entities
    /// </summary>
    public interface ICompanyNoteRepository : IJunctionRepository<Company_Note, Guid, Guid>
    {
        /// <summary>
        /// Gets all company-note relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-note relationships for the specified company</returns>
        Task<IEnumerable<Company_Note>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all company-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-note relationships for the specified note</returns>
        Task<IEnumerable<Company_Note>> GetByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific company-note relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-note relationship if found, otherwise null</returns>
        Task<Company_Note?> GetByCompanyAndNoteIdAsync(Guid companyId, Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-note relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets company-note relationships by note type
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="noteTypeId">The note type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-note relationships with the specified note type</returns>
        Task<IEnumerable<Company_Note>> GetByNoteTypeAsync(Guid companyId, Guid noteTypeId, CancellationToken cancellationToken = default);
    }
}