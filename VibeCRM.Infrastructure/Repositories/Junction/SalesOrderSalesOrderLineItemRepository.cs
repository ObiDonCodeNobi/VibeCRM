using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing SalesOrder_SalesOrderLineItem junction entities
    /// </summary>
    public class SalesOrderSalesOrderLineItemRepository : BaseJunctionRepository<SalesOrder_SalesOrderLineItem, Guid, Guid>, ISalesOrderSalesOrderLineItemRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "SalesOrder_SalesOrderLineItem";

        /// <summary>
        /// Gets the name of the first ID column (SalesOrderId)
        /// </summary>
        protected override string FirstIdColumnName => "SalesOrderId";

        /// <summary>
        /// Gets the name of the second ID column (SalesOrderLineItemId)
        /// </summary>
        protected override string SecondIdColumnName => "SalesOrderLineItemId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "SalesOrderId", "SalesOrderLineItemId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderSalesOrderLineItemRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public SalesOrderSalesOrderLineItemRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<SalesOrderSalesOrderLineItemRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all salesOrder-salesOrderLineItem relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrder-salesOrderLineItem relationships for the specified sales order</returns>
        public async Task<IEnumerable<SalesOrder_SalesOrderLineItem>> GetBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @SalesOrderId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<SalesOrder_SalesOrderLineItem>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderId = salesOrderId },
                        cancellationToken: cancellationToken)),
                "GetBySalesOrderIdAsync",
                new { SalesOrderId = salesOrderId, EntityType = nameof(SalesOrder_SalesOrderLineItem) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all salesOrder-salesOrderLineItem relationships for a specific sales order line item
        /// </summary>
        /// <param name="salesOrderLineItemId">The sales order line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrder-salesOrderLineItem relationships for the specified sales order line item</returns>
        public async Task<IEnumerable<SalesOrder_SalesOrderLineItem>> GetBySalesOrderLineItemIdAsync(Guid salesOrderLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @SalesOrderLineItemId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<SalesOrder_SalesOrderLineItem>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderLineItemId = salesOrderLineItemId },
                        cancellationToken: cancellationToken)),
                "GetBySalesOrderLineItemIdAsync",
                new { SalesOrderLineItemId = salesOrderLineItemId, EntityType = nameof(SalesOrder_SalesOrderLineItem) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific salesOrder-salesOrderLineItem relationship
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="salesOrderLineItemId">The sales order line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The salesOrder-salesOrderLineItem relationship if found, otherwise null</returns>
        public async Task<SalesOrder_SalesOrderLineItem?> GetBySalesOrderAndSalesOrderLineItemIdAsync(Guid salesOrderId, Guid salesOrderLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @SalesOrderId
                AND {SecondIdColumnName} = @SalesOrderLineItemId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<SalesOrder_SalesOrderLineItem>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderId = salesOrderId, SalesOrderLineItemId = salesOrderLineItemId },
                        cancellationToken: cancellationToken)),
                "GetBySalesOrderAndSalesOrderLineItemIdAsync",
                new { SalesOrderId = salesOrderId, SalesOrderLineItemId = salesOrderLineItemId, EntityType = nameof(SalesOrder_SalesOrderLineItem) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a sales order and a sales order line item
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="salesOrderLineItemId">The sales order line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsBySalesOrderAndSalesOrderLineItemAsync(Guid salesOrderId, Guid salesOrderLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @SalesOrderId
                AND {SecondIdColumnName} = @SalesOrderLineItemId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderId = salesOrderId, SalesOrderLineItemId = salesOrderLineItemId },
                        cancellationToken: cancellationToken)),
                "ExistsBySalesOrderAndSalesOrderLineItemAsync",
                new { SalesOrderId = salesOrderId, SalesOrderLineItemId = salesOrderLineItemId, EntityType = nameof(SalesOrder_SalesOrderLineItem) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Adds a new relationship between a sales order and a sales order line item
        /// </summary>
        /// <param name="entity">The entity containing the sales order and sales order line item identifiers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created salesOrder-salesOrderLineItem relationship</returns>
        public override async Task<SalesOrder_SalesOrderLineItem> AddAsync(SalesOrder_SalesOrderLineItem entity, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                IF NOT EXISTS (
                    SELECT 1 FROM {TableName}
                    WHERE {FirstIdColumnName} = @SalesOrderId
                    AND {SecondIdColumnName} = @SalesOrderLineItemId
                    AND Active = 1
                )
                BEGIN
                    INSERT INTO {TableName} ({FirstIdColumnName}, {SecondIdColumnName}, Active, ModifiedDate)
                    VALUES (@SalesOrderId, @SalesOrderLineItemId, 1, @ModifiedDate)
                END
                ELSE
                BEGIN
                    UPDATE {TableName}
                    SET Active = 1, ModifiedDate = @ModifiedDate
                    WHERE {FirstIdColumnName} = @SalesOrderId
                    AND {SecondIdColumnName} = @SalesOrderLineItemId
                END";

            var parameters = new
            {
                SalesOrderId = entity.SalesOrderId,
                SalesOrderLineItemId = entity.SalesOrderLineItemId,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { entity.SalesOrderId, entity.SalesOrderLineItemId, EntityType = nameof(SalesOrder_SalesOrderLineItem) },
                cancellationToken);

            entity.ModifiedDate = parameters.ModifiedDate;
            entity.Active = true;

            return entity;
        }

        /// <summary>
        /// Deletes a specific salesOrder-salesOrderLineItem relationship
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="salesOrderLineItemId">The sales order line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteBySalesOrderAndSalesOrderLineItemIdAsync(Guid salesOrderId, Guid salesOrderLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @SalesOrderId
                AND {SecondIdColumnName} = @SalesOrderLineItemId
                AND Active = 1";

            var parameters = new
            {
                SalesOrderId = salesOrderId,
                SalesOrderLineItemId = salesOrderLineItemId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteBySalesOrderAndSalesOrderLineItemIdAsync",
                new { SalesOrderId = salesOrderId, SalesOrderLineItemId = salesOrderLineItemId, EntityType = nameof(SalesOrder_SalesOrderLineItem) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all salesOrder-salesOrderLineItem relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @SalesOrderId
                AND Active = 1";

            var parameters = new
            {
                SalesOrderId = salesOrderId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteBySalesOrderIdAsync",
                new { SalesOrderId = salesOrderId, EntityType = nameof(SalesOrder_SalesOrderLineItem) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all salesOrder-salesOrderLineItem relationships for a specific sales order line item
        /// </summary>
        /// <param name="salesOrderLineItemId">The sales order line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteBySalesOrderLineItemIdAsync(Guid salesOrderLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @SalesOrderLineItemId
                AND Active = 1";

            var parameters = new
            {
                SalesOrderLineItemId = salesOrderLineItemId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteBySalesOrderLineItemIdAsync",
                new { SalesOrderLineItemId = salesOrderLineItemId, EntityType = nameof(SalesOrder_SalesOrderLineItem) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}