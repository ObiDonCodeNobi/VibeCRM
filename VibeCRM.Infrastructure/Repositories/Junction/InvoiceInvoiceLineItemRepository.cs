using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Invoice_InvoiceLineItem junction entities
    /// </summary>
    public class InvoiceInvoiceLineItemRepository : BaseJunctionRepository<Invoice_InvoiceLineItem, Guid, Guid>, IInvoiceInvoiceLineItemRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Invoice_InvoiceLineItem";

        /// <summary>
        /// Gets the name of the first ID column (InvoiceId)
        /// </summary>
        protected override string FirstIdColumnName => "InvoiceId";

        /// <summary>
        /// Gets the name of the second ID column (InvoiceLineItemId)
        /// </summary>
        protected override string SecondIdColumnName => "InvoiceLineItemId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "InvoiceId", "InvoiceLineItemId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceInvoiceLineItemRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public InvoiceInvoiceLineItemRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<InvoiceInvoiceLineItemRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all invoice-invoiceLineItem relationships for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice-invoiceLineItem relationships for the specified invoice</returns>
        public async Task<IEnumerable<Invoice_InvoiceLineItem>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @InvoiceId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Invoice_InvoiceLineItem>(
                    new CommandDefinition(
                        sql,
                        new { InvoiceId = invoiceId },
                        cancellationToken: cancellationToken)),
                "GetByInvoiceIdAsync",
                new { InvoiceId = invoiceId, EntityType = nameof(Invoice_InvoiceLineItem) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all invoice-invoiceLineItem relationships for a specific invoice line item
        /// </summary>
        /// <param name="invoiceLineItemId">The invoice line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice-invoiceLineItem relationships for the specified invoice line item</returns>
        public async Task<IEnumerable<Invoice_InvoiceLineItem>> GetByInvoiceLineItemIdAsync(Guid invoiceLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @InvoiceLineItemId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Invoice_InvoiceLineItem>(
                    new CommandDefinition(
                        sql,
                        new { InvoiceLineItemId = invoiceLineItemId },
                        cancellationToken: cancellationToken)),
                "GetByInvoiceLineItemIdAsync",
                new { InvoiceLineItemId = invoiceLineItemId, EntityType = nameof(Invoice_InvoiceLineItem) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific invoice-invoiceLineItem relationship
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="invoiceLineItemId">The invoice line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The invoice-invoiceLineItem relationship if found, otherwise null</returns>
        public async Task<Invoice_InvoiceLineItem?> GetByInvoiceAndInvoiceLineItemIdAsync(Guid invoiceId, Guid invoiceLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @InvoiceId
                AND {SecondIdColumnName} = @InvoiceLineItemId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Invoice_InvoiceLineItem>(
                    new CommandDefinition(
                        sql,
                        new { InvoiceId = invoiceId, InvoiceLineItemId = invoiceLineItemId },
                        cancellationToken: cancellationToken)),
                "GetByInvoiceAndInvoiceLineItemIdAsync",
                new { InvoiceId = invoiceId, InvoiceLineItemId = invoiceLineItemId, EntityType = nameof(Invoice_InvoiceLineItem) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between an invoice and an invoice line item
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="invoiceLineItemId">The invoice line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByInvoiceAndInvoiceLineItemAsync(Guid invoiceId, Guid invoiceLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @InvoiceId
                AND {SecondIdColumnName} = @InvoiceLineItemId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { InvoiceId = invoiceId, InvoiceLineItemId = invoiceLineItemId },
                        cancellationToken: cancellationToken)),
                "ExistsByInvoiceAndInvoiceLineItemAsync",
                new { InvoiceId = invoiceId, InvoiceLineItemId = invoiceLineItemId, EntityType = nameof(Invoice_InvoiceLineItem) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Adds a new relationship between an invoice and an invoice line item
        /// </summary>
        /// <param name="entity">The entity containing the invoice and invoice line item identifiers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created invoice-invoiceLineItem relationship</returns>
        public override async Task<Invoice_InvoiceLineItem> AddAsync(Invoice_InvoiceLineItem entity, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                IF NOT EXISTS (
                    SELECT 1 FROM {TableName}
                    WHERE {FirstIdColumnName} = @InvoiceId
                    AND {SecondIdColumnName} = @InvoiceLineItemId
                    AND Active = 1
                )
                BEGIN
                    INSERT INTO {TableName} ({FirstIdColumnName}, {SecondIdColumnName}, Active, ModifiedDate)
                    VALUES (@InvoiceId, @InvoiceLineItemId, 1, @ModifiedDate)
                END
                ELSE
                BEGIN
                    UPDATE {TableName}
                    SET Active = 1, ModifiedDate = @ModifiedDate
                    WHERE {FirstIdColumnName} = @InvoiceId
                    AND {SecondIdColumnName} = @InvoiceLineItemId
                END";

            var parameters = new
            {
                InvoiceId = entity.InvoiceId,
                InvoiceLineItemId = entity.InvoiceLineItemId,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { entity.InvoiceId, entity.InvoiceLineItemId, EntityType = nameof(Invoice_InvoiceLineItem) },
                cancellationToken);

            entity.ModifiedDate = parameters.ModifiedDate;
            entity.Active = true;

            return entity;
        }

        /// <summary>
        /// Deletes a specific invoice-invoiceLineItem relationship
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="invoiceLineItemId">The invoice line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByInvoiceAndInvoiceLineItemIdAsync(Guid invoiceId, Guid invoiceLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @InvoiceId
                AND {SecondIdColumnName} = @InvoiceLineItemId
                AND Active = 1";

            var parameters = new
            {
                InvoiceId = invoiceId,
                InvoiceLineItemId = invoiceLineItemId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByInvoiceAndInvoiceLineItemIdAsync",
                new { InvoiceId = invoiceId, InvoiceLineItemId = invoiceLineItemId, EntityType = nameof(Invoice_InvoiceLineItem) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all invoice-invoiceLineItem relationships for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @InvoiceId
                AND Active = 1";

            var parameters = new
            {
                InvoiceId = invoiceId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByInvoiceIdAsync",
                new { InvoiceId = invoiceId, EntityType = nameof(Invoice_InvoiceLineItem) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all invoice-invoiceLineItem relationships for a specific invoice line item
        /// </summary>
        /// <param name="invoiceLineItemId">The invoice line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByInvoiceLineItemIdAsync(Guid invoiceLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @InvoiceLineItemId
                AND Active = 1";

            var parameters = new
            {
                InvoiceLineItemId = invoiceLineItemId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByInvoiceLineItemIdAsync",
                new { InvoiceLineItemId = invoiceLineItemId, EntityType = nameof(Invoice_InvoiceLineItem) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}