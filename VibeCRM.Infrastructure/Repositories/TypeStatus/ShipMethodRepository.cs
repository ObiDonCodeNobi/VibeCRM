using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing ShipMethod entities
    /// </summary>
    public class ShipMethodRepository : BaseRepository<ShipMethod, Guid>, IShipMethodRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "ShipMethod";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "ShipMethodId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "ShipMethodId", "Method", "Description", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the ShipMethodRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        public ShipMethodRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<ShipMethodRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets shipping methods ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of shipping methods ordered by their ordinal position</returns>
        public async Task<IEnumerable<ShipMethod>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ShipMethod>>(
                async (connection) => await connection.QueryAsync<ShipMethod>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetByOrdinalPositionAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets shipping methods by method name
        /// </summary>
        /// <param name="method">The method name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of shipping methods with the specified method name</returns>
        public async Task<IEnumerable<ShipMethod>> GetByMethodAsync(string method, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Method = @Method
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ShipMethod>>(
                async (connection) => await connection.QueryAsync<ShipMethod>(
                    new CommandDefinition(
                        sql,
                        new { Method = method },
                        cancellationToken: cancellationToken)),
                "GetByMethodAsync",
                new { Method = method, TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets the default shipping method
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default shipping method, or null if no shipping methods exist</returns>
        public async Task<ShipMethod?> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            // Typically the default method would have the lowest ordinal position
            var sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<ShipMethod?>(
                async (connection) => await connection.QueryFirstOrDefaultAsync<ShipMethod>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetDefaultAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new shipping method
        /// </summary>
        /// <param name="entity">The shipping method to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added shipping method</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<ShipMethod> AddAsync(ShipMethod entity, CancellationToken cancellationToken = default)
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
                INSERT INTO ShipMethod (
                    ShipMethodId,
                    Method,
                    Description,
                    OrdinalPosition,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @ShipMethodId,
                    @Method,
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
                ShipMethodId = entity.Id,
                entity.Method,
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
                new { ErrorMessage = $"Error adding {typeof(ShipMethod).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(ShipMethod) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing shipping method
        /// </summary>
        /// <param name="entity">The shipping method to update</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated shipping method</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<ShipMethod> UpdateAsync(ShipMethod entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE ShipMethod
                SET Method = @Method,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE ShipMethodId = @ShipMethodId
                AND Active = 1";

            var parameters = new
            {
                ShipMethodId = entity.Id,
                entity.Method,
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
                new { ErrorMessage = $"Error updating {typeof(ShipMethod).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(ShipMethod) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Checks if a shipping method with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier of the shipping method to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a shipping method with the specified identifier exists; otherwise, false</returns>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM ShipMethod
                    WHERE ShipMethodId = @Id
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