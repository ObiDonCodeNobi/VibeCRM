using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing ProductType entities
    /// </summary>
    public class ProductTypeRepository : BaseRepository<ProductType, Guid>, IProductTypeRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "ProductType";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "ProductTypeId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "ProductTypeId", "Type", "Description", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the ProductTypeRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        public ProductTypeRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<ProductTypeRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets product types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of product types ordered by their ordinal position</returns>
        public async Task<IEnumerable<ProductType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ProductType>>(
                async (connection) => await connection.QueryAsync<ProductType>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetByOrdinalPositionAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets product types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of product types with the specified type name</returns>
        public async Task<IEnumerable<ProductType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Type = @Type
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ProductType>>(
                async (connection) => await connection.QueryAsync<ProductType>(
                    new CommandDefinition(
                        sql,
                        new { Type = type },
                        cancellationToken: cancellationToken)),
                "GetByTypeAsync",
                new { Type = type, TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets the default product type
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default product type, or null if no product types exist</returns>
        public async Task<ProductType?> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            // Typically the default type would have the lowest ordinal position
            var sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<ProductType?>(
                async (connection) => await connection.QueryFirstOrDefaultAsync<ProductType>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetDefaultAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new product type
        /// </summary>
        /// <param name="entity">The product type to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added product type</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<ProductType> AddAsync(ProductType entity, CancellationToken cancellationToken = default)
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
                INSERT INTO ProductType (
                    ProductTypeId,
                    Type,
                    Description,
                    OrdinalPosition,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @ProductTypeId,
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
                ProductTypeId = entity.Id,
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
                new { ErrorMessage = $"Error adding {typeof(ProductType).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(ProductType) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing product type
        /// </summary>
        /// <param name="entity">The product type to update</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated product type</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<ProductType> UpdateAsync(ProductType entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE ProductType
                SET Type = @Type,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE ProductTypeId = @ProductTypeId
                AND Active = 1";

            var parameters = new
            {
                ProductTypeId = entity.Id,
                entity.Type,
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
                new { ErrorMessage = $"Error updating {typeof(ProductType).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(ProductType) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Checks if a product type with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier of the product type to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a product type with the specified identifier exists; otherwise, false</returns>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM ProductType
                    WHERE ProductTypeId = @Id
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