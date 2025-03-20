using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing CallType entities
    /// </summary>
    public class CallTypeRepository : BaseTypeStatusRepository<CallType>, ICallTypeRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "CallType";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "CallTypeId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "CallTypeId", "Type", "Description", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the CallTypeRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public CallTypeRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<CallTypeRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new call type
        /// </summary>
        /// <param name="entity">The call type to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added call type</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<CallType> AddAsync(CallType entity, CancellationToken cancellationToken = default)
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
                INSERT INTO CallType (
                    CallTypeId,
                    Type,
                    Description,
                    OrdinalPosition,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @CallTypeId,
                    @Type,
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
                CallTypeId = entity.Id,
                entity.Type,
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
                new { ErrorMessage = $"Error adding {typeof(CallType).Name}", EntityType = nameof(CallType), EntityId = entity.Id.ToString() },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing call type
        /// </summary>
        /// <param name="entity">The call type to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated call type</returns>
        public override async Task<CallType> UpdateAsync(CallType entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE CallType
                SET Type = @Type,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE CallTypeId = @CallTypeId";

            var parameters = new
            {
                CallTypeId = entity.Id,
                entity.Type,
                entity.Description,
                entity.OrdinalPosition,
                entity.ModifiedDate,
                entity.ModifiedBy,
                entity.Active
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async (connection) => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating {typeof(CallType).Name} with ID {entity.Id}", EntityType = nameof(CallType), EntityId = entity.Id.ToString() },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Checks if a call type with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a call type with the specified ID exists, otherwise false</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM {TableName}
                    WHERE {IdColumnName} = @Id
                    AND Active = 1
                ) THEN 1 ELSE 0 END";

            return await ExecuteWithResilienceAndLoggingAsync<bool>(
                async (connection) => await connection.ExecuteScalarAsync<bool>(
                    new CommandDefinition(
                        sql,
                        new { Id = id },
                        cancellationToken: cancellationToken)),
                "ExistsAsync",
                new { ErrorMessage = $"Error checking existence of {typeof(CallType).Name} with ID {id}", EntityType = nameof(CallType), EntityId = id.ToString() },
                cancellationToken);
        }

        /// <summary>
        /// Gets inbound call types
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of call types for inbound calls</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<IEnumerable<CallType>> GetInboundTypesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Retrieving inbound call types");

            try
            {
                // Since CallType doesn't have IsInbound property, we filter based on naming convention
                // or return all and let the application filter them
                const string sql = @"
                    SELECT *
                    FROM CallType
                    WHERE Active = 1
                    AND (Type LIKE '%Inbound%' OR Type LIKE '%Incoming%' OR Type LIKE '%Received%')
                    ORDER BY OrdinalPosition";

                return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<CallType>>(
                    async connection => await connection.QueryAsync<CallType>(
                        new CommandDefinition(
                            sql,
                            cancellationToken: cancellationToken)),
                    "GetInboundTypesAsync",
                    new { ErrorMessage = "Error retrieving inbound call types" },
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving inbound call types");
                throw new Exception("Error retrieving inbound call types", ex);
            }
        }

        /// <summary>
        /// Gets outbound call types
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of call types for outbound calls</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<IEnumerable<CallType>> GetOutboundTypesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Retrieving outbound call types");

            try
            {
                // Since CallType doesn't have IsOutbound property, we filter based on naming convention
                // or return all and let the application filter them
                const string sql = @"
                    SELECT *
                    FROM CallType
                    WHERE Active = 1
                    AND (Type LIKE '%Outbound%' OR Type LIKE '%Outgoing%' OR Type LIKE '%Made%')
                    ORDER BY OrdinalPosition";

                return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<CallType>>(
                    async connection => await connection.QueryAsync<CallType>(
                        new CommandDefinition(
                            sql,
                            cancellationToken: cancellationToken)),
                    "GetOutboundTypesAsync",
                    new { ErrorMessage = "Error retrieving outbound call types" },
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving outbound call types");
                throw new Exception("Error retrieving outbound call types", ex);
            }
        }
    }
}