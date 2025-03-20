using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Note entities
    /// </summary>
    public interface INoteRepository : IRepository<Note, Guid>
    {
        /// <summary>
        /// Gets notes by their type
        /// </summary>
        /// <param name="noteTypeId">The note type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of notes with the specified type</returns>
        Task<IEnumerable<Note>> GetByNoteTypeAsync(Guid noteTypeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets notes for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of notes associated with the specified company</returns>
        Task<IEnumerable<Note>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets notes for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of notes associated with the specified person</returns>
        Task<IEnumerable<Note>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets notes created by a specific user
        /// </summary>
        /// <param name="createdById">The identifier of the user who created the notes</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of notes created by the specified user</returns>
        Task<IEnumerable<Note>> GetByCreatedByAsync(Guid createdById, CancellationToken cancellationToken = default);

        /// <summary>
        /// Searches notes by content
        /// </summary>
        /// <param name="searchText">The text to search for in the note content</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of notes containing the specified text</returns>
        Task<IEnumerable<Note>> SearchByContentAsync(string searchText, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets notes by creation date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of notes created within the specified date range</returns>
        Task<IEnumerable<Note>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}