using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing PaymentLineItem entities
    /// </summary>
    public class PaymentLineItemRepository : BaseRepository<PaymentLineItem, Guid>, IPaymentLineItemRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "PaymentLineItem";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "PaymentLineItemId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "PaymentLineItemId", "PaymentId", "InvoiceId", "InvoiceLineItemId", "Amount", "Description", "Notes",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Base SELECT query for PaymentLineItem entities
        /// </summary>
        private const string BaseSelectQuery = @"
            SELECT pli.PaymentLineItemId AS Id, pli.PaymentId, pli.InvoiceId, pli.InvoiceLineItemId,
                   pli.Amount, pli.Description, pli.Notes,
                   pli.CreatedBy, pli.CreatedDate, pli.ModifiedBy, pli.ModifiedDate, pli.Active
            FROM PaymentLineItem pli
            WHERE pli.Active = 1";

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentLineItemRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public PaymentLineItemRepository(ISQLConnectionFactory connectionFactory, ILogger<PaymentLineItemRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new payment line item to the repository
        /// </summary>
        /// <param name="entity">The payment line item to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added payment line item with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when PaymentLineItemId is empty</exception>
        public override async Task<PaymentLineItem> AddAsync(PaymentLineItem entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.PaymentLineItemId == Guid.Empty) throw new ArgumentException("The PaymentLineItem ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO PaymentLineItem (
                    PaymentLineItemId, PaymentId, InvoiceId, InvoiceLineItemId,
                    Amount, Description, Notes,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @PaymentLineItemId, @PaymentId, @InvoiceId, @InvoiceLineItemId,
                    @Amount, @Description, @Notes,
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
                new { ErrorMessage = $"Error adding PaymentLineItem with ID {entity.PaymentLineItemId}", PaymentLineItemId = entity.PaymentLineItemId, EntityType = nameof(PaymentLineItem) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing payment line item in the repository
        /// </summary>
        /// <param name="entity">The payment line item to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated payment line item</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when PaymentLineItemId is empty</exception>
        public override async Task<PaymentLineItem> UpdateAsync(PaymentLineItem entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.PaymentLineItemId == Guid.Empty) throw new ArgumentException("The PaymentLineItem ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE PaymentLineItem SET
                    PaymentId = @PaymentId,
                    InvoiceId = @InvoiceId,
                    InvoiceLineItemId = @InvoiceLineItemId,
                    Amount = @Amount,
                    Description = @Description,
                    Notes = @Notes,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                WHERE PaymentLineItemId = @PaymentLineItemId AND Active = 1;";

            int rowsAffected = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating PaymentLineItem with ID {entity.PaymentLineItemId}", PaymentLineItemId = entity.PaymentLineItemId, EntityType = nameof(PaymentLineItem) },
                cancellationToken);

            if (rowsAffected == 0)
            {
                _logger.LogWarning("PaymentLineItem with ID {PaymentLineItemId} not found for update or already inactive", entity.PaymentLineItemId);
            }

            return entity;
        }

        /// <summary>
        /// Gets payment line items for a specific payment
        /// </summary>
        /// <param name="paymentId">The unique identifier of the payment</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment line items associated with the specified payment</returns>
        public async Task<IEnumerable<PaymentLineItem>> GetByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND pli.PaymentId = @PaymentId";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<PaymentLineItem>>(
                async connection => await connection.QueryAsync<PaymentLineItem>(
                    new CommandDefinition(
                        sql,
                        new { PaymentId = paymentId },
                        cancellationToken: cancellationToken)),
                "GetByPaymentIdAsync",
                new { PaymentId = paymentId },
                cancellationToken);
        }

        /// <summary>
        /// Gets payment line items for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The unique identifier of the invoice</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment line items associated with the specified invoice</returns>
        public async Task<IEnumerable<PaymentLineItem>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND pli.InvoiceId = @InvoiceId";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<PaymentLineItem>>(
                async connection => await connection.QueryAsync<PaymentLineItem>(
                    new CommandDefinition(
                        sql,
                        new { InvoiceId = invoiceId },
                        cancellationToken: cancellationToken)),
                "GetByInvoiceIdAsync",
                new { InvoiceId = invoiceId },
                cancellationToken);
        }

        /// <summary>
        /// Gets payment line items for a specific invoice line item
        /// </summary>
        /// <param name="invoiceLineItemId">The unique identifier of the invoice line item</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment line items associated with the specified invoice line item</returns>
        public async Task<IEnumerable<PaymentLineItem>> GetByInvoiceLineItemIdAsync(Guid invoiceLineItemId, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND pli.InvoiceLineItemId = @InvoiceLineItemId";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<PaymentLineItem>>(
                async connection => await connection.QueryAsync<PaymentLineItem>(
                    new CommandDefinition(
                        sql,
                        new { InvoiceLineItemId = invoiceLineItemId },
                        cancellationToken: cancellationToken)),
                "GetByInvoiceLineItemIdAsync",
                new { InvoiceLineItemId = invoiceLineItemId },
                cancellationToken);
        }

        /// <summary>
        /// Gets the total payment amount for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The unique identifier of the invoice</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The total amount paid for the specified invoice</returns>
        public async Task<decimal> GetTotalPaidForInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT ISNULL(SUM(pli.Amount), 0) AS TotalPaid
                FROM PaymentLineItem pli
                JOIN Payment p ON pli.PaymentId = p.PaymentId
                WHERE pli.InvoiceId = @InvoiceId
                  AND pli.Active = 1
                  AND p.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<decimal>(
                async connection => await connection.ExecuteScalarAsync<decimal>(
                    new CommandDefinition(
                        sql,
                        new { InvoiceId = invoiceId },
                        cancellationToken: cancellationToken)),
                "GetTotalPaidForInvoiceAsync",
                new { InvoiceId = invoiceId },
                cancellationToken);
        }
    }
}