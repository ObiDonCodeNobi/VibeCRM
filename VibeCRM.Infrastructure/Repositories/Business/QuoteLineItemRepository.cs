using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing QuoteLineItem entities
    /// </summary>
    public class QuoteLineItemRepository : BaseRepository<QuoteLineItem, Guid>, IQuoteLineItemRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "QuoteLineItem";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "QuoteLineItemId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "QuoteLineItemId", "QuoteId", "ProductId", "ServiceId", "Description",
            "Quantity", "UnitPrice", "DiscountPercentage", "DiscountAmount",
            "TaxPercentage", "LineNumber", "Notes",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Base SELECT query for QuoteLineItem entities
        /// </summary>
        private const string BaseSelectQuery = @"
            SELECT qli.QuoteLineItemId AS Id, qli.QuoteId, qli.ProductId, qli.ServiceId, qli.Description,
                   qli.Quantity, qli.UnitPrice, qli.DiscountPercentage, qli.DiscountAmount,
                   qli.TaxPercentage, qli.LineNumber, qli.Notes,
                   qli.CreatedBy, qli.CreatedDate, qli.ModifiedBy, qli.ModifiedDate, qli.Active
            FROM QuoteLineItem qli
            WHERE qli.Active = 1";

        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteLineItemRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public QuoteLineItemRepository(ISQLConnectionFactory connectionFactory, ILogger<QuoteLineItemRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new quote line item to the repository
        /// </summary>
        /// <param name="entity">The quote line item to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added quote line item with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when QuoteLineItemId is empty</exception>
        public override async Task<QuoteLineItem> AddAsync(QuoteLineItem entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.QuoteLineItemId == Guid.Empty) throw new ArgumentException("The QuoteLineItem ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO QuoteLineItem (
                    QuoteLineItemId, QuoteId, ProductId, ServiceId, Description,
                    Quantity, UnitPrice, DiscountPercentage, DiscountAmount,
                    TaxPercentage, LineNumber, Notes,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @QuoteLineItemId, @QuoteId, @ProductId, @ServiceId, @Description,
                    @Quantity, @UnitPrice, @DiscountPercentage, @DiscountAmount,
                    @TaxPercentage, @LineNumber, @Notes,
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
                new { ErrorMessage = $"Error adding QuoteLineItem with ID {entity.QuoteLineItemId}", QuoteLineItemId = entity.QuoteLineItemId, EntityType = nameof(QuoteLineItem) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing quote line item in the repository
        /// </summary>
        /// <param name="entity">The quote line item to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated quote line item</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when QuoteLineItemId is empty</exception>
        public override async Task<QuoteLineItem> UpdateAsync(QuoteLineItem entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.QuoteLineItemId == Guid.Empty) throw new ArgumentException("The QuoteLineItem ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE QuoteLineItem SET
                    QuoteId = @QuoteId,
                    ProductId = @ProductId,
                    ServiceId = @ServiceId,
                    Description = @Description,
                    Quantity = @Quantity,
                    UnitPrice = @UnitPrice,
                    DiscountPercentage = @DiscountPercentage,
                    DiscountAmount = @DiscountAmount,
                    TaxPercentage = @TaxPercentage,
                    LineNumber = @LineNumber,
                    Notes = @Notes,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                WHERE QuoteLineItemId = @QuoteLineItemId AND Active = 1;";

            int rowsAffected = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating QuoteLineItem with ID {entity.QuoteLineItemId}", QuoteLineItemId = entity.QuoteLineItemId, EntityType = nameof(QuoteLineItem) },
                cancellationToken);

            if (rowsAffected == 0)
            {
                _logger.LogWarning("QuoteLineItem with ID {QuoteLineItemId} not found for update or already inactive", entity.QuoteLineItemId);
            }

            return entity;
        }

        /// <summary>
        /// Gets quote line items for a specific quote
        /// </summary>
        /// <param name="quoteId">The unique identifier of the quote</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote line items associated with the specified quote</returns>
        public async Task<IEnumerable<QuoteLineItem>> GetByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND qli.QuoteId = @QuoteId ORDER BY qli.LineNumber";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<QuoteLineItem>>(
                async connection => await connection.QueryAsync<QuoteLineItem>(
                    new CommandDefinition(
                        sql,
                        new { QuoteId = quoteId },
                        cancellationToken: cancellationToken)),
                "GetByQuoteIdAsync",
                new { QuoteId = quoteId },
                cancellationToken);
        }

        /// <summary>
        /// Gets quote line items for a specific product
        /// </summary>
        /// <param name="productId">The unique identifier of the product</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote line items associated with the specified product</returns>
        public async Task<IEnumerable<QuoteLineItem>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND qli.ProductId = @ProductId";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<QuoteLineItem>>(
                async connection => await connection.QueryAsync<QuoteLineItem>(
                    new CommandDefinition(
                        sql,
                        new { ProductId = productId },
                        cancellationToken: cancellationToken)),
                "GetByProductIdAsync",
                new { ProductId = productId },
                cancellationToken);
        }

        /// <summary>
        /// Gets quote line items for a specific service
        /// </summary>
        /// <param name="serviceId">The unique identifier of the service</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote line items associated with the specified service</returns>
        public async Task<IEnumerable<QuoteLineItem>> GetByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND qli.ServiceId = @ServiceId";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<QuoteLineItem>>(
                async connection => await connection.QueryAsync<QuoteLineItem>(
                    new CommandDefinition(
                        sql,
                        new { ServiceId = serviceId },
                        cancellationToken: cancellationToken)),
                "GetByServiceIdAsync",
                new { ServiceId = serviceId },
                cancellationToken);
        }

        /// <summary>
        /// Gets the total amount for a specific quote
        /// </summary>
        /// <param name="quoteId">The unique identifier of the quote</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The total amount for the specified quote</returns>
        public async Task<decimal> GetTotalForQuoteAsync(Guid quoteId, CancellationToken cancellationToken = default)
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
                FROM QuoteLineItem
                WHERE QuoteId = @QuoteId AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<decimal>(
                async connection => await connection.ExecuteScalarAsync<decimal>(
                    new CommandDefinition(
                        sql,
                        new { QuoteId = quoteId },
                        cancellationToken: cancellationToken)),
                "GetTotalForQuoteAsync",
                new { QuoteId = quoteId },
                cancellationToken);
        }

        /// <summary>
        /// Gets quote line items that were created within a specific date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote line items created within the specified date range</returns>
        public async Task<IEnumerable<QuoteLineItem>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND qli.CreatedDate >= @StartDate AND qli.CreatedDate <= @EndDate";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<QuoteLineItem>>(
                async connection => await connection.QueryAsync<QuoteLineItem>(
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