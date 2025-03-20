using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing ServiceType entities
    /// </summary>
    public class ServiceTypeRepository : BaseTypeStatusRepository<ServiceType>, IServiceTypeRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "ServiceType";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "ServiceTypeId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "ServiceTypeId", "Type", "Description", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the ServiceTypeRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public ServiceTypeRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<ServiceTypeRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new service type
        /// </summary>
        /// <param name="entity">The service type to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added service type</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<ServiceType> AddAsync(ServiceType entity, CancellationToken cancellationToken = default)
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
                INSERT INTO ServiceType (
                    ServiceTypeId,
                    Type,
                    Description,
                    OrdinalPosition,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @ServiceTypeId,
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
                ServiceTypeId = entity.Id,
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
                new { ErrorMessage = $"Error adding {typeof(ServiceType).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(ServiceType) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing service type
        /// </summary>
        /// <param name="entity">The service type to update</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated service type</returns>
        public override async Task<ServiceType> UpdateAsync(ServiceType entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE ServiceType
                SET Type = @Type,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE ServiceTypeId = @ServiceTypeId";

            var parameters = new
            {
                ServiceTypeId = entity.Id,
                entity.Type,
                entity.Description,
                entity.OrdinalPosition,
                entity.ModifiedDate,
                entity.ModifiedBy,
                entity.Active
            };

            var affectedRows = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating {typeof(ServiceType).Name} with ID {entity.Id}", EntityId = entity.Id.ToString(), EntityType = nameof(ServiceType) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Checks if a service type with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a service type with the specified ID exists, otherwise false</returns>
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

            var count = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { Id = id },
                        cancellationToken: cancellationToken)),
                "ExistsAsync",
                new { ErrorMessage = $"Error checking existence of {typeof(ServiceType).Name} with ID {id}", EntityId = id.ToString(), EntityType = nameof(ServiceType) },
                cancellationToken);

            return count == 1;
        }

        /// <summary>
        /// Gets service types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of service types ordered by their ordinal position</returns>
        public override async Task<IEnumerable<ServiceType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ServiceType>>(
                async connection => await connection.QueryAsync<ServiceType>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetByOrdinalPositionAsync",
                new { EntityType = nameof(ServiceType) },
                cancellationToken);
        }

        /// <summary>
        /// Gets service types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of service types with the specified type name</returns>
        public override async Task<IEnumerable<ServiceType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new ArgumentException("Type cannot be null or whitespace", nameof(type));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Type LIKE @Type
                AND Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ServiceType>>(
                async connection => await connection.QueryAsync<ServiceType>(
                    new CommandDefinition(
                        sql,
                        new { Type = $"%{type}%" },
                        cancellationToken: cancellationToken)),
                "GetByTypeAsync",
                new { Type = type, EntityType = nameof(ServiceType) },
                cancellationToken);
        }

        /// <summary>
        /// Gets the default service type (the one with the lowest ordinal position)
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default service type or null if not found</returns>
        public override async Task<ServiceType?> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<ServiceType?>(
                async connection => await connection.QueryFirstOrDefaultAsync<ServiceType>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetDefaultAsync",
                new { EntityType = nameof(ServiceType) },
                cancellationToken);
        }
    }
}