using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing ActivityStatus entities
    /// </summary>
    public class ActivityStatusRepository : BaseRepository<ActivityStatus, Guid>, IActivityStatusRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "ActivityStatus";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "ActivityStatusId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "ActivityStatusId", "Status", "Description", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Predefined completed status names for consistent business rule application
        /// </summary>
        private static readonly string[] CompletedStatuses =
        {
            "Completed", "Done", "Finished", "Closed"
        };

        /// <summary>
        /// Initializes a new instance of the ActivityStatusRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        public ActivityStatusRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<ActivityStatusRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets activity statuses ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity statuses ordered by their ordinal position</returns>
        public async Task<IEnumerable<ActivityStatus>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ActivityStatus>>(
                async (connection) => await connection.QueryAsync<ActivityStatus>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetByOrdinalPositionAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets activity statuses by status name
        /// </summary>
        /// <param name="status">The status name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity statuses with the specified status name</returns>
        public async Task<IEnumerable<ActivityStatus>> GetByStatusAsync(string status, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Status = @Status
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ActivityStatus>>(
                async (connection) => await connection.QueryAsync<ActivityStatus>(
                    new CommandDefinition(
                        sql,
                        new { Status = status },
                        cancellationToken: cancellationToken)),
                "GetByStatusAsync",
                new { Status = status, TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets the default activity status
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default activity status, or null if no activity statuses exist</returns>
        public async Task<ActivityStatus?> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            // Typically the default status would have the lowest ordinal position
            // or a specific name like "Not Started" or "Planned"
            var sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<ActivityStatus?>(
                async (connection) => await connection.QueryFirstOrDefaultAsync<ActivityStatus>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetDefaultAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets completed activity statuses
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity statuses that represent completed activities</returns>
        public async Task<IEnumerable<ActivityStatus>> GetCompletedStatusesAsync(CancellationToken cancellationToken = default)
        {
            // Create SQL IN clause parameters dynamically to avoid SQL injection
            var parameters = new DynamicParameters();
            var sqlBuilder = new System.Text.StringBuilder();

            sqlBuilder.AppendLine($"SELECT {string.Join(", ", SelectColumns)}");
            sqlBuilder.AppendLine($"FROM {TableName}");
            sqlBuilder.AppendLine("WHERE Active = 1");

            // Only add the IN clause if we have completed statuses defined
            if (CompletedStatuses.Length > 0)
            {
                sqlBuilder.Append("AND Status IN (");

                for (int i = 0; i < CompletedStatuses.Length; i++)
                {
                    string paramName = $"@Status{i}";
                    parameters.Add(paramName, CompletedStatuses[i]);

                    sqlBuilder.Append(paramName);
                    if (i < CompletedStatuses.Length - 1)
                        sqlBuilder.Append(", ");
                }

                sqlBuilder.AppendLine(")");
            }

            sqlBuilder.AppendLine("ORDER BY OrdinalPosition ASC");

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ActivityStatus>>(
                async (connection) => await connection.QueryAsync<ActivityStatus>(
                    new CommandDefinition(
                        sqlBuilder.ToString(),
                        parameters,
                        cancellationToken: cancellationToken)),
                "GetCompletedStatusesAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new activity status
        /// </summary>
        /// <param name="entity">The activity status to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added activity status</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<ActivityStatus> AddAsync(ActivityStatus entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Ensure ID is set
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            // Set audit fields if not already set
            if (entity.CreatedDate == default)
            {
                entity.CreatedDate = DateTime.UtcNow;
            }

            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = entity.CreatedDate;
            }

            // Ensure Active is set
            entity.Active = true;

            var sql = @"
                INSERT INTO ActivityStatus (
                    ActivityStatusId,
                    Status,
                    Description,
                    OrdinalPosition,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @ActivityStatusId,
                    @Status,
                    @Description,
                    @OrdinalPosition,
                    @CreatedDate,
                    @CreatedBy,
                    @ModifiedDate,
                    @ModifiedBy,
                    @Active
                )";

            var parameters = new
            {
                ActivityStatusId = entity.Id,
                entity.Status,
                entity.Description,
                entity.OrdinalPosition,
                entity.CreatedDate,
                entity.CreatedBy,
                entity.ModifiedDate,
                entity.ModifiedBy,
                entity.Active
            };

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding {typeof(ActivityStatus).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(ActivityStatus) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing activity status
        /// </summary>
        /// <param name="entity">The activity status to update</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated activity status</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<ActivityStatus> UpdateAsync(ActivityStatus entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE ActivityStatus
                SET Status = @Status,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE ActivityStatusId = @ActivityStatusId
                AND Active = 1";

            var parameters = new
            {
                ActivityStatusId = entity.Id,
                entity.Status,
                entity.Description,
                entity.OrdinalPosition,
                entity.ModifiedDate,
                entity.ModifiedBy,
                entity.Active
            };

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating {typeof(ActivityStatus).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(ActivityStatus) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Checks if an activity status with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier of the activity status to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if an activity status with the specified identifier exists; otherwise, false</returns>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM ActivityStatus
                    WHERE ActivityStatusId = @Id
                    AND Active = 1
                ) THEN 1 ELSE 0 END";

            return await ExecuteWithResilienceAndLoggingAsync<bool>(
                async (connection) => await connection.ExecuteScalarAsync<bool>(
                    new CommandDefinition(
                        sql,
                        new { Id = id },
                        cancellationToken: cancellationToken)),
                "ExistsAsync",
                new { Id = id, TableName },
                cancellationToken);
        }
    }
}