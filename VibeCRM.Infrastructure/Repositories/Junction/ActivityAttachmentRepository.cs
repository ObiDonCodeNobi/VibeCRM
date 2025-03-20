using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Activity_Attachment junction entities
    /// </summary>
    public class ActivityAttachmentRepository : BaseJunctionRepository<Activity_Attachment, Guid, Guid>, IActivityAttachmentRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Activity_Attachment";

        /// <summary>
        /// Gets the name of the first ID column (ActivityId)
        /// </summary>
        protected override string FirstIdColumnName => "ActivityId";

        /// <summary>
        /// Gets the name of the second ID column (AttachmentId)
        /// </summary>
        protected override string SecondIdColumnName => "AttachmentId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "ActivityId", "AttachmentId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityAttachmentRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public ActivityAttachmentRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<ActivityAttachmentRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all activity-attachment relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity-attachment relationships for the specified activity</returns>
        public async Task<IEnumerable<Activity_Attachment>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @ActivityId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Activity_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { ActivityId = activityId },
                        cancellationToken: cancellationToken)),
                "GetByActivityIdAsync",
                new { ActivityId = activityId, EntityType = nameof(Activity_Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all activity-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity-attachment relationships for the specified attachment</returns>
        public async Task<IEnumerable<Activity_Attachment>> GetByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Activity_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { AttachmentId = attachmentId },
                        cancellationToken: cancellationToken)),
                "GetByAttachmentIdAsync",
                new { AttachmentId = attachmentId, EntityType = nameof(Activity_Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific activity-attachment relationship
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The activity-attachment relationship if found, otherwise null</returns>
        public async Task<Activity_Attachment?> GetByActivityAndAttachmentIdAsync(Guid activityId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @ActivityId
                AND {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Activity_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { ActivityId = activityId, AttachmentId = attachmentId },
                        cancellationToken: cancellationToken)),
                "GetByActivityAndAttachmentIdAsync",
                new { ActivityId = activityId, AttachmentId = attachmentId, EntityType = nameof(Activity_Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between an activity and an attachment
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByActivityAndAttachmentAsync(Guid activityId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @ActivityId
                AND {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { ActivityId = activityId, AttachmentId = attachmentId },
                        cancellationToken: cancellationToken)),
                "ExistsByActivityAndAttachmentAsync",
                new { ActivityId = activityId, AttachmentId = attachmentId, EntityType = nameof(Activity_Attachment) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Deletes a specific activity-attachment relationship
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByActivityAndAttachmentIdAsync(Guid activityId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @ActivityId
                AND {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            var parameters = new
            {
                ActivityId = activityId,
                AttachmentId = attachmentId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByActivityAndAttachmentIdAsync",
                new { ActivityId = activityId, AttachmentId = attachmentId, EntityType = nameof(Activity_Attachment) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all activity-attachment relationships for a specific activity
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
                new { ActivityId = activityId, EntityType = nameof(Activity_Attachment) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all activity-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            var parameters = new
            {
                AttachmentId = attachmentId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByAttachmentIdAsync",
                new { AttachmentId = attachmentId, EntityType = nameof(Activity_Attachment) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}