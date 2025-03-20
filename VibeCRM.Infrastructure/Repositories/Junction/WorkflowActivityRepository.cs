using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Workflow_Activity junction entities
    /// </summary>
    public class WorkflowActivityRepository : BaseJunctionRepository<Workflow_Activity, Guid, Guid>, IWorkflowActivityRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Workflow_Activity";

        /// <summary>
        /// Gets the name of the first ID column (WorkflowId)
        /// </summary>
        protected override string FirstIdColumnName => "WorkflowId";

        /// <summary>
        /// Gets the name of the second ID column (ActivityId)
        /// </summary>
        protected override string SecondIdColumnName => "ActivityId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "WorkflowId", "ActivityId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowActivityRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public WorkflowActivityRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<WorkflowActivityRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all workflow-activity relationships for a specific workflow
        /// </summary>
        /// <param name="workflowId">The workflow identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of workflow-activity relationships for the specified workflow</returns>
        public async Task<IEnumerable<Workflow_Activity>> GetByWorkflowIdAsync(Guid workflowId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @WorkflowId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Workflow_Activity>(
                    new CommandDefinition(
                        sql,
                        new { WorkflowId = workflowId },
                        cancellationToken: cancellationToken)),
                "GetByWorkflowIdAsync",
                new { WorkflowId = workflowId, EntityType = nameof(Workflow_Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all workflow-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of workflow-activity relationships for the specified activity</returns>
        public async Task<IEnumerable<Workflow_Activity>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Workflow_Activity>(
                    new CommandDefinition(
                        sql,
                        new { ActivityId = activityId },
                        cancellationToken: cancellationToken)),
                "GetByActivityIdAsync",
                new { ActivityId = activityId, EntityType = nameof(Workflow_Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific workflow-activity relationship
        /// </summary>
        /// <param name="workflowId">The workflow identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The workflow-activity relationship if found, otherwise null</returns>
        public async Task<Workflow_Activity?> GetByWorkflowAndActivityIdAsync(Guid workflowId, Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @WorkflowId
                AND {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Workflow_Activity>(
                    new CommandDefinition(
                        sql,
                        new { WorkflowId = workflowId, ActivityId = activityId },
                        cancellationToken: cancellationToken)),
                "GetByWorkflowAndActivityIdAsync",
                new { WorkflowId = workflowId, ActivityId = activityId, EntityType = nameof(Workflow_Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a workflow and an activity
        /// </summary>
        /// <param name="workflowId">The workflow identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByWorkflowAndActivityAsync(Guid workflowId, Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @WorkflowId
                AND {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { WorkflowId = workflowId, ActivityId = activityId },
                        cancellationToken: cancellationToken)),
                "ExistsByWorkflowAndActivityAsync",
                new { WorkflowId = workflowId, ActivityId = activityId, EntityType = nameof(Workflow_Activity) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Deletes a specific workflow-activity relationship
        /// </summary>
        /// <param name="workflowId">The workflow identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByWorkflowAndActivityIdAsync(Guid workflowId, Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @WorkflowId
                AND {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            var parameters = new
            {
                WorkflowId = workflowId,
                ActivityId = activityId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByWorkflowAndActivityIdAsync",
                new { WorkflowId = workflowId, ActivityId = activityId, EntityType = nameof(Workflow_Activity) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all workflow-activity relationships for a specific workflow
        /// </summary>
        /// <param name="workflowId">The workflow identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByWorkflowIdAsync(Guid workflowId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @WorkflowId
                AND Active = 1";

            var parameters = new
            {
                WorkflowId = workflowId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByWorkflowIdAsync",
                new { WorkflowId = workflowId, EntityType = nameof(Workflow_Activity) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all workflow-activity relationships for a specific activity
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
                new { ActivityId = activityId, EntityType = nameof(Workflow_Activity) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}