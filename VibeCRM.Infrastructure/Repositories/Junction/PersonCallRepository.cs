using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Person_Call junction entities
    /// </summary>
    public class PersonCallRepository : BaseJunctionRepository<Person_Call, Guid, Guid>, IPersonCallRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Person_Call";

        /// <summary>
        /// Gets the name of the first ID column (PersonId)
        /// </summary>
        protected override string FirstIdColumnName => "PersonId";

        /// <summary>
        /// Gets the name of the second ID column (CallId)
        /// </summary>
        protected override string SecondIdColumnName => "CallId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "PersonId", "CallId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonCallRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public PersonCallRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PersonCallRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all person-call relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-call relationships for the specified person</returns>
        public async Task<IEnumerable<Person_Call>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            return await GetByFirstIdAsync(personId, cancellationToken);
        }

        /// <summary>
        /// Gets all person-call relationships for a specific call
        /// </summary>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-call relationships for the specified call</returns>
        public async Task<IEnumerable<Person_Call>> GetByCallIdAsync(Guid callId, CancellationToken cancellationToken = default)
        {
            return await GetBySecondIdAsync(callId, cancellationToken);
        }

        /// <summary>
        /// Adds a relationship between a person and a call
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created person-call relationship</returns>
        public async Task<Person_Call> AddRelationshipAsync(Guid personId, Guid callId, CancellationToken cancellationToken = default)
        {
            var entity = new Person_Call
            {
                PersonId = personId,
                CallId = callId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        "INSERT INTO Person_Call (PersonId, CallId, Active, ModifiedDate) VALUES (@PersonId, @CallId, @Active, @ModifiedDate)",
                        entity,
                        cancellationToken: cancellationToken)),
                "AddRelationshipAsync",
                new { PersonId = personId, CallId = callId, EntityType = nameof(Person_Call) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Removes a relationship between a person and a call
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid personId, Guid callId, CancellationToken cancellationToken = default)
        {
            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        "UPDATE Person_Call SET Active = 0 WHERE PersonId = @PersonId AND CallId = @CallId",
                        new { PersonId = personId, CallId = callId },
                        cancellationToken: cancellationToken)),
                "RemoveRelationshipAsync",
                new { PersonId = personId, CallId = callId, EntityType = nameof(Person_Call) },
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
                    new { PersonId = personId, EntityType = nameof(Person_Call) },
                    cancellationToken);
            }

            return 0;
        }

        /// <summary>
        /// Removes all relationships for a specific call
        /// </summary>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForCallAsync(Guid callId, CancellationToken cancellationToken = default)
        {
            if (await DeleteByCallIdAsync(callId, cancellationToken))
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
                            new { SecondId = callId },
                            cancellationToken: cancellationToken)),
                    "RemoveAllForCallAsync",
                    new { CallId = callId, EntityType = nameof(Person_Call) },
                    cancellationToken);
            }

            return 0;
        }

        /// <summary>
        /// Deletes all person-call relationships for a specific person
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

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "DeleteByPersonIdAsync",
                new { PersonId = personId, EntityType = nameof(Person_Call) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-call relationships for a specific call
        /// </summary>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByCallIdAsync(Guid callId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0
                WHERE {SecondIdColumnName} = @CallId";

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        new { CallId = callId },
                        cancellationToken: cancellationToken)),
                "DeleteByCallIdAsync",
                new { CallId = callId, EntityType = nameof(Person_Call) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Gets a specific person-call relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person-call relationship if found, otherwise null</returns>
        public async Task<Person_Call?> GetByPersonAndCallIdAsync(Guid personId, Guid callId, CancellationToken cancellationToken = default)
        {
            return await GetByIdAsync(personId, callId, cancellationToken);
        }

        /// <summary>
        /// Gets person-call relationships by call type
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="callTypeId">The call type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-call relationships with the specified call type</returns>
        public async Task<IEnumerable<Person_Call>> GetByCallTypeAsync(Guid personId, Guid callTypeId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT pc.{string.Join(", pc.", SelectColumns)}
                FROM {TableName} pc
                INNER JOIN Call c ON pc.CallId = c.Id
                WHERE pc.{FirstIdColumnName} = @PersonId
                  AND c.CallTypeId = @CallTypeId
                  AND pc.Active = 1
                  AND c.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Call>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, CallTypeId = callTypeId },
                        cancellationToken: cancellationToken)),
                "GetByCallTypeAsync",
                new { PersonId = personId, CallTypeId = callTypeId, EntityType = nameof(Person_Call) },
                cancellationToken);
        }
    }
}