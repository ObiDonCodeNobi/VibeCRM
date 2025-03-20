using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Person_EmailAddress junction entities
    /// </summary>
    public class PersonEmailAddressRepository : BaseJunctionRepository<Person_EmailAddress, Guid, Guid>, IPersonEmailAddressRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Person_EmailAddress";

        /// <summary>
        /// Gets the name of the first ID column (PersonId)
        /// </summary>
        protected override string FirstIdColumnName => "PersonId";

        /// <summary>
        /// Gets the name of the second ID column (EmailAddressId)
        /// </summary>
        protected override string SecondIdColumnName => "EmailAddressId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "PersonId", "EmailAddressId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonEmailAddressRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public PersonEmailAddressRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PersonEmailAddressRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all person-email address relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-email address relationships for the specified person</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<IEnumerable<Person_EmailAddress>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_EmailAddress>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetByPersonIdAsync",
                new { PersonId = personId, EntityType = nameof(Person_EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all person-email address relationships for a specific email address
        /// </summary>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-email address relationships for the specified email address</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<IEnumerable<Person_EmailAddress>> GetByEmailAddressIdAsync(Guid emailAddressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @EmailAddressId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_EmailAddress>(
                    new CommandDefinition(
                        sql,
                        new { EmailAddressId = emailAddressId },
                        cancellationToken: cancellationToken)),
                "GetByEmailAddressIdAsync",
                new { EmailAddressId = emailAddressId, EntityType = nameof(Person_EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific person-email address relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person-email address relationship if found, otherwise null</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<Person_EmailAddress?> GetByPersonAndEmailAddressIdAsync(Guid personId, Guid emailAddressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @EmailAddressId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Person_EmailAddress>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, EmailAddressId = emailAddressId },
                        cancellationToken: cancellationToken)),
                "GetByPersonAndEmailAddressIdAsync",
                new { PersonId = personId, EmailAddressId = emailAddressId, EntityType = nameof(Person_EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Gets the primary email address for a person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The primary email address if found, otherwise null</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<Person_EmailAddress?> GetPrimaryEmailAddressForPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            string sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName} pe
                INNER JOIN EmailAddress e ON pe.EmailAddressId = e.Id
                WHERE pe.PersonId = @PersonId
                  AND pe.Active = 1
                  AND e.Active = 1
                ORDER BY e.IsPrimary DESC, e.CreatedDate ASC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Person_EmailAddress>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                nameof(GetPrimaryEmailAddressForPersonAsync),
                new { PersonId = personId, EntityType = nameof(Person_EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Sets an email address as the primary email address for a person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated person-email address relationship or null if the operation fails</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<Person_EmailAddress?> SetPrimaryEmailAddressAsync(Guid personId, Guid emailAddressId, CancellationToken cancellationToken = default)
        {
            // First, clear the primary flag from all other email addresses for this person
            var updateOthersSql = @"
                UPDATE EmailAddress
                SET IsPrimary = 0,
                    ModifiedDate = @ModifiedDate
                FROM EmailAddress e
                JOIN Person_EmailAddress pe ON e.Id = pe.EmailAddressId
                WHERE pe.PersonId = @PersonId
                  AND pe.Active = 1
                  AND e.Active = 1";

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        updateOthersSql,
                        new
                        {
                            PersonId = personId,
                            ModifiedDate = DateTime.UtcNow
                        },
                        cancellationToken: cancellationToken)),
                "SetPrimaryEmailAddressAsync",
                new { PersonId = personId, EntityType = nameof(Person_EmailAddress) },
                cancellationToken);

            // Now set the specified email address as primary
            var updateTargetSql = @"
                UPDATE EmailAddress
                SET IsPrimary = 1,
                    ModifiedDate = @ModifiedDate
                WHERE Id = @EmailAddressId
                  AND Active = 1";

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        updateTargetSql,
                        new
                        {
                            EmailAddressId = emailAddressId,
                            ModifiedDate = DateTime.UtcNow
                        },
                        cancellationToken: cancellationToken)),
                "SetPrimaryEmailAddressAsync",
                new { PersonId = personId, EmailAddressId = emailAddressId, EntityType = nameof(Person_EmailAddress) },
                cancellationToken);

            // Return the updated relationship
            return await GetByPersonAndEmailAddressIdAsync(personId, emailAddressId, cancellationToken);
        }

        /// <summary>
        /// Deletes all person-email address relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<bool> DeleteByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPersonIdAsync",
                new { PersonId = personId, EntityType = nameof(Person_EmailAddress) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-email address relationships for a specific email address
        /// </summary>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<bool> DeleteByEmailAddressIdAsync(Guid emailAddressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @EmailAddressId
                AND Active = 1";

            var parameters = new
            {
                EmailAddressId = emailAddressId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByEmailAddressIdAsync",
                new { EmailAddressId = emailAddressId, EntityType = nameof(Person_EmailAddress) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Gets person-email address relationships by email address type
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="emailAddressTypeId">The email address type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-email address relationships with the specified email address type</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<IEnumerable<Person_EmailAddress>> GetByEmailAddressTypeAsync(Guid personId, Guid emailAddressTypeId, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT pe.PersonId, pe.EmailAddressId, pe.Active, pe.ModifiedDate
                FROM Person_EmailAddress pe
                JOIN EmailAddress e ON pe.EmailAddressId = e.Id
                WHERE pe.PersonId = @PersonId
                  AND e.EmailAddressTypeId = @EmailAddressTypeId
                  AND pe.Active = 1
                  AND e.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_EmailAddress>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, EmailAddressTypeId = emailAddressTypeId },
                        cancellationToken: cancellationToken)),
                "GetByEmailAddressTypeAsync",
                new { PersonId = personId, EmailAddressTypeId = emailAddressTypeId, EntityType = nameof(Person_EmailAddress) },
                cancellationToken);
        }
    }
}