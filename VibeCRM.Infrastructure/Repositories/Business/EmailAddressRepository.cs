using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing EmailAddress entities in the database.
    /// Provides methods to create, read, update, and soft delete EmailAddress records,
    /// as well as specialized queries for filtering email addresses by various criteria.
    /// </summary>
    public class EmailAddressRepository : BaseRepository<EmailAddress, Guid>, IEmailAddressRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "EmailAddress";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "EmailAddressId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "EmailAddressId", "EmailAddressTypeId", "Address",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddressRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public EmailAddressRepository(ISQLConnectionFactory connectionFactory, ILogger<EmailAddressRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new email address to the repository
        /// </summary>
        /// <param name="entity">The email address entity to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added email address with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when EmailAddressId is empty or Address is null or empty</exception>
        public override async Task<EmailAddress> AddAsync(EmailAddress entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.EmailAddressId == Guid.Empty) throw new ArgumentException("The Email Address ID cannot be empty", nameof(entity));
            if (string.IsNullOrWhiteSpace(entity.Address)) throw new ArgumentException("The Email Address cannot be null or empty", nameof(entity));

            const string sql = @"
                INSERT INTO EmailAddress (
                    EmailAddressId, EmailAddressTypeId, Address,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @EmailAddressId, @EmailAddressTypeId, @Address,
                    @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active
                );";

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding Email Address with ID {entity.EmailAddressId}", EmailAddressId = entity.EmailAddressId, EntityType = nameof(EmailAddress) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing email address in the repository
        /// </summary>
        /// <param name="entity">The email address to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated email address</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when EmailAddressId is empty or Address is null or empty</exception>
        public override async Task<EmailAddress> UpdateAsync(EmailAddress entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.EmailAddressId == Guid.Empty) throw new ArgumentException("The Email Address ID cannot be empty", nameof(entity));
            if (string.IsNullOrWhiteSpace(entity.Address)) throw new ArgumentException("The Email Address cannot be null or empty", nameof(entity));

            const string sql = @"
                UPDATE EmailAddress
                SET
                    EmailAddressTypeId = @EmailAddressTypeId,
                    Address = @Address,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate,
                    Active = @Active
                WHERE EmailAddressId = @EmailAddressId";

            var affectedRows = await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating Email Address with ID {entity.EmailAddressId}", EmailAddressId = entity.EmailAddressId, EntityType = nameof(EmailAddress) },
                cancellationToken);

            if (affectedRows == 0)
            {
                _logger.LogWarning("No Email Address was updated for ID {EmailAddressId}", entity.EmailAddressId);
            }

            return entity;
        }

        /// <summary>
        /// Deletes an email address by its unique identifier (soft delete)
        /// </summary>
        /// <param name="id">The unique identifier of the email address to delete</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the email address was successfully deleted, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Email Address ID cannot be empty", nameof(id));

            // Use the BaseRepository soft delete pattern
            return await base.DeleteAsync(id, cancellationToken);
        }

        /// <summary>
        /// Checks if an email address with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if an email address with the specified ID exists, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Email Address ID cannot be empty", nameof(id));

            const string sql = "SELECT COUNT(1) FROM EmailAddress WHERE EmailAddressId = @EmailAddressId AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            new { EmailAddressId = id },
                            cancellationToken: cancellationToken)),
                "ExistsAsync",
                new { ErrorMessage = $"Error checking if Email Address with ID {id} exists", EmailAddressId = id, EntityType = nameof(EmailAddress) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Gets all email addresses from the repository
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of all email addresses in the repository</returns>
        public override async Task<IEnumerable<EmailAddress>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY Address";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<EmailAddress>>(
                async (connection) =>
                    await connection.QueryAsync<EmailAddress>(
                        new CommandDefinition(
                            sql,
                            cancellationToken: cancellationToken)),
                "GetAllAsync",
                new { ErrorMessage = "Error retrieving all Email Addresses", EntityType = nameof(EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Gets an email address by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the email address</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The email address if found, otherwise null</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<EmailAddress?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Email Address ID cannot be empty", nameof(id));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE EmailAddressId = @EmailAddressId AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<EmailAddress?>(
                async (connection) =>
                    await connection.QueryFirstOrDefaultAsync<EmailAddress>(
                        new CommandDefinition(
                            sql,
                            new { EmailAddressId = id },
                            cancellationToken: cancellationToken)),
                "GetByIdAsync",
                new { ErrorMessage = $"Error retrieving Email Address with ID {id}", EmailAddressId = id, EntityType = nameof(EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Gets email addresses by email address type
        /// </summary>
        /// <param name="emailAddressTypeId">The email address type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of email addresses with the specified type</returns>
        /// <exception cref="ArgumentException">Thrown when emailAddressTypeId is empty</exception>
        public async Task<IEnumerable<EmailAddress>> GetByEmailAddressTypeAsync(Guid emailAddressTypeId, CancellationToken cancellationToken = default)
        {
            if (emailAddressTypeId == Guid.Empty) throw new ArgumentException("The Email Address Type ID cannot be empty", nameof(emailAddressTypeId));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE EmailAddressTypeId = @EmailAddressTypeId AND Active = 1
                ORDER BY Address";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<EmailAddress>>(
                async (connection) =>
                    await connection.QueryAsync<EmailAddress>(
                        new CommandDefinition(
                            sql,
                            new { EmailAddressTypeId = emailAddressTypeId },
                            cancellationToken: cancellationToken)),
                "GetByEmailAddressTypeAsync",
                new { ErrorMessage = $"Error retrieving Email Addresses with Type ID {emailAddressTypeId}", EmailAddressTypeId = emailAddressTypeId, EntityType = nameof(EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Gets email addresses associated with a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of email addresses associated with the specified company</returns>
        /// <exception cref="ArgumentException">Thrown when companyId is empty</exception>
        public async Task<IEnumerable<EmailAddress>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            if (companyId == Guid.Empty) throw new ArgumentException("The Company ID cannot be empty", nameof(companyId));

            const string sql = @"
                SELECT e.EmailAddressId, e.EmailAddressTypeId, e.Address,
                       e.CreatedBy, e.CreatedDate, e.ModifiedBy, e.ModifiedDate, e.Active
                FROM EmailAddress e
                INNER JOIN Company_EmailAddress ce ON e.EmailAddressId = ce.EmailAddressId
                WHERE ce.CompanyId = @CompanyId AND e.Active = 1
                ORDER BY e.Address";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<EmailAddress>>(
                async (connection) =>
                    await connection.QueryAsync<EmailAddress>(
                        new CommandDefinition(
                            sql,
                            new { CompanyId = companyId },
                            cancellationToken: cancellationToken)),
                "GetByCompanyAsync",
                new { ErrorMessage = $"Error retrieving Email Addresses associated with Company ID {companyId}", CompanyId = companyId, EntityType = nameof(EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Gets email addresses associated with a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of email addresses associated with the specified person</returns>
        /// <exception cref="ArgumentException">Thrown when personId is empty</exception>
        public async Task<IEnumerable<EmailAddress>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            if (personId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(personId));

            const string sql = @"
                SELECT e.EmailAddressId, e.EmailAddressTypeId, e.Address,
                       e.CreatedBy, e.CreatedDate, e.ModifiedBy, e.ModifiedDate, e.Active
                FROM EmailAddress e
                INNER JOIN Person_EmailAddress pe ON e.EmailAddressId = pe.EmailAddressId
                WHERE pe.PersonId = @PersonId AND e.Active = 1
                ORDER BY e.Address";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<EmailAddress>>(
                async (connection) =>
                    await connection.QueryAsync<EmailAddress>(
                        new CommandDefinition(
                            sql,
                            new { PersonId = personId },
                            cancellationToken: cancellationToken)),
                "GetByPersonAsync",
                new { ErrorMessage = $"Error retrieving Email Addresses associated with Person ID {personId}", PersonId = personId, EntityType = nameof(EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Searches for email addresses matching the provided search term
        /// </summary>
        /// <param name="searchTerm">The search term to filter by</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of email addresses that match the search term</returns>
        /// <exception cref="ArgumentException">Thrown when searchTerm is null or empty</exception>
        public async Task<IEnumerable<EmailAddress>> SearchByAddressAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Search term cannot be null or whitespace", nameof(searchTerm));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Address LIKE @SearchPattern AND Active = 1
                ORDER BY Address";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<EmailAddress>>(
                async (connection) =>
                    await connection.QueryAsync<EmailAddress>(
                        new CommandDefinition(
                            sql,
                            new { SearchPattern = $"%{searchTerm}%" },
                            cancellationToken: cancellationToken)),
                "SearchByAddressAsync",
                new { ErrorMessage = $"Error searching Email Addresses with term '{searchTerm}'", SearchTerm = searchTerm, EntityType = nameof(EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Gets an email address by the email string
        /// </summary>
        /// <param name="email">The email address string to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The email address entity with the specified email string, or null if not found</returns>
        /// <exception cref="ArgumentException">Thrown when email is null or empty</exception>
        public async Task<EmailAddress?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email address cannot be null or empty", nameof(email));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Address = @Email AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<EmailAddress?>(
                async (connection) =>
                    await connection.QueryFirstOrDefaultAsync<EmailAddress?>(
                        new CommandDefinition(
                            sql,
                            new { Email = email },
                            cancellationToken: cancellationToken)),
                "GetByEmailAsync",
                new { ErrorMessage = $"Error retrieving Email Address with address '{email}'", Email = email, EntityType = nameof(EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Gets email addresses by domain
        /// </summary>
        /// <param name="domain">The domain to search for (e.g. "example.com")</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of email addresses with the specified domain</returns>
        /// <exception cref="ArgumentException">Thrown when domain is null or empty</exception>
        public async Task<IEnumerable<EmailAddress>> GetByDomainAsync(string domain, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(domain))
                throw new ArgumentException("Domain cannot be null or empty", nameof(domain));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Address LIKE @DomainPattern AND Active = 1
                ORDER BY Address";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<EmailAddress>>(
                async (connection) =>
                    await connection.QueryAsync<EmailAddress>(
                        new CommandDefinition(
                            sql,
                            new { DomainPattern = $"%@{domain}" },
                            cancellationToken: cancellationToken)),
                "GetByDomainAsync",
                new { ErrorMessage = $"Error retrieving Email Addresses with domain '{domain}'", Domain = domain, EntityType = nameof(EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if an email address is unique
        /// </summary>
        /// <param name="email">The email address string to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the email address is unique, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when email is null or empty</exception>
        public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email address cannot be null or empty", nameof(email));

            const string sql = "SELECT COUNT(1) FROM EmailAddress WHERE Address = @Email AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            new { Email = email },
                            cancellationToken: cancellationToken)),
                "IsEmailUniqueAsync",
                new { ErrorMessage = $"Error checking if email '{email}' is unique", Email = email, EntityType = nameof(EmailAddress) },
                cancellationToken);

            return count == 0;
        }
    }
}