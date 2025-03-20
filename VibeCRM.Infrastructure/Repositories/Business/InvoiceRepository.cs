using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing Invoice entities
    /// </summary>
    public class InvoiceRepository : BaseRepository<Invoice, Guid>, IInvoiceRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Invoice";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "InvoiceId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "InvoiceId", "SalesOrderId", "Number",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Base select query used by multiple methods
        /// </summary>
        private const string BaseSelectQuery = @"
            SELECT
                i.InvoiceId, i.SalesOrderId, i.Number,
                i.CreatedBy, i.CreatedDate, i.ModifiedBy, i.ModifiedDate, i.Active
            FROM Invoice i
            WHERE i.Active = 1";

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The database connection factory</param>
        /// <param name="logger">The logger instance</param>
        public InvoiceRepository(ISQLConnectionFactory connectionFactory, ILogger<InvoiceRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets an invoice by its identifier
        /// </summary>
        /// <param name="id">The unique identifier of the invoice to retrieve</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The invoice if found; otherwise, null</returns>
        public override async Task<Invoice?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = $"{BaseSelectQuery} AND i.{IdColumnName} = @Id";

            var result = await ExecuteWithResilienceAndLoggingAsync(
                async (connection) => await connection.QueryAsync<Invoice>(
                    new CommandDefinition(
                        sql,
                        new { Id = id },
                        cancellationToken: cancellationToken)),
                "GetByIdAsync",
                new { Id = id, TableName },
                cancellationToken);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Gets all invoices
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of all active invoices</returns>
        public override async Task<IEnumerable<Invoice>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var sql = BaseSelectQuery;

            return await ExecuteWithResilienceAndLoggingAsync(
                async (connection) => await connection.QueryAsync<Invoice>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetAllAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets invoices by sales order identifier
        /// </summary>
        /// <param name="salesOrderId">The unique identifier of the sales order</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoices associated with the specified sales order</returns>
        public async Task<IEnumerable<Invoice>> GetBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            var sql = $"{BaseSelectQuery} AND i.SalesOrderId = @SalesOrderId";

            return await ExecuteWithResilienceAndLoggingAsync(
                async (connection) => await connection.QueryAsync<Invoice>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderId = salesOrderId },
                        cancellationToken: cancellationToken)),
                "GetBySalesOrderIdAsync",
                new { SalesOrderId = salesOrderId, TableName },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new invoice
        /// </summary>
        /// <param name="entity">The invoice to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added invoice</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<Invoice> AddAsync(Invoice entity, CancellationToken cancellationToken = default)
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
                INSERT INTO Invoice (
                    InvoiceId,
                    SalesOrderId,
                    Number,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @InvoiceId,
                    @SalesOrderId,
                    @Number,
                    @CreatedDate,
                    @CreatedBy,
                    @ModifiedDate,
                    @ModifiedBy,
                    @Active
                )";

            var parameters = new
            {
                InvoiceId = entity.Id,
                entity.SalesOrderId,
                entity.Number,
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
                new { ErrorMessage = $"Error adding {typeof(Invoice).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(Invoice) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing invoice
        /// </summary>
        /// <param name="entity">The invoice to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated invoice</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<Invoice> UpdateAsync(Invoice entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE Invoice
                SET SalesOrderId = @SalesOrderId,
                    Number = @Number,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE InvoiceId = @InvoiceId
                AND Active = 1";

            var parameters = new
            {
                InvoiceId = entity.Id,
                entity.SalesOrderId,
                entity.Number,
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
                new { ErrorMessage = $"Error updating {typeof(Invoice).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(Invoice) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Checks if an invoice with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier of the invoice to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if an invoice with the specified identifier exists; otherwise, false</returns>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM Invoice
                    WHERE InvoiceId = @Id AND Active = 1
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