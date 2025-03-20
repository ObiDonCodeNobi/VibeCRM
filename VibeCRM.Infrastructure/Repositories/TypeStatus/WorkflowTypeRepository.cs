using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing WorkflowType entities
    /// </summary>
    public class WorkflowTypeRepository : BaseRepository<WorkflowType, Guid>, IWorkflowTypeRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "WorkflowType";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "WorkflowTypeId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "WorkflowTypeId", "Type", "Description", "OrdinalPosition",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Base SELECT query for WorkflowType entities
        /// </summary>
        private const string BaseSelectQuery = @"
            SELECT wt.WorkflowTypeId AS Id, wt.Type, wt.Description, wt.OrdinalPosition,
                   wt.CreatedBy, wt.CreatedDate, wt.ModifiedBy, wt.ModifiedDate, wt.Active
            FROM WorkflowType wt
            WHERE wt.Active = 1";

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTypeRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public WorkflowTypeRepository(ISQLConnectionFactory connectionFactory, ILogger<WorkflowTypeRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets workflow types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of workflow types ordered by their ordinal position</returns>
        public async Task<IEnumerable<WorkflowType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default)
        {
            string sql = $@"{BaseSelectQuery}
                ORDER BY wt.OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<WorkflowType>>(
                async connection => await connection.QueryAsync<WorkflowType>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetByOrdinalPositionAsync",
                new { },
                cancellationToken);
        }

        /// <summary>
        /// Gets workflow types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of workflow types with the specified type name</returns>
        public async Task<IEnumerable<WorkflowType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default)
        {
            string sql = $@"{BaseSelectQuery}
                AND wt.Type = @Type
                ORDER BY wt.OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<WorkflowType>>(
                async connection => await connection.QueryAsync<WorkflowType>(
                    new CommandDefinition(
                        sql,
                        new { Type = type },
                        cancellationToken: cancellationToken)),
                "GetByTypeAsync",
                new { Type = type },
                cancellationToken);
        }

        /// <summary>
        /// Gets the default workflow type
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default workflow type, or null if no workflow types exist</returns>
        public async Task<WorkflowType?> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            // Typically the default type would have the lowest ordinal position
            string sql = $@"{BaseSelectQuery}
                ORDER BY wt.OrdinalPosition ASC
                OFFSET 0 ROWS
                FETCH NEXT 1 ROWS ONLY";

            return await ExecuteWithResilienceAndLoggingAsync<WorkflowType?>(
                async connection => await connection.QueryFirstOrDefaultAsync<WorkflowType>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetDefaultAsync",
                new { },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new workflow type to the database
        /// </summary>
        /// <param name="entity">The workflow type entity to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added workflow type with its assigned ID</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<WorkflowType> AddAsync(WorkflowType entity, CancellationToken cancellationToken = default)
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
                INSERT INTO WorkflowType (
                    WorkflowTypeId,
                    Type,
                    Description,
                    OrdinalPosition,
                    CreatedBy,
                    CreatedDate,
                    ModifiedBy,
                    ModifiedDate,
                    Active
                ) VALUES (
                    @WorkflowTypeId,
                    @Type,
                    @Description,
                    @OrdinalPosition,
                    @CreatedBy,
                    @CreatedDate,
                    @ModifiedBy,
                    @ModifiedDate,
                    @Active
                )";

            var parameters = new
            {
                WorkflowTypeId = entity.Id,
                entity.Type,
                entity.Description,
                entity.OrdinalPosition,
                entity.CreatedBy,
                entity.CreatedDate,
                entity.ModifiedBy,
                entity.ModifiedDate,
                entity.Active
            };

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding {typeof(WorkflowType).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(WorkflowType) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing workflow type in the database
        /// </summary>
        /// <param name="entity">The workflow type entity to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated workflow type</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<WorkflowType> UpdateAsync(WorkflowType entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE WorkflowType
                SET Type = @Type,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate,
                    Active = @Active
                WHERE WorkflowTypeId = @WorkflowTypeId
                AND Active = 1";

            var parameters = new
            {
                WorkflowTypeId = entity.Id,
                entity.Type,
                entity.Description,
                entity.OrdinalPosition,
                entity.ModifiedBy,
                entity.ModifiedDate,
                entity.Active
            };

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating {typeof(WorkflowType).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(WorkflowType) },
                cancellationToken);

            return entity;
        }
    }
}