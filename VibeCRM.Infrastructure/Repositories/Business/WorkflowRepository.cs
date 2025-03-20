using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing Workflow entities
    /// </summary>
    public class WorkflowRepository : BaseRepository<Workflow, Guid>, IWorkflowRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Workflow";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "WorkflowId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "WorkflowId", "WorkflowTypeId", "Subject", "Description", "StartDate", "CompletedDate",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Base SELECT query for Workflow entities
        /// </summary>
        private const string BaseSelectQuery = @"
            SELECT w.WorkflowId AS Id, w.WorkflowTypeId, w.Subject, w.Description,
                   w.StartDate, w.CompletedDate,
                   w.CreatedBy, w.CreatedDate, w.ModifiedBy, w.ModifiedDate, w.Active
            FROM Workflow w
            WHERE w.Active = 1";

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public WorkflowRepository(ISQLConnectionFactory connectionFactory, ILogger<WorkflowRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new workflow to the repository
        /// </summary>
        /// <param name="entity">The workflow to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added workflow with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when WorkflowId is empty</exception>
        public override async Task<Workflow> AddAsync(Workflow entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.WorkflowId == Guid.Empty) throw new ArgumentException("The Workflow ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO Workflow (
                    WorkflowId, WorkflowTypeId, Subject, Description, StartDate, CompletedDate,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @WorkflowId, @WorkflowTypeId, @Subject, @Description, @StartDate, @CompletedDate,
                    @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, 1
                );";

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding Workflow with ID {entity.WorkflowId}", WorkflowId = entity.WorkflowId, EntityType = nameof(Workflow) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing workflow in the repository
        /// </summary>
        /// <param name="entity">The workflow to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated workflow</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when WorkflowId is empty</exception>
        public override async Task<Workflow> UpdateAsync(Workflow entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.WorkflowId == Guid.Empty) throw new ArgumentException("The Workflow ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE Workflow SET
                    WorkflowTypeId = @WorkflowTypeId,
                    Subject = @Subject,
                    Description = @Description,
                    StartDate = @StartDate,
                    CompletedDate = @CompletedDate,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                WHERE WorkflowId = @WorkflowId AND Active = 1;";

            int rowsAffected = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating Workflow with ID {entity.WorkflowId}", WorkflowId = entity.WorkflowId, EntityType = nameof(Workflow) },
                cancellationToken);

            if (rowsAffected == 0)
            {
                _logger.LogWarning("Workflow with ID {WorkflowId} not found for update or already inactive", entity.WorkflowId);
            }

            return entity;
        }

        /// <summary>
        /// Gets a workflow by its name
        /// </summary>
        /// <param name="name">The name of the workflow</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The workflow if found, otherwise null</returns>
        public async Task<Workflow?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND w.Subject = @Name";

            return await ExecuteWithResilienceAndLoggingAsync<Workflow?>(
                async connection => await connection.QuerySingleOrDefaultAsync<Workflow>(
                    new CommandDefinition(
                        sql,
                        new { Name = name },
                        cancellationToken: cancellationToken)),
                "GetByNameAsync",
                new { Name = name },
                cancellationToken);
        }

        /// <summary>
        /// Gets all workflows associated with a specific activity
        /// </summary>
        /// <param name="activityId">The unique identifier of the activity</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of workflows associated with the specified activity</returns>
        public async Task<IEnumerable<Workflow>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT w.WorkflowId AS Id, w.WorkflowTypeId, w.Subject, w.Description,
                       w.StartDate, w.CompletedDate,
                       w.CreatedBy, w.CreatedDate, w.ModifiedBy, w.ModifiedDate, w.Active
                FROM Workflow w
                JOIN Activity_Workflow aw ON w.WorkflowId = aw.WorkflowId
                WHERE aw.ActivityId = @ActivityId
                  AND w.Active = 1
                  AND aw.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Workflow>>(
                async connection => await connection.QueryAsync<Workflow>(
                    new CommandDefinition(
                        sql,
                        new { ActivityId = activityId },
                        cancellationToken: cancellationToken)),
                "GetByActivityIdAsync",
                new { ActivityId = activityId },
                cancellationToken);
        }

        /// <summary>
        /// Gets workflows by their workflow type
        /// </summary>
        /// <param name="workflowTypeId">The workflow type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of workflows of the specified type</returns>
        public async Task<IEnumerable<Workflow>> GetByWorkflowTypeAsync(Guid workflowTypeId, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND w.WorkflowTypeId = @WorkflowTypeId";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Workflow>>(
                async connection => await connection.QueryAsync<Workflow>(
                    new CommandDefinition(
                        sql,
                        new { WorkflowTypeId = workflowTypeId },
                        cancellationToken: cancellationToken)),
                "GetByWorkflowTypeAsync",
                new { WorkflowTypeId = workflowTypeId },
                cancellationToken);
        }
    }
}