using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing ProductGroup entities
    /// </summary>
    public class ProductGroupRepository : BaseRepository<ProductGroup, Guid>, IProductGroupRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "ProductGroup";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "ProductGroupId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "ProductGroupId", "Name", "Description", "ParentProductGroupId", "DisplayOrder",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Base SELECT query for ProductGroup entities
        /// </summary>
        private const string BaseSelectQuery = @"
            SELECT pg.ProductGroupId, pg.Name, pg.Description, pg.ParentProductGroupId,
                   pg.DisplayOrder, pg.CreatedBy, pg.CreatedDate, pg.ModifiedBy, pg.ModifiedDate,
                   pg.Active
            FROM ProductGroup pg
            WHERE pg.Active = 1";

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductGroupRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public ProductGroupRepository(ISQLConnectionFactory connectionFactory, ILogger<ProductGroupRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new product group to the repository
        /// </summary>
        /// <param name="entity">The product group to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added product group with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when ProductGroupId is empty</exception>
        public override async Task<ProductGroup> AddAsync(ProductGroup entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.ProductGroupId == Guid.Empty) throw new ArgumentException("The ProductGroup ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO ProductGroup (
                    ProductGroupId, Name, Description, ParentProductGroupId, DisplayOrder,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @ProductGroupId, @Name, @Description, @ParentProductGroupId, @DisplayOrder,
                    @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active
                );";

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding ProductGroup with ID {entity.ProductGroupId}", ProductGroupId = entity.ProductGroupId, EntityType = nameof(ProductGroup) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing product group in the repository
        /// </summary>
        /// <param name="entity">The product group to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated product group</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when ProductGroupId is empty</exception>
        public override async Task<ProductGroup> UpdateAsync(ProductGroup entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.ProductGroupId == Guid.Empty) throw new ArgumentException("The ProductGroup ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE ProductGroup SET
                    Name = @Name,
                    Description = @Description,
                    ParentProductGroupId = @ParentProductGroupId,
                    DisplayOrder = @DisplayOrder,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                WHERE ProductGroupId = @ProductGroupId AND Active = 1;";

            int rowsAffected = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating ProductGroup with ID {entity.ProductGroupId}", ProductGroupId = entity.ProductGroupId, EntityType = nameof(ProductGroup) },
                cancellationToken);

            if (rowsAffected == 0)
            {
                _logger.LogWarning("ProductGroup with ID {ProductGroupId} not found for update or already inactive", entity.ProductGroupId);
            }

            return entity;
        }

        /// <summary>
        /// Gets a product group by its name
        /// </summary>
        /// <param name="name">The name of the product group</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The product group if found, otherwise null</returns>
        public async Task<ProductGroup?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND pg.Name = @Name";

            return await ExecuteWithResilienceAndLoggingAsync<ProductGroup?>(
                async connection => await connection.QuerySingleOrDefaultAsync<ProductGroup>(
                    new CommandDefinition(
                        sql,
                        new { Name = name },
                        cancellationToken: cancellationToken)),
                "GetByNameAsync",
                new { Name = name },
                cancellationToken);
        }

        /// <summary>
        /// Gets all product groups that contain a specific product
        /// </summary>
        /// <param name="productId">The unique identifier of the product</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of product groups containing the specified product</returns>
        public async Task<IEnumerable<ProductGroup>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT pg.ProductGroupId, pg.Name, pg.Description, pg.ParentProductGroupId,
                       pg.DisplayOrder, pg.CreatedBy, pg.CreatedDate, pg.ModifiedBy, pg.ModifiedDate,
                       pg.Active
                FROM ProductGroup pg
                JOIN Product_ProductGroup ppg ON pg.ProductGroupId = ppg.ProductGroupId
                WHERE ppg.ProductId = @ProductId
                AND pg.Active = 1
                AND ppg.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ProductGroup>>(
                async connection => await connection.QueryAsync<ProductGroup>(
                    new CommandDefinition(
                        sql,
                        new { ProductId = productId },
                        cancellationToken: cancellationToken)),
                "GetByProductIdAsync",
                new { ProductId = productId },
                cancellationToken);
        }

        /// <summary>
        /// Gets all product groups within a specific parent group
        /// </summary>
        /// <param name="parentGroupId">The unique identifier of the parent product group</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of product groups that are children of the specified parent group</returns>
        public async Task<IEnumerable<ProductGroup>> GetByParentGroupIdAsync(Guid parentGroupId, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND pg.ParentProductGroupId = @ParentGroupId ORDER BY pg.DisplayOrder";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ProductGroup>>(
                async connection => await connection.QueryAsync<ProductGroup>(
                    new CommandDefinition(
                        sql,
                        new { ParentGroupId = parentGroupId },
                        cancellationToken: cancellationToken)),
                "GetByParentGroupIdAsync",
                new { ParentGroupId = parentGroupId },
                cancellationToken);
        }

        /// <summary>
        /// Gets all root-level product groups (those with no parent)
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of root-level product groups</returns>
        public async Task<IEnumerable<ProductGroup>> GetRootGroupsAsync(CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND pg.ParentProductGroupId IS NULL ORDER BY pg.DisplayOrder";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<ProductGroup>>(
                async connection => await connection.QueryAsync<ProductGroup>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetRootGroupsAsync",
                null,
                cancellationToken);
        }
    }
}