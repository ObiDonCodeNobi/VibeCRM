using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Payment_PaymentLineItem junction entities
    /// </summary>
    public class PaymentPaymentLineItemRepository : BaseJunctionRepository<Payment_PaymentLineItem, Guid, Guid>, IPaymentPaymentLineItemRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Payment_PaymentLineItem";

        /// <summary>
        /// Gets the name of the first ID column (PaymentId)
        /// </summary>
        protected override string FirstIdColumnName => "PaymentId";

        /// <summary>
        /// Gets the name of the second ID column (PaymentLineItemId)
        /// </summary>
        protected override string SecondIdColumnName => "PaymentLineItemId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "PaymentId", "PaymentLineItemId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentPaymentLineItemRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public PaymentPaymentLineItemRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PaymentPaymentLineItemRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all payment-paymentLineItem relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-paymentLineItem relationships for the specified payment</returns>
        public async Task<IEnumerable<Payment_PaymentLineItem>> GetByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PaymentId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Payment_PaymentLineItem>(
                    new CommandDefinition(
                        sql,
                        new { PaymentId = paymentId },
                        cancellationToken: cancellationToken)),
                "GetByPaymentIdAsync",
                new { PaymentId = paymentId, EntityType = nameof(Payment_PaymentLineItem) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all payment-paymentLineItem relationships for a specific payment line item
        /// </summary>
        /// <param name="paymentLineItemId">The payment line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-paymentLineItem relationships for the specified payment line item</returns>
        public async Task<IEnumerable<Payment_PaymentLineItem>> GetByPaymentLineItemIdAsync(Guid paymentLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @PaymentLineItemId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Payment_PaymentLineItem>(
                    new CommandDefinition(
                        sql,
                        new { PaymentLineItemId = paymentLineItemId },
                        cancellationToken: cancellationToken)),
                "GetByPaymentLineItemIdAsync",
                new { PaymentLineItemId = paymentLineItemId, EntityType = nameof(Payment_PaymentLineItem) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific payment-paymentLineItem relationship
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="paymentLineItemId">The payment line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The payment-paymentLineItem relationship if found, otherwise null</returns>
        public async Task<Payment_PaymentLineItem?> GetByPaymentAndPaymentLineItemIdAsync(Guid paymentId, Guid paymentLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PaymentId
                AND {SecondIdColumnName} = @PaymentLineItemId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Payment_PaymentLineItem>(
                    new CommandDefinition(
                        sql,
                        new { PaymentId = paymentId, PaymentLineItemId = paymentLineItemId },
                        cancellationToken: cancellationToken)),
                "GetByPaymentAndPaymentLineItemIdAsync",
                new { PaymentId = paymentId, PaymentLineItemId = paymentLineItemId, EntityType = nameof(Payment_PaymentLineItem) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a payment and a payment line item
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="paymentLineItemId">The payment line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByPaymentAndPaymentLineItemAsync(Guid paymentId, Guid paymentLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PaymentId
                AND {SecondIdColumnName} = @PaymentLineItemId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { PaymentId = paymentId, PaymentLineItemId = paymentLineItemId },
                        cancellationToken: cancellationToken)),
                "ExistsByPaymentAndPaymentLineItemAsync",
                new { PaymentId = paymentId, PaymentLineItemId = paymentLineItemId, EntityType = nameof(Payment_PaymentLineItem) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Adds a new relationship between a payment and a payment line item
        /// </summary>
        /// <param name="entity">The entity containing the payment and payment line item identifiers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created payment-paymentLineItem relationship</returns>
        public override async Task<Payment_PaymentLineItem> AddAsync(Payment_PaymentLineItem entity, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                IF NOT EXISTS (
                    SELECT 1 FROM {TableName}
                    WHERE {FirstIdColumnName} = @PaymentId
                    AND {SecondIdColumnName} = @PaymentLineItemId
                    AND Active = 1
                )
                BEGIN
                    INSERT INTO {TableName} ({FirstIdColumnName}, {SecondIdColumnName}, Active, ModifiedDate)
                    VALUES (@PaymentId, @PaymentLineItemId, 1, @ModifiedDate)
                END
                ELSE
                BEGIN
                    UPDATE {TableName}
                    SET Active = 1, ModifiedDate = @ModifiedDate
                    WHERE {FirstIdColumnName} = @PaymentId
                    AND {SecondIdColumnName} = @PaymentLineItemId
                END";

            var parameters = new
            {
                PaymentId = entity.PaymentId,
                PaymentLineItemId = entity.PaymentLineItemId,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { entity.PaymentId, entity.PaymentLineItemId, EntityType = nameof(Payment_PaymentLineItem) },
                cancellationToken);

            entity.ModifiedDate = parameters.ModifiedDate;
            entity.Active = true;

            return entity;
        }

        /// <summary>
        /// Deletes a specific payment-paymentLineItem relationship
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="paymentLineItemId">The payment line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByPaymentAndPaymentLineItemIdAsync(Guid paymentId, Guid paymentLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PaymentId
                AND {SecondIdColumnName} = @PaymentLineItemId
                AND Active = 1";

            var parameters = new
            {
                PaymentId = paymentId,
                PaymentLineItemId = paymentLineItemId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPaymentAndPaymentLineItemIdAsync",
                new { PaymentId = paymentId, PaymentLineItemId = paymentLineItemId, EntityType = nameof(Payment_PaymentLineItem) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all payment-paymentLineItem relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PaymentId
                AND Active = 1";

            var parameters = new
            {
                PaymentId = paymentId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPaymentIdAsync",
                new { PaymentId = paymentId, EntityType = nameof(Payment_PaymentLineItem) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all payment-paymentLineItem relationships for a specific payment line item
        /// </summary>
        /// <param name="paymentLineItemId">The payment line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByPaymentLineItemIdAsync(Guid paymentLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @PaymentLineItemId
                AND Active = 1";

            var parameters = new
            {
                PaymentLineItemId = paymentLineItemId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPaymentLineItemIdAsync",
                new { PaymentLineItemId = paymentLineItemId, EntityType = nameof(Payment_PaymentLineItem) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}