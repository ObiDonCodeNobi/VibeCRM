using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing SalesOrderLineItem entities
    /// </summary>
    public class SalesOrderLineItemRepository : BaseRepository<SalesOrderLineItem, Guid>, ISalesOrderLineItemRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "SalesOrderLineItem";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "SalesOrderLineItemId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "SalesOrderLineItemId", "SalesOrderId", "ProductId", "ServiceId", "QuoteLineItemId", "Description",
            "Quantity", "UnitPrice", "DiscountPercentage", "DiscountAmount",
            "TaxPercentage", "LineNumber", "ShipDate", "Notes",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Base SELECT query for SalesOrderLineItem entities
        /// </summary>
        private const string BaseSelectQuery = @"
            SELECT soli.SalesOrderLineItemId AS Id, soli.SalesOrderId, soli.ProductId, soli.ServiceId, soli.QuoteLineItemId,
                   soli.Description, soli.Quantity, soli.UnitPrice, soli.DiscountPercentage, soli.DiscountAmount,
                   soli.TaxPercentage, soli.LineNumber, soli.ShipDate, soli.Notes,
                   soli.CreatedBy, soli.CreatedDate, soli.ModifiedBy, soli.ModifiedDate, soli.Active
            FROM SalesOrderLineItem soli
            WHERE soli.Active = 1";

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderLineItemRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public SalesOrderLineItemRepository(ISQLConnectionFactory connectionFactory, ILogger<SalesOrderLineItemRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new sales order line item to the repository
        /// </summary>
        /// <param name="entity">The sales order line item to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added sales order line item with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when SalesOrderLineItemId is empty</exception>
        public override async Task<SalesOrderLineItem> AddAsync(SalesOrderLineItem entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.SalesOrderLineItemId == Guid.Empty) throw new ArgumentException("The SalesOrderLineItem ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO SalesOrderLineItem (
                    SalesOrderLineItemId, SalesOrderId, ProductId, ServiceId, QuoteLineItemId, Description,
                    Quantity, UnitPrice, DiscountPercentage, DiscountAmount,
                    TaxPercentage, LineNumber, ShipDate, Notes,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @SalesOrderLineItemId, @SalesOrderId, @ProductId, @ServiceId, @QuoteLineItemId, @Description,
                    @Quantity, @UnitPrice, @DiscountPercentage, @DiscountAmount,
                    @TaxPercentage, @LineNumber, @ShipDate, @Notes,
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
                new { ErrorMessage = $"Error adding SalesOrderLineItem with ID {entity.SalesOrderLineItemId}", SalesOrderLineItemId = entity.SalesOrderLineItemId, EntityType = nameof(SalesOrderLineItem) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing sales order line item in the repository
        /// </summary>
        /// <param name="entity">The sales order line item to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated sales order line item</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when SalesOrderLineItemId is empty</exception>
        public override async Task<SalesOrderLineItem> UpdateAsync(SalesOrderLineItem entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.SalesOrderLineItemId == Guid.Empty) throw new ArgumentException("The SalesOrderLineItem ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE SalesOrderLineItem SET
                    SalesOrderId = @SalesOrderId,
                    ProductId = @ProductId,
                    ServiceId = @ServiceId,
                    QuoteLineItemId = @QuoteLineItemId,
                    Description = @Description,
                    Quantity = @Quantity,
                    UnitPrice = @UnitPrice,
                    DiscountPercentage = @DiscountPercentage,
                    DiscountAmount = @DiscountAmount,
                    TaxPercentage = @TaxPercentage,
                    LineNumber = @LineNumber,
                    ShipDate = @ShipDate,
                    Notes = @Notes,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                WHERE SalesOrderLineItemId = @SalesOrderLineItemId AND Active = 1;";

            int rowsAffected = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating SalesOrderLineItem with ID {entity.SalesOrderLineItemId}", SalesOrderLineItemId = entity.SalesOrderLineItemId, EntityType = nameof(SalesOrderLineItem) },
                cancellationToken);

            if (rowsAffected == 0)
            {
                _logger.LogWarning("SalesOrderLineItem with ID {SalesOrderLineItemId} not found for update or already inactive", entity.SalesOrderLineItemId);
            }

            return entity;
        }

        /// <summary>
        /// Gets sales order line items for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The unique identifier of the sales order</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order line items associated with the specified sales order</returns>
        public async Task<IEnumerable<SalesOrderLineItem>> GetBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND soli.SalesOrderId = @SalesOrderId ORDER BY soli.LineNumber";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrderLineItem>>(
                async connection => await connection.QueryAsync<SalesOrderLineItem>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderId = salesOrderId },
                        cancellationToken: cancellationToken)),
                "GetBySalesOrderIdAsync",
                new { SalesOrderId = salesOrderId },
                cancellationToken);
        }

        /// <summary>
        /// Gets sales order line items for a specific product
        /// </summary>
        /// <param name="productId">The unique identifier of the product</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order line items associated with the specified product</returns>
        public async Task<IEnumerable<SalesOrderLineItem>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND soli.ProductId = @ProductId";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrderLineItem>>(
                async connection => await connection.QueryAsync<SalesOrderLineItem>(
                    new CommandDefinition(
                        sql,
                        new { ProductId = productId },
                        cancellationToken: cancellationToken)),
                "GetByProductIdAsync",
                new { ProductId = productId },
                cancellationToken);
        }

        /// <summary>
        /// Gets sales order line items for a specific service
        /// </summary>
        /// <param name="serviceId">The unique identifier of the service</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order line items associated with the specified service</returns>
        public async Task<IEnumerable<SalesOrderLineItem>> GetByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND soli.ServiceId = @ServiceId";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrderLineItem>>(
                async connection => await connection.QueryAsync<SalesOrderLineItem>(
                    new CommandDefinition(
                        sql,
                        new { ServiceId = serviceId },
                        cancellationToken: cancellationToken)),
                "GetByServiceIdAsync",
                new { ServiceId = serviceId },
                cancellationToken);
        }

        /// <summary>
        /// Gets the total amount for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The unique identifier of the sales order</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The total amount for the specified sales order</returns>
        public async Task<decimal> GetTotalForSalesOrderAsync(Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT ISNULL(SUM(
                    (Quantity * UnitPrice) -
                    COALESCE(
                        CASE
                            WHEN DiscountAmount IS NOT NULL AND DiscountAmount > 0
                                THEN DiscountAmount
                            WHEN DiscountPercentage IS NOT NULL AND DiscountPercentage > 0
                                THEN (Quantity * UnitPrice) * (DiscountPercentage / 100.0)
                            ELSE 0
                        END
                    , 0) +
                    COALESCE(
                        CASE
                            WHEN TaxPercentage IS NOT NULL AND TaxPercentage > 0
                                THEN ((Quantity * UnitPrice) -
                                      COALESCE(
                                          CASE
                                              WHEN DiscountAmount IS NOT NULL AND DiscountAmount > 0
                                                  THEN DiscountAmount
                                              WHEN DiscountPercentage IS NOT NULL AND DiscountPercentage > 0
                                                  THEN (Quantity * UnitPrice) * (DiscountPercentage / 100.0)
                                              ELSE 0
                                          END
                                      , 0)) * (TaxPercentage / 100.0)
                            ELSE 0
                        END
                    , 0)
                ), 0) AS TotalAmount
                FROM SalesOrderLineItem
                WHERE SalesOrderId = @SalesOrderId AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<decimal>(
                async connection => await connection.ExecuteScalarAsync<decimal>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderId = salesOrderId },
                        cancellationToken: cancellationToken)),
                "GetTotalForSalesOrderAsync",
                new { SalesOrderId = salesOrderId },
                cancellationToken);
        }

        /// <summary>
        /// Gets sales order line items that were created within a specific date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order line items created within the specified date range</returns>
        public async Task<IEnumerable<SalesOrderLineItem>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND soli.CreatedDate >= @StartDate AND soli.CreatedDate <= @EndDate";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrderLineItem>>(
                async connection => await connection.QueryAsync<SalesOrderLineItem>(
                    new CommandDefinition(
                        sql,
                        new { StartDate = startDate, EndDate = endDate },
                        cancellationToken: cancellationToken)),
                "GetByDateRangeAsync",
                new { StartDate = startDate, EndDate = endDate },
                cancellationToken);
        }
    }
}