using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Person_Note junction entities
    /// </summary>
    public interface IPersonNoteRepository : IJunctionRepository<Person_Note, Guid, Guid>
    {
        /// <summary>
        /// Gets all person-note relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-note relationships for the specified person</returns>
        Task<IEnumerable<Person_Note>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all person-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-note relationships for the specified note</returns>
        Task<IEnumerable<Person_Note>> GetByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific person-note relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person-note relationship if found, otherwise null</returns>
        Task<Person_Note?> GetByPersonAndNoteIdAsync(Guid personId, Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all person-note relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all person-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets person-note relationships by note type
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="noteTypeId">The note type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-note relationships with the specified note type</returns>
        Task<IEnumerable<Person_Note>> GetByNoteTypeAsync(Guid personId, Guid noteTypeId, CancellationToken cancellationToken = default);
    }
}