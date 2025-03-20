using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Payment_Attachment junction entities
    /// </summary>
    public class PaymentAttachmentRepository : BaseJunctionRepository<Payment_Attachment, Guid, Guid>, IPaymentAttachmentRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Payment_Attachment";

        /// <summary>
        /// Gets the name of the first ID column (PaymentId)
        /// </summary>
        protected override string FirstIdColumnName => "PaymentId";

        /// <summary>
        /// Gets the name of the second ID column (AttachmentId)
        /// </summary>
        protected override string SecondIdColumnName => "AttachmentId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "PaymentId", "AttachmentId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentAttachmentRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public PaymentAttachmentRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PaymentAttachmentRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all payment-attachment relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-attachment relationships for the specified payment</returns>
        public async Task<IEnumerable<Payment_Attachment>> GetByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PaymentId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Payment_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { PaymentId = paymentId },
                        cancellationToken: cancellationToken)),
                "GetByPaymentIdAsync",
                new { PaymentId = paymentId, EntityType = nameof(Payment_Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all payment-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-attachment relationships for the specified attachment</returns>
        public async Task<IEnumerable<Payment_Attachment>> GetByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Payment_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { AttachmentId = attachmentId },
                        cancellationToken: cancellationToken)),
                "GetByAttachmentIdAsync",
                new { AttachmentId = attachmentId, EntityType = nameof(Payment_Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific payment-attachment relationship
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The payment-attachment relationship if found, otherwise null</returns>
        public async Task<Payment_Attachment?> GetByPaymentAndAttachmentIdAsync(Guid paymentId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PaymentId
                AND {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Payment_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { PaymentId = paymentId, AttachmentId = attachmentId },
                        cancellationToken: cancellationToken)),
                "GetByPaymentAndAttachmentIdAsync",
                new { PaymentId = paymentId, AttachmentId = attachmentId, EntityType = nameof(Payment_Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a payment and an attachment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByPaymentAndAttachmentAsync(Guid paymentId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PaymentId
                AND {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { PaymentId = paymentId, AttachmentId = attachmentId },
                        cancellationToken: cancellationToken)),
                "ExistsByPaymentAndAttachmentAsync",
                new { PaymentId = paymentId, AttachmentId = attachmentId, EntityType = nameof(Payment_Attachment) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Deletes a specific payment-attachment relationship
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByPaymentAndAttachmentIdAsync(Guid paymentId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PaymentId
                AND {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            var parameters = new
            {
                PaymentId = paymentId,
                AttachmentId = attachmentId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPaymentAndAttachmentIdAsync",
                new { PaymentId = paymentId, AttachmentId = attachmentId, EntityType = nameof(Payment_Attachment) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all payment-attachment relationships for a specific payment
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
                new { PaymentId = paymentId, EntityType = nameof(Payment_Attachment) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all payment-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            var parameters = new
            {
                AttachmentId = attachmentId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByAttachmentIdAsync",
                new { AttachmentId = attachmentId, EntityType = nameof(Payment_Attachment) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}