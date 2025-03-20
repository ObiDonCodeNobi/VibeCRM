using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Person entities
    /// </summary>
    public interface IPersonRepository : IRepository<Person, Guid>
    {
        /// <summary>
        /// Gets persons by their type
        /// </summary>
        /// <param name="personTypeId">The person type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons with the specified type</returns>
        Task<IEnumerable<Person>> GetByPersonTypeAsync(Guid personTypeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets persons by their status
        /// </summary>
        /// <param name="personStatusId">The person status identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons with the specified status</returns>
        Task<IEnumerable<Person>> GetByPersonStatusAsync(Guid personStatusId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets persons for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons associated with the specified company</returns>
        Task<IEnumerable<Person>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Searches persons by name
        /// </summary>
        /// <param name="searchName">The name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons matching the search criteria</returns>
        Task<IEnumerable<Person>> SearchByNameAsync(string searchName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets persons by email address
        /// </summary>
        /// <param name="email">The email address to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons with the specified email address</returns>
        Task<IEnumerable<Person>> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets persons by phone number
        /// </summary>
        /// <param name="phoneNumber">The phone number to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons with the specified phone number</returns>
        Task<IEnumerable<Person>> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets persons created within a date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons created within the specified date range</returns>
        Task<IEnumerable<Person>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets persons by job title
        /// </summary>
        /// <param name="jobTitle">The job title to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons with the specified job title</returns>
        Task<IEnumerable<Person>> GetByJobTitleAsync(string jobTitle, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the companies associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load companies</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadCompaniesAsync(Person person, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the addresses associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load addresses</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadAddressesAsync(Person person, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the phone numbers associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load phone numbers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadPhoneNumbersAsync(Person person, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the email addresses associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load email addresses</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadEmailAddressesAsync(Person person, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the activities associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load activities</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadActivitiesAsync(Person person, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the attachments associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load attachments</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadAttachmentsAsync(Person person, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the notes associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load notes</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadNotesAsync(Person person, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the calls associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load calls</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadCallsAsync(Person person, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a person by ID with all related entities loaded
        /// </summary>
        /// <param name="id">The unique identifier of the person</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person with all related entities loaded, or null if not found</returns>
        Task<Person?> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default);
    }
}