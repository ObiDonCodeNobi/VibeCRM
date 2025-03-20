using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing Product entities
    /// </summary>
    public class ProductRepository : BaseRepository<Product, Guid>, IProductRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Product";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "ProductId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "ProductId", "ProductTypeId", "Name", "Description",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Base SELECT query for Product entities
        /// </summary>
        private const string BaseSelectQuery = @"
            SELECT p.ProductId AS Id, p.ProductTypeId, p.Name, p.Description,
                   p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
            FROM Product p
            WHERE p.Active = 1";

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public ProductRepository(ISQLConnectionFactory connectionFactory, ILogger<ProductRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new product to the repository
        /// </summary>
        /// <param name="entity">The product to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added product with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when ProductId is empty</exception>
        public override async Task<Product> AddAsync(Product entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.ProductId == Guid.Empty) throw new ArgumentException("The Product ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO Product (
                    ProductId, ProductTypeId, Name, Description,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @ProductId, @ProductTypeId, @Name, @Description,
                    @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, 1
                );";

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding Product with ID {entity.ProductId}", ProductId = entity.ProductId, EntityType = nameof(Product) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing product in the repository
        /// </summary>
        /// <param name="entity">The product to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated product</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when ProductId is empty</exception>
        public override async Task<Product> UpdateAsync(Product entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.ProductId == Guid.Empty) throw new ArgumentException("The Product ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE Product SET
                    ProductTypeId = @ProductTypeId,
                    Name = @Name,
                    Description = @Description,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                WHERE ProductId = @ProductId AND Active = 1;";

            int rowsAffected = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating Product with ID {entity.ProductId}", ProductId = entity.ProductId, EntityType = nameof(Product) },
                cancellationToken);

            if (rowsAffected == 0)
            {
                _logger.LogWarning("Product with ID {ProductId} not found for update or already inactive", entity.ProductId);
            }

            return entity;
        }

        /// <summary>
        /// Gets a product by its name
        /// </summary>
        /// <param name="name">The name of the product</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The product if found, otherwise null</returns>
        public async Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND p.Name = @Name";

            return await ExecuteWithResilienceAndLoggingAsync<Product?>(
                async connection => await connection.QuerySingleOrDefaultAsync<Product>(
                    new CommandDefinition(
                        sql,
                        new { Name = name },
                        cancellationToken: cancellationToken)),
                "GetByNameAsync",
                new { Name = name },
                cancellationToken);
        }

        /// <summary>
        /// Gets all products of a specific product type
        /// </summary>
        /// <param name="productTypeId">The unique identifier of the product type</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of products of the specified type</returns>
        public async Task<IEnumerable<Product>> GetByProductTypeIdAsync(Guid productTypeId, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND p.ProductTypeId = @ProductTypeId";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Product>>(
                async connection => await connection.QueryAsync<Product>(
                    new CommandDefinition(
                        sql,
                        new { ProductTypeId = productTypeId },
                        cancellationToken: cancellationToken)),
                "GetByProductTypeIdAsync",
                new { ProductTypeId = productTypeId },
                cancellationToken);
        }

        /// <summary>
        /// Gets all products that belong to a specific product group
        /// </summary>
        /// <param name="productGroupId">The unique identifier of the product group</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of products that belong to the specified product group</returns>
        public async Task<IEnumerable<Product>> GetByProductGroupIdAsync(Guid productGroupId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT p.ProductId AS Id, p.ProductTypeId, p.Name, p.Description,
                       p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Product p
                JOIN ProductGroup_Product pgp ON p.ProductId = pgp.ProductId
                WHERE pgp.ProductGroupId = @ProductGroupId
                  AND p.Active = 1
                  AND pgp.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Product>>(
                async connection => await connection.QueryAsync<Product>(
                    new CommandDefinition(
                        sql,
                        new { ProductGroupId = productGroupId },
                        cancellationToken: cancellationToken)),
                "GetByProductGroupIdAsync",
                new { ProductGroupId = productGroupId },
                cancellationToken);
        }

        /// <summary>
        /// Gets a product by its unique identifier with all related entities loaded
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The product with all related entities if found, otherwise null</returns>
        public async Task<Product?> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await GetByIdAsync(id, cancellationToken);
            if (product == null) return null;

            // Load all related entities
            await LoadProductTypeAsync(product, cancellationToken);
            await LoadQuoteLineItemsAsync(product, cancellationToken);
            await LoadSalesOrderLineItemsAsync(product, cancellationToken);
            await LoadProductGroupsAsync(product, cancellationToken);

            return product;
        }

        /// <summary>
        /// Loads the product type for a product
        /// </summary>
        /// <param name="product">The product to load the product type for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task LoadProductTypeAsync(Product product, CancellationToken cancellationToken = default)
        {
            if (product.ProductTypeId == Guid.Empty) return;

            const string sql = @"
                SELECT
                    ProductTypeId, Type, Description, OrdinalPosition,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                FROM ProductType
                WHERE ProductTypeId = @ProductTypeId AND Active = 1";

            var productType = await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QuerySingleOrDefaultAsync<ProductType>(
                        new CommandDefinition(
                            sql,
                            new { ProductTypeId = product.ProductTypeId },
                            cancellationToken: cancellationToken)),
                "LoadProductTypeAsync",
                new { ProductId = product.ProductId, ProductTypeId = product.ProductTypeId },
                cancellationToken);

            product.ProductType = productType;
        }

        /// <summary>
        /// Loads the quote line items for a product
        /// </summary>
        /// <param name="product">The product to load the quote line items for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task LoadQuoteLineItemsAsync(Product product, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT
                    QuoteLineItemId, QuoteId, ProductId, ServiceId, Description,
                    Quantity, UnitPrice, DiscountPercentage, DiscountAmount, TaxPercentage,
                    LineNumber, Notes, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                FROM QuoteLineItem
                WHERE ProductId = @ProductId AND Active = 1
                ORDER BY LineNumber";

            var quoteLineItems = await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<QuoteLineItem>(
                        new CommandDefinition(
                            sql,
                            new { ProductId = product.ProductId },
                            cancellationToken: cancellationToken)),
                "LoadQuoteLineItemsAsync",
                new { ProductId = product.ProductId },
                cancellationToken);

            foreach (var lineItem in quoteLineItems)
            {
                lineItem.Product = product;
                product.QuoteLineItems.Add(lineItem);
            }
        }

        /// <summary>
        /// Loads the sales order line items for a product
        /// </summary>
        /// <param name="product">The product to load the sales order line items for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task LoadSalesOrderLineItemsAsync(Product product, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT
                    SalesOrderLineItemId, SalesOrderId, ProductId, ServiceId, QuoteLineItemId,
                    Description, Quantity, UnitPrice, DiscountPercentage, DiscountAmount,
                    TaxPercentage, LineNumber, ShipDate, Notes,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                FROM SalesOrderLineItem
                WHERE ProductId = @ProductId AND Active = 1
                ORDER BY LineNumber";

            var salesOrderLineItems = await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<SalesOrderLineItem>(
                        new CommandDefinition(
                            sql,
                            new { ProductId = product.ProductId },
                            cancellationToken: cancellationToken)),
                "LoadSalesOrderLineItemsAsync",
                new { ProductId = product.ProductId },
                cancellationToken);

            foreach (var lineItem in salesOrderLineItems)
            {
                lineItem.Product = product;
                product.SalesOrderLineItems.Add(lineItem);
            }
        }

        /// <summary>
        /// Loads the product groups for a product
        /// </summary>
        /// <param name="product">The product to load the product groups for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task LoadProductGroupsAsync(Product product, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT
                    pg.ProductGroupId, pg.Name, pg.Description, pg.ParentProductGroupId,
                    pg.DisplayOrder, pg.CreatedBy, pg.CreatedDate, pg.ModifiedBy, pg.ModifiedDate, pg.Active
                FROM ProductGroup pg
                JOIN ProductGroup_Product pgp ON pg.ProductGroupId = pgp.ProductGroupId
                WHERE pgp.ProductId = @ProductId
                  AND pg.Active = 1
                  AND pgp.Active = 1
                ORDER BY pg.DisplayOrder";

            var productGroups = await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<ProductGroup>(
                        new CommandDefinition(
                            sql,
                            new { ProductId = product.ProductId },
                            cancellationToken: cancellationToken)),
                "LoadProductGroupsAsync",
                new { ProductId = product.ProductId },
                cancellationToken);

            foreach (var group in productGroups)
            {
                product.ProductGroups.Add(group);
            }
        }
    }
}