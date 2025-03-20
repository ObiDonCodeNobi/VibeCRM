using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing SalesOrder_Attachment junction entities
    /// </summary>
    public class SalesOrderAttachmentRepository : BaseJunctionRepository<SalesOrder_Attachment, Guid, Guid>, ISalesOrderAttachmentRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "SalesOrder_Attachment";

        /// <summary>
        /// Gets the name of the first ID column (SalesOrderId)
        /// </summary>
        protected override string FirstIdColumnName => "SalesOrderId";

        /// <summary>
        /// Gets the name of the second ID column (AttachmentId)
        /// </summary>
        protected override string SecondIdColumnName => "AttachmentId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "SalesOrderId", "AttachmentId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderAttachmentRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public SalesOrderAttachmentRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<SalesOrderAttachmentRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all salesOrder-attachment relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrder-attachment relationships for the specified sales order</returns>
        public async Task<IEnumerable<SalesOrder_Attachment>> GetBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @SalesOrderId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<SalesOrder_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderId = salesOrderId },
                        cancellationToken: cancellationToken)),
                "GetBySalesOrderIdAsync",
                new { SalesOrderId = salesOrderId, EntityType = nameof(SalesOrder_Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all salesOrder-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrder-attachment relationships for the specified attachment</returns>
        public async Task<IEnumerable<SalesOrder_Attachment>> GetByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<SalesOrder_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { AttachmentId = attachmentId },
                        cancellationToken: cancellationToken)),
                "GetByAttachmentIdAsync",
                new { AttachmentId = attachmentId, EntityType = nameof(SalesOrder_Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific salesOrder-attachment relationship
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The salesOrder-attachment relationship if found, otherwise null</returns>
        public async Task<SalesOrder_Attachment?> GetBySalesOrderAndAttachmentIdAsync(Guid salesOrderId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @SalesOrderId
                AND {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<SalesOrder_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderId = salesOrderId, AttachmentId = attachmentId },
                        cancellationToken: cancellationToken)),
                "GetBySalesOrderAndAttachmentIdAsync",
                new { SalesOrderId = salesOrderId, AttachmentId = attachmentId, EntityType = nameof(SalesOrder_Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a sales order and an attachment
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsBySalesOrderAndAttachmentAsync(Guid salesOrderId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @SalesOrderId
                AND {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderId = salesOrderId, AttachmentId = attachmentId },
                        cancellationToken: cancellationToken)),
                "ExistsBySalesOrderAndAttachmentAsync",
                new { SalesOrderId = salesOrderId, AttachmentId = attachmentId, EntityType = nameof(SalesOrder_Attachment) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Deletes a specific salesOrder-attachment relationship
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteBySalesOrderAndAttachmentIdAsync(Guid salesOrderId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @SalesOrderId
                AND {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            var parameters = new
            {
                SalesOrderId = salesOrderId,
                AttachmentId = attachmentId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteBySalesOrderAndAttachmentIdAsync",
                new { SalesOrderId = salesOrderId, AttachmentId = attachmentId, EntityType = nameof(SalesOrder_Attachment) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all salesOrder-attachment relationships for a specific sales order
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
                new { SalesOrderId = salesOrderId, EntityType = nameof(SalesOrder_Attachment) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all salesOrder-attachment relationships for a specific attachment
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
                new { AttachmentId = attachmentId, EntityType = nameof(SalesOrder_Attachment) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}