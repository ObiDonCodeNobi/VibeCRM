using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing ActivityDefinition entities.
    /// Provides data access operations for activity definitions using Dapper ORM.
    /// </summary>
    public class ActivityDefinitionRepository : BaseRepository<ActivityDefinition, Guid>, IActivityDefinitionRepository
    {
        /// <summary>
        /// Gets the table name for the entity.
        /// </summary>
        protected override string TableName => "ActivityDefinition";

        /// <summary>
        /// Gets the ID column name for the entity.
        /// </summary>
        protected override string IdColumnName => "ActivityDefinitionId";

        /// <summary>
        /// Gets a list of columns to select for basic queries.
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "ActivityDefinitionId", "ActivityTypeId", "ActivityStatusId", "AssignedUserId",
            "AssignedTeamId", "Subject", "Description", "DueDateOffset",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Base select query used by multiple methods.
        /// </summary>
        private const string BaseSelectQuery = @"
            SELECT
                ad.ActivityDefinitionId, ad.ActivityTypeId, ad.ActivityStatusId, ad.AssignedUserId,
                ad.AssignedTeamId, ad.Subject, ad.Description, ad.DueDateOffset,
                ad.CreatedBy, ad.CreatedDate, ad.ModifiedBy, ad.ModifiedDate, ad.Active,
                at.ActivityTypeId, at.Name as ActivityTypeName, at.Description as ActivityTypeDescription,
                ast.ActivityStatusId, ast.Name as ActivityStatusName, ast.Description as ActivityStatusDescription
            FROM ActivityDefinition ad
            LEFT JOIN ActivityType at ON ad.ActivityTypeId = at.ActivityTypeId
            LEFT JOIN ActivityStatus ast ON ad.ActivityStatusId = ast.ActivityStatusId
            WHERE ad.Active = 1";

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityDefinitionRepository"/> class.
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections.</param>
        /// <param name="logger">The logger for logging events and errors.</param>
        public ActivityDefinitionRepository(ISQLConnectionFactory connectionFactory, ILogger<ActivityDefinitionRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new activity definition to the repository.
        /// </summary>
        /// <param name="entity">The activity definition to add.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The added activity definition with any system-generated values populated.</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null.</exception>
        /// <exception cref="ArgumentException">Thrown when ActivityDefinitionId is empty.</exception>
        public override async Task<ActivityDefinition> AddAsync(ActivityDefinition entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Id == Guid.Empty) throw new ArgumentException("The ActivityDefinition ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO ActivityDefinition (
                    ActivityDefinitionId, ActivityTypeId, ActivityStatusId, AssignedUserId,
                    AssignedTeamId, Subject, Description, DueDateOffset,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @Id, @ActivityTypeId, @ActivityStatusId, @AssignedUserId,
                    @AssignedTeamId, @Subject, @Description, @DueDateOffset,
                    @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active
                );";

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            new
                            {
                                entity.Id,
                                entity.ActivityTypeId,
                                entity.ActivityStatusId,
                                entity.AssignedUserId,
                                entity.AssignedTeamId,
                                entity.Subject,
                                entity.Description,
                                entity.DueDateOffset,
                                entity.CreatedBy,
                                entity.CreatedDate,
                                entity.ModifiedBy,
                                entity.ModifiedDate,
                                entity.Active
                            },
                            cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding ActivityDefinition with ID {entity.Id}", ActivityDefinitionId = entity.Id, EntityType = nameof(ActivityDefinition) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing activity definition in the repository.
        /// </summary>
        /// <param name="entity">The activity definition to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The updated activity definition.</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null.</exception>
        /// <exception cref="ArgumentException">Thrown when ActivityDefinitionId is empty.</exception>
        public override async Task<ActivityDefinition> UpdateAsync(ActivityDefinition entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Id == Guid.Empty) throw new ArgumentException("The ActivityDefinition ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE ActivityDefinition
                SET
                    ActivityTypeId = @ActivityTypeId,
                    ActivityStatusId = @ActivityStatusId,
                    AssignedUserId = @AssignedUserId,
                    AssignedTeamId = @AssignedTeamId,
                    Subject = @Subject,
                    Description = @Description,
                    DueDateOffset = @DueDateOffset,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate,
                    Active = @Active
                WHERE ActivityDefinitionId = @Id
                AND Active = 1";

            var affectedRows = await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            new
                            {
                                entity.Id,
                                entity.ActivityTypeId,
                                entity.ActivityStatusId,
                                entity.AssignedUserId,
                                entity.AssignedTeamId,
                                entity.Subject,
                                entity.Description,
                                entity.DueDateOffset,
                                entity.ModifiedBy,
                                entity.ModifiedDate,
                                entity.Active
                            },
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating ActivityDefinition with ID {entity.Id}", ActivityDefinitionId = entity.Id, EntityType = nameof(ActivityDefinition) },
                cancellationToken);

            if (affectedRows == 0)
            {
                _logger.LogWarning("No ActivityDefinition was updated for ID {ActivityDefinitionId}", entity.Id);
            }

            return entity;
        }

        /// <summary>
        /// Gets all activity definitions from the repository.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of all activity definitions in the repository.</returns>
        public override async Task<IEnumerable<ActivityDefinition>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ActivityDefinition>>(
                async (connection) =>
                {
                    var result = await connection.QueryAsync<ActivityDefinition>(
                        new CommandDefinition(
                            $"SELECT {string.Join(", ", SelectColumns)} FROM {TableName} WHERE Active = 1",
                            cancellationToken: cancellationToken));

                    return result ?? Enumerable.Empty<ActivityDefinition>();
                },
                "GetAllAsync",
                new { ErrorMessage = "Error getting all ActivityDefinitions", EntityType = nameof(ActivityDefinition) },
                cancellationToken);
        }

        /// <summary>
        /// Gets an activity definition by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the activity definition.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The activity definition if found, otherwise null.</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty.</exception>
        public override async Task<ActivityDefinition?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The ActivityDefinition ID cannot be empty", nameof(id));

            return await ExecuteWithResilienceAndLoggingAsync<ActivityDefinition?>(
                async (connection) =>
                {
                    var result = await connection.QueryFirstOrDefaultAsync<ActivityDefinition>(
                        new CommandDefinition(
                            $"SELECT {string.Join(", ", SelectColumns)} FROM {TableName} WHERE {IdColumnName} = @Id AND Active = 1",
                            new { Id = id },
                            cancellationToken: cancellationToken));

                    return result;
                },
                "GetByIdAsync",
                new { ErrorMessage = $"Error getting ActivityDefinition with ID {id}", ActivityDefinitionId = id, EntityType = nameof(ActivityDefinition) },
                cancellationToken);
        }

        /// <summary>
        /// Gets an activity definition by its name.
        /// </summary>
        /// <param name="name">The name of the activity definition.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The activity definition if found, otherwise null.</returns>
        /// <exception cref="ArgumentException">Thrown when name is null or empty.</exception>
        public async Task<ActivityDefinition?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be null or empty", nameof(name));

            return await ExecuteWithResilienceAndLoggingAsync<ActivityDefinition?>(
                async (connection) =>
                {
                    var result = await connection.QueryFirstOrDefaultAsync<ActivityDefinition>(
                        new CommandDefinition(
                            $"SELECT {string.Join(", ", SelectColumns)} FROM {TableName} WHERE Subject = @Name AND Active = 1",
                            new { Name = name },
                            cancellationToken: cancellationToken));

                    return result;
                },
                "GetByNameAsync",
                new { ErrorMessage = $"Error getting ActivityDefinition with name {name}", Name = name, EntityType = nameof(ActivityDefinition) },
                cancellationToken);
        }

        /// <summary>
        /// Gets activity definitions by their activity type.
        /// </summary>
        /// <param name="activityTypeId">The activity type identifier.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of activity definitions of the specified type.</returns>
        /// <exception cref="ArgumentException">Thrown when activityTypeId is empty.</exception>
        public async Task<IEnumerable<ActivityDefinition>> GetByActivityTypeAsync(Guid activityTypeId, CancellationToken cancellationToken = default)
        {
            if (activityTypeId == Guid.Empty) throw new ArgumentException("The ActivityType ID cannot be empty", nameof(activityTypeId));

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ActivityDefinition>>(
                async (connection) =>
                {
                    var result = await connection.QueryAsync<ActivityDefinition>(
                        new CommandDefinition(
                            $"SELECT {string.Join(", ", SelectColumns)} FROM {TableName} WHERE ActivityTypeId = @ActivityTypeId AND Active = 1",
                            new { ActivityTypeId = activityTypeId },
                            cancellationToken: cancellationToken));

                    return result ?? Enumerable.Empty<ActivityDefinition>();
                },
                "GetByActivityTypeAsync",
                new { ErrorMessage = $"Error getting ActivityDefinitions with ActivityTypeId {activityTypeId}", ActivityTypeId = activityTypeId, EntityType = nameof(ActivityDefinition) },
                cancellationToken);
        }

        /// <summary>
        /// Gets activity definitions associated with a specific workflow.
        /// </summary>
        /// <param name="workflowId">The unique identifier of the workflow.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of activity definitions associated with the specified workflow.</returns>
        /// <exception cref="ArgumentException">Thrown when workflowId is empty.</exception>
        public async Task<IEnumerable<ActivityDefinition>> GetByWorkflowIdAsync(Guid workflowId, CancellationToken cancellationToken = default)
        {
            if (workflowId == Guid.Empty) throw new ArgumentException("The Workflow ID cannot be empty", nameof(workflowId));

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ActivityDefinition>>(
                async (connection) =>
                {
                    const string sql = @"
                        SELECT ad.* FROM ActivityDefinition ad
                        INNER JOIN WorkflowActivityDefinition wad ON ad.ActivityDefinitionId = wad.ActivityDefinitionId
                        WHERE wad.WorkflowId = @WorkflowId AND ad.Active = 1 AND wad.Active = 1";

                    var result = await connection.QueryAsync<ActivityDefinition>(
                        new CommandDefinition(
                            sql,
                            new { WorkflowId = workflowId },
                            cancellationToken: cancellationToken));

                    return result ?? Enumerable.Empty<ActivityDefinition>();
                },
                "GetByWorkflowIdAsync",
                new { ErrorMessage = $"Error getting ActivityDefinitions with WorkflowId {workflowId}", WorkflowId = workflowId, EntityType = nameof(ActivityDefinition) },
                cancellationToken);
        }

        /// <summary>
        /// Gets activity definitions created by a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of activity definitions created by the specified user.</returns>
        /// <exception cref="ArgumentException">Thrown when userId is empty.</exception>
        public async Task<IEnumerable<ActivityDefinition>> GetByCreatedByAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty) throw new ArgumentException("The User ID cannot be empty", nameof(userId));

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ActivityDefinition>>(
                async (connection) =>
                {
                    var result = await connection.QueryAsync<ActivityDefinition>(
                        new CommandDefinition(
                            $"SELECT {string.Join(", ", SelectColumns)} FROM {TableName} WHERE CreatedBy = @UserId AND Active = 1",
                            new { UserId = userId },
                            cancellationToken: cancellationToken));

                    return result ?? Enumerable.Empty<ActivityDefinition>();
                },
                "GetByCreatedByAsync",
                new { ErrorMessage = $"Error getting ActivityDefinitions created by user {userId}", UserId = userId, EntityType = nameof(ActivityDefinition) },
                cancellationToken);
        }
    }
}