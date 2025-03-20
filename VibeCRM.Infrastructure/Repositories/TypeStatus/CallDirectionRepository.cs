using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing CallDirection entities
    /// </summary>
    public class CallDirectionRepository : BaseRepository<CallDirection, Guid>, ICallDirectionRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "CallDirection";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "CallDirectionId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "CallDirectionId", "Direction", "Description", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the CallDirectionRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        public CallDirectionRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<CallDirectionRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets call directions ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of call directions ordered by their ordinal position</returns>
        public async Task<IEnumerable<CallDirection>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<CallDirection>>(
                async (connection) => await connection.QueryAsync<CallDirection>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetByOrdinalPositionAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets call directions by direction name
        /// </summary>
        /// <param name="direction">The direction name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of call directions with the specified direction name</returns>
        public async Task<IEnumerable<CallDirection>> GetByDirectionAsync(string direction, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Direction = @Direction
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<CallDirection>>(
                async (connection) => await connection.QueryAsync<CallDirection>(
                    new CommandDefinition(
                        sql,
                        new { Direction = direction },
                        cancellationToken: cancellationToken)),
                "GetByDirectionAsync",
                new { Direction = direction, TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets the default call direction
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default call direction, or null if no call directions exist</returns>
        public async Task<CallDirection?> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            // Typically the default direction would have the lowest ordinal position
            var sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<CallDirection?>(
                async (connection) => await connection.QueryFirstOrDefaultAsync<CallDirection>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetDefaultAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new call direction
        /// </summary>
        /// <param name="entity">The call direction to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added call direction</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<CallDirection> AddAsync(CallDirection entity, CancellationToken cancellationToken = default)
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
                INSERT INTO CallDirection (
                    CallDirectionId,
                    Direction,
                    Description,
                    OrdinalPosition,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @CallDirectionId,
                    @Direction,
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
                CallDirectionId = entity.Id,
                entity.Direction,
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
                new { ErrorMessage = $"Error adding {typeof(CallDirection).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(CallDirection) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing call direction
        /// </summary>
        /// <param name="entity">The call direction to update</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated call direction</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<CallDirection> UpdateAsync(CallDirection entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE CallDirection
                SET Direction = @Direction,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE CallDirectionId = @CallDirectionId
                AND Active = 1";

            var parameters = new
            {
                CallDirectionId = entity.Id,
                entity.Direction,
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
                new { ErrorMessage = $"Error updating {typeof(CallDirection).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(CallDirection) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Checks if a call direction with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier of the call direction to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a call direction with the specified identifier exists; otherwise, false</returns>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM CallDirection
                    WHERE CallDirectionId = @Id
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