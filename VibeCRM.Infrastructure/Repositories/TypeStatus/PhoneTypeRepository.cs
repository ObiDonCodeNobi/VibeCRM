using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing PhoneType entities
    /// </summary>
    public class PhoneTypeRepository : BaseTypeStatusRepository<PhoneType>, IPhoneTypeRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "PhoneType";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "PhoneTypeId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "PhoneTypeId", "Type", "Description", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the PhoneTypeRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public PhoneTypeRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PhoneTypeRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new phone type
        /// </summary>
        /// <param name="entity">The phone type to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added phone type</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<PhoneType> AddAsync(PhoneType entity, CancellationToken cancellationToken = default)
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
                INSERT INTO PhoneType (
                    PhoneTypeId,
                    Type,
                    Description,
                    OrdinalPosition,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @PhoneTypeId,
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
                PhoneTypeId = entity.Id,
                entity.Type,
                entity.Description,
                entity.OrdinalPosition,
                entity.CreatedDate,
                entity.CreatedBy,
                entity.ModifiedDate,
                entity.ModifiedBy,
                entity.Active
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    sql, parameters),
                "AddAsync",
                new { EntityId = entity.Id, EntityType = typeof(PhoneType).Name },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing phone type
        /// </summary>
        /// <param name="entity">The phone type to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated phone type</returns>
        public override async Task<PhoneType> UpdateAsync(PhoneType entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE PhoneType
                SET Type = @Type,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE PhoneTypeId = @PhoneTypeId";

            var parameters = new
            {
                PhoneTypeId = entity.Id,
                entity.Type,
                entity.Description,
                entity.OrdinalPosition,
                entity.ModifiedDate,
                entity.ModifiedBy,
                entity.Active
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    sql, parameters),
                "UpdateAsync",
                new { EntityId = entity.Id, EntityType = typeof(PhoneType).Name },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Checks if a phone type with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a phone type with the specified ID exists, otherwise false</returns>
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

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<bool>(
                    sql, new { Id = id }),
                "ExistsAsync",
                new { Id = id, EntityType = typeof(PhoneType).Name },
                cancellationToken);
        }
    }
}