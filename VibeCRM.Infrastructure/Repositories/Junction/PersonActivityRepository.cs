using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Person_Activity junction entities
    /// </summary>
    public class PersonActivityRepository : BaseJunctionRepository<Person_Activity, Guid, Guid>, IPersonActivityRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Person_Activity";

        /// <summary>
        /// Gets the name of the first ID column (PersonId)
        /// </summary>
        protected override string FirstIdColumnName => "PersonId";

        /// <summary>
        /// Gets the name of the second ID column (ActivityId)
        /// </summary>
        protected override string SecondIdColumnName => "ActivityId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "PersonId", "ActivityId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonActivityRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public PersonActivityRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PersonActivityRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all person-activity relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-activity relationships for the specified person</returns>
        public async Task<IEnumerable<Person_Activity>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Activity>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetByPersonIdAsync",
                new { PersonId = personId, EntityType = nameof(Person_Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all person-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-activity relationships for the specified activity</returns>
        public async Task<IEnumerable<Person_Activity>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Activity>(
                    new CommandDefinition(
                        sql,
                        new { ActivityId = activityId },
                        cancellationToken: cancellationToken)),
                "GetByActivityIdAsync",
                new { ActivityId = activityId, EntityType = nameof(Person_Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific person-activity relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person-activity relationship if found, otherwise null</returns>
        public async Task<Person_Activity?> GetByPersonAndActivityIdAsync(Guid personId, Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Person_Activity>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, ActivityId = activityId },
                        cancellationToken: cancellationToken)),
                "GetByPersonAndActivityIdAsync",
                new { PersonId = personId, ActivityId = activityId, EntityType = nameof(Person_Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a person and an activity
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByPersonAndActivityAsync(Guid personId, Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, ActivityId = activityId },
                        cancellationToken: cancellationToken)),
                "ExistsByPersonAndActivityAsync",
                new { PersonId = personId, ActivityId = activityId, EntityType = nameof(Person_Activity) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Adds a relationship between a person and an activity
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created person-activity relationship</returns>
        public async Task<Person_Activity> AddRelationshipAsync(Guid personId, Guid activityId, CancellationToken cancellationToken = default)
        {
            var entity = new Person_Activity
            {
                PersonId = personId,
                ActivityId = activityId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            var sql = $@"
                INSERT INTO {TableName} ({string.Join(", ", SelectColumns)})
                VALUES (@PersonId, @ActivityId, @Active, @ModifiedDate)";

            var parameters = new
            {
                PersonId = personId,
                ActivityId = activityId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddRelationshipAsync",
                new { PersonId = personId, ActivityId = activityId, EntityType = nameof(Person_Activity) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Removes a relationship between a person and an activity
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid personId, Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                ActivityId = activityId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveRelationshipAsync",
                new { PersonId = personId, ActivityId = activityId, EntityType = nameof(Person_Activity) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes a specific person-activity relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByPersonAndActivityIdAsync(Guid personId, Guid activityId, CancellationToken cancellationToken = default)
        {
            return await RemoveRelationshipAsync(personId, activityId, cancellationToken);
        }

        /// <summary>
        /// Deletes all person-activity relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
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
                new { PersonId = personId, EntityType = nameof(Person_Activity) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            var parameters = new
            {
                ActivityId = activityId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByActivityIdAsync",
                new { ActivityId = activityId, EntityType = nameof(Person_Activity) },
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
                    new { PersonId = personId, EntityType = nameof(Person_Activity) },
                    cancellationToken);
            }

            return 0;
        }

        /// <summary>
        /// Removes all relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForActivityAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            if (await DeleteByActivityIdAsync(activityId, cancellationToken))
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
                            new { SecondId = activityId },
                            cancellationToken: cancellationToken)),
                    "RemoveAllForActivityAsync",
                    new { ActivityId = activityId, EntityType = nameof(Person_Activity) },
                    cancellationToken);
            }

            return 0;
        }
    }
}