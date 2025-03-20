using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing EmailAddress entities
    /// </summary>
    public interface IEmailAddressRepository : IRepository<EmailAddress, Guid>
    {
        /// <summary>
        /// Gets email addresses by their type
        /// </summary>
        /// <param name="emailAddressTypeId">The email address type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of email addresses with the specified type</returns>
        Task<IEnumerable<EmailAddress>> GetByEmailAddressTypeAsync(Guid emailAddressTypeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets email addresses for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of email addresses associated with the specified company</returns>
        Task<IEnumerable<EmailAddress>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets email addresses for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of email addresses associated with the specified person</returns>
        Task<IEnumerable<EmailAddress>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets an email address by the email string
        /// </summary>
        /// <param name="email">The email address string to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The email address entity with the specified email string, or null if not found</returns>
        Task<EmailAddress?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets email addresses by domain
        /// </summary>
        /// <param name="domain">The domain to search for (e.g. "example.com")</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of email addresses with the specified domain</returns>
        Task<IEnumerable<EmailAddress>> GetByDomainAsync(string domain, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if an email address is unique
        /// </summary>
        /// <param name="email">The email address string to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the email address is unique, otherwise false</returns>
        Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);
    }
}