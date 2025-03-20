using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Quote_Attachment junction entities
    /// </summary>
    public class QuoteAttachmentRepository : BaseJunctionRepository<Quote_Attachment, Guid, Guid>, IQuoteAttachmentRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Quote_Attachment";

        /// <summary>
        /// Gets the name of the first ID column (QuoteId)
        /// </summary>
        protected override string FirstIdColumnName => "QuoteId";

        /// <summary>
        /// Gets the name of the second ID column (AttachmentId)
        /// </summary>
        protected override string SecondIdColumnName => "AttachmentId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "QuoteId", "AttachmentId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteAttachmentRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public QuoteAttachmentRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<QuoteAttachmentRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all quote-attachment relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote-attachment relationships for the specified quote</returns>
        public async Task<IEnumerable<Quote_Attachment>> GetByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @QuoteId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Quote_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { QuoteId = quoteId },
                        cancellationToken: cancellationToken)),
                "GetByQuoteIdAsync",
                new { QuoteId = quoteId, EntityType = nameof(Quote_Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all quote-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote-attachment relationships for the specified attachment</returns>
        public async Task<IEnumerable<Quote_Attachment>> GetByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Quote_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { AttachmentId = attachmentId },
                        cancellationToken: cancellationToken)),
                "GetByAttachmentIdAsync",
                new { AttachmentId = attachmentId, EntityType = nameof(Quote_Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific quote-attachment relationship
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The quote-attachment relationship if found, otherwise null</returns>
        public async Task<Quote_Attachment?> GetByQuoteAndAttachmentIdAsync(Guid quoteId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @QuoteId
                AND {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Quote_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { QuoteId = quoteId, AttachmentId = attachmentId },
                        cancellationToken: cancellationToken)),
                "GetByQuoteAndAttachmentIdAsync",
                new { QuoteId = quoteId, AttachmentId = attachmentId, EntityType = nameof(Quote_Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a quote and an attachment
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByQuoteAndAttachmentAsync(Guid quoteId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @QuoteId
                AND {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { QuoteId = quoteId, AttachmentId = attachmentId },
                        cancellationToken: cancellationToken)),
                "ExistsByQuoteAndAttachmentAsync",
                new { QuoteId = quoteId, AttachmentId = attachmentId, EntityType = nameof(Quote_Attachment) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Deletes a specific quote-attachment relationship
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByQuoteAndAttachmentIdAsync(Guid quoteId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @QuoteId
                AND {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            var parameters = new
            {
                QuoteId = quoteId,
                AttachmentId = attachmentId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByQuoteAndAttachmentIdAsync",
                new { QuoteId = quoteId, AttachmentId = attachmentId, EntityType = nameof(Quote_Attachment) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all quote-attachment relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @QuoteId
                AND Active = 1";

            var parameters = new
            {
                QuoteId = quoteId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByQuoteIdAsync",
                new { QuoteId = quoteId, EntityType = nameof(Quote_Attachment) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all quote-attachment relationships for a specific attachment
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
                new { AttachmentId = attachmentId, EntityType = nameof(Quote_Attachment) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}