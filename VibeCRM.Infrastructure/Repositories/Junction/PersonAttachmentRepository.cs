using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Person_Attachment junction entities
    /// </summary>
    public class PersonAttachmentRepository : BaseJunctionRepository<Person_Attachment, Guid, Guid>, IPersonAttachmentRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Person_Attachment";

        /// <summary>
        /// Gets the name of the first ID column (PersonId)
        /// </summary>
        protected override string FirstIdColumnName => "PersonId";

        /// <summary>
        /// Gets the name of the second ID column (AttachmentId)
        /// </summary>
        protected override string SecondIdColumnName => "AttachmentId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "PersonId", "AttachmentId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonAttachmentRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public PersonAttachmentRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PersonAttachmentRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all person-attachment relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-attachment relationships for the specified person</returns>
        public async Task<IEnumerable<Person_Attachment>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            return await GetByFirstIdAsync(personId, cancellationToken);
        }

        /// <summary>
        /// Gets all person-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-attachment relationships for the specified attachment</returns>
        public async Task<IEnumerable<Person_Attachment>> GetByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            return await GetBySecondIdAsync(attachmentId, cancellationToken);
        }

        /// <summary>
        /// Gets a specific person-attachment relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person-attachment relationship if found, otherwise null</returns>
        public async Task<Person_Attachment?> GetByPersonAndAttachmentIdAsync(Guid personId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            return await GetByIdAsync(personId, attachmentId, cancellationToken);
        }

        /// <summary>
        /// Adds a relationship between a person and an attachment
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created person-attachment relationship</returns>
        public async Task<Person_Attachment> AddRelationshipAsync(Guid personId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var entity = new Person_Attachment
            {
                PersonId = personId,
                AttachmentId = attachmentId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        $"INSERT INTO {TableName} ({string.Join(", ", SelectColumns)}) VALUES (@PersonId, @AttachmentId, @Active, @ModifiedDate)",
                        entity,
                        cancellationToken: cancellationToken)),
                "AddRelationshipAsync",
                new { PersonId = personId, AttachmentId = attachmentId, EntityType = nameof(Person_Attachment) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Removes a relationship between a person and an attachment
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid personId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0
                WHERE {FirstIdColumnName} = @PersonId
                  AND {SecondIdColumnName} = @AttachmentId";

            var parameters = new { PersonId = personId, AttachmentId = attachmentId };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveRelationshipAsync",
                new { PersonId = personId, AttachmentId = attachmentId, EntityType = nameof(Person_Attachment) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-attachment relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0
                WHERE {FirstIdColumnName} = @PersonId";

            var parameters = new { PersonId = personId };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPersonIdAsync",
                new { PersonId = personId, EntityType = nameof(Person_Attachment) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0
                WHERE {SecondIdColumnName} = @AttachmentId";

            var parameters = new { AttachmentId = attachmentId };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByAttachmentIdAsync",
                new { AttachmentId = attachmentId, EntityType = nameof(Person_Attachment) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Removes all relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            if (await DeleteByPersonIdAsync(personId, cancellationToken))
            {
                // Get approximate count by querying with Active = 0
                var sql = $@"
                    SELECT COUNT(*)
                    FROM {TableName}
                    WHERE {FirstIdColumnName} = @FirstId
                      AND Active = 0";

                return await ExecuteWithResilienceAndLoggingAsync(
                    async connection => await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            new { FirstId = personId },
                            cancellationToken: cancellationToken)),
                    "RemoveAllForPersonAsync",
                    new { PersonId = personId, EntityType = nameof(Person_Attachment) },
                    cancellationToken);
            }

            return 0;
        }

        /// <summary>
        /// Removes all relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForAttachmentAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            if (await DeleteByAttachmentIdAsync(attachmentId, cancellationToken))
            {
                // Get approximate count by querying with Active = 0
                var sql = $@"
                    SELECT COUNT(*)
                    FROM {TableName}
                    WHERE {SecondIdColumnName} = @SecondId
                      AND Active = 0";

                return await ExecuteWithResilienceAndLoggingAsync(
                    async connection => await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            new { SecondId = attachmentId },
                            cancellationToken: cancellationToken)),
                    "RemoveAllForAttachmentAsync",
                    new { AttachmentId = attachmentId, EntityType = nameof(Person_Attachment) },
                    cancellationToken);
            }

            return 0;
        }

        /// <summary>
        /// Gets person-attachment relationships by attachment type
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="attachmentTypeId">The attachment type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-attachment relationships with the specified attachment type</returns>
        public async Task<IEnumerable<Person_Attachment>> GetByAttachmentTypeAsync(Guid personId, Guid attachmentTypeId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND AttachmentTypeId = @AttachmentTypeId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, AttachmentTypeId = attachmentTypeId },
                        cancellationToken: cancellationToken)),
                "GetByAttachmentTypeAsync",
                new { PersonId = personId, AttachmentTypeId = attachmentTypeId, EntityType = nameof(Person_Attachment) },
                cancellationToken);
        }
    }
}