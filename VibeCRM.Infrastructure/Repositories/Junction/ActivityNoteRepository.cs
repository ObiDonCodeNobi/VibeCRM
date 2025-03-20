using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Activity_Note junction entities
    /// </summary>
    public class ActivityNoteRepository : BaseJunctionRepository<Activity_Note, Guid, Guid>, IActivityNoteRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Activity_Note";

        /// <summary>
        /// Gets the name of the first ID column (ActivityId)
        /// </summary>
        protected override string FirstIdColumnName => "ActivityId";

        /// <summary>
        /// Gets the name of the second ID column (NoteId)
        /// </summary>
        protected override string SecondIdColumnName => "NoteId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "ActivityId", "NoteId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityNoteRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public ActivityNoteRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<ActivityNoteRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all activity-note relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity-note relationships for the specified activity</returns>
        public async Task<IEnumerable<Activity_Note>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @ActivityId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Activity_Note>(
                    new CommandDefinition(
                        sql,
                        new { ActivityId = activityId },
                        cancellationToken: cancellationToken)),
                "GetByActivityIdAsync",
                new { ActivityId = activityId, EntityType = nameof(Activity_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all activity-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity-note relationships for the specified note</returns>
        public async Task<IEnumerable<Activity_Note>> GetByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @NoteId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Activity_Note>(
                    new CommandDefinition(
                        sql,
                        new { NoteId = noteId },
                        cancellationToken: cancellationToken)),
                "GetByNoteIdAsync",
                new { NoteId = noteId, EntityType = nameof(Activity_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific activity-note relationship
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The activity-note relationship if found, otherwise null</returns>
        public async Task<Activity_Note?> GetByActivityAndNoteIdAsync(Guid activityId, Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @ActivityId
                AND {SecondIdColumnName} = @NoteId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Activity_Note>(
                    new CommandDefinition(
                        sql,
                        new { ActivityId = activityId, NoteId = noteId },
                        cancellationToken: cancellationToken)),
                "GetByActivityAndNoteIdAsync",
                new { ActivityId = activityId, NoteId = noteId, EntityType = nameof(Activity_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between an activity and a note
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByActivityAndNoteAsync(Guid activityId, Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @ActivityId
                AND {SecondIdColumnName} = @NoteId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { ActivityId = activityId, NoteId = noteId },
                        cancellationToken: cancellationToken)),
                "ExistsByActivityAndNoteAsync",
                new { ActivityId = activityId, NoteId = noteId, EntityType = nameof(Activity_Note) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Adds a new relationship between an activity and a note
        /// </summary>
        /// <param name="entity">The entity containing the activity and note identifiers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created activity-note relationship</returns>
        public override async Task<Activity_Note> AddAsync(Activity_Note entity, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                IF NOT EXISTS (
                    SELECT 1 FROM {TableName}
                    WHERE {FirstIdColumnName} = @ActivityId
                    AND {SecondIdColumnName} = @NoteId
                    AND Active = 1
                )
                BEGIN
                    INSERT INTO {TableName} ({FirstIdColumnName}, {SecondIdColumnName}, Active, ModifiedDate)
                    VALUES (@ActivityId, @NoteId, 1, @ModifiedDate)
                END
                ELSE
                BEGIN
                    UPDATE {TableName}
                    SET Active = 1, ModifiedDate = @ModifiedDate
                    WHERE {FirstIdColumnName} = @ActivityId
                    AND {SecondIdColumnName} = @NoteId
                END";

            var parameters = new
            {
                ActivityId = entity.ActivityId,
                NoteId = entity.NoteId,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { entity.ActivityId, entity.NoteId, EntityType = nameof(Activity_Note) },
                cancellationToken);

            entity.ModifiedDate = parameters.ModifiedDate;
            entity.Active = true;

            return entity;
        }

        /// <summary>
        /// Deletes a specific activity-note relationship
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByActivityAndNoteIdAsync(Guid activityId, Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @ActivityId
                AND {SecondIdColumnName} = @NoteId
                AND Active = 1";

            var parameters = new
            {
                ActivityId = activityId,
                NoteId = noteId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByActivityAndNoteIdAsync",
                new { ActivityId = activityId, NoteId = noteId, EntityType = nameof(Activity_Note) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all activity-note relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @ActivityId
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
                new { ActivityId = activityId, EntityType = nameof(Activity_Note) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all activity-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @NoteId
                AND Active = 1";

            var parameters = new
            {
                NoteId = noteId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByNoteIdAsync",
                new { NoteId = noteId, EntityType = nameof(Activity_Note) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}