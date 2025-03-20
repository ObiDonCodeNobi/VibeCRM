using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Quote_QuoteLineItem junction entities
    /// </summary>
    public class QuoteQuoteLineItemRepository : BaseJunctionRepository<Quote_QuoteLineItem, Guid, Guid>, IQuoteQuoteLineItemRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Quote_QuoteLineItem";

        /// <summary>
        /// Gets the name of the first ID column (QuoteId)
        /// </summary>
        protected override string FirstIdColumnName => "QuoteId";

        /// <summary>
        /// Gets the name of the second ID column (QuoteLineItemId)
        /// </summary>
        protected override string SecondIdColumnName => "QuoteLineItemId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "QuoteId", "QuoteLineItemId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteQuoteLineItemRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public QuoteQuoteLineItemRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<QuoteQuoteLineItemRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all quote-quoteLineItem relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote-quoteLineItem relationships for the specified quote</returns>
        public async Task<IEnumerable<Quote_QuoteLineItem>> GetByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @QuoteId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Quote_QuoteLineItem>(
                    new CommandDefinition(
                        sql,
                        new { QuoteId = quoteId },
                        cancellationToken: cancellationToken)),
                "GetByQuoteIdAsync",
                new { QuoteId = quoteId, EntityType = nameof(Quote_QuoteLineItem) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all quote-quoteLineItem relationships for a specific quote line item
        /// </summary>
        /// <param name="quoteLineItemId">The quote line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote-quoteLineItem relationships for the specified quote line item</returns>
        public async Task<IEnumerable<Quote_QuoteLineItem>> GetByQuoteLineItemIdAsync(Guid quoteLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @QuoteLineItemId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Quote_QuoteLineItem>(
                    new CommandDefinition(
                        sql,
                        new { QuoteLineItemId = quoteLineItemId },
                        cancellationToken: cancellationToken)),
                "GetByQuoteLineItemIdAsync",
                new { QuoteLineItemId = quoteLineItemId, EntityType = nameof(Quote_QuoteLineItem) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific quote-quoteLineItem relationship
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="quoteLineItemId">The quote line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The quote-quoteLineItem relationship if found, otherwise null</returns>
        public async Task<Quote_QuoteLineItem?> GetByQuoteAndQuoteLineItemIdAsync(Guid quoteId, Guid quoteLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @QuoteId
                AND {SecondIdColumnName} = @QuoteLineItemId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Quote_QuoteLineItem>(
                    new CommandDefinition(
                        sql,
                        new { QuoteId = quoteId, QuoteLineItemId = quoteLineItemId },
                        cancellationToken: cancellationToken)),
                "GetByQuoteAndQuoteLineItemIdAsync",
                new { QuoteId = quoteId, QuoteLineItemId = quoteLineItemId, EntityType = nameof(Quote_QuoteLineItem) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a quote and a quote line item
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="quoteLineItemId">The quote line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByQuoteAndQuoteLineItemAsync(Guid quoteId, Guid quoteLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @QuoteId
                AND {SecondIdColumnName} = @QuoteLineItemId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { QuoteId = quoteId, QuoteLineItemId = quoteLineItemId },
                        cancellationToken: cancellationToken)),
                "ExistsByQuoteAndQuoteLineItemAsync",
                new { QuoteId = quoteId, QuoteLineItemId = quoteLineItemId, EntityType = nameof(Quote_QuoteLineItem) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Adds a new relationship between a quote and a quote line item
        /// </summary>
        /// <param name="entity">The entity containing the quote and quote line item identifiers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created quote-quoteLineItem relationship</returns>
        public override async Task<Quote_QuoteLineItem> AddAsync(Quote_QuoteLineItem entity, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                IF NOT EXISTS (
                    SELECT 1 FROM {TableName}
                    WHERE {FirstIdColumnName} = @QuoteId
                    AND {SecondIdColumnName} = @QuoteLineItemId
                    AND Active = 1
                )
                BEGIN
                    INSERT INTO {TableName} ({FirstIdColumnName}, {SecondIdColumnName}, Active, ModifiedDate)
                    VALUES (@QuoteId, @QuoteLineItemId, 1, @ModifiedDate)
                END
                ELSE
                BEGIN
                    UPDATE {TableName}
                    SET Active = 1, ModifiedDate = @ModifiedDate
                    WHERE {FirstIdColumnName} = @QuoteId
                    AND {SecondIdColumnName} = @QuoteLineItemId
                END";

            var parameters = new
            {
                QuoteId = entity.QuoteId,
                QuoteLineItemId = entity.QuoteLineItemId,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { entity.QuoteId, entity.QuoteLineItemId, EntityType = nameof(Quote_QuoteLineItem) },
                cancellationToken);

            entity.ModifiedDate = parameters.ModifiedDate;
            entity.Active = true;

            return entity;
        }

        /// <summary>
        /// Deletes a specific quote-quoteLineItem relationship
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="quoteLineItemId">The quote line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByQuoteAndQuoteLineItemIdAsync(Guid quoteId, Guid quoteLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @QuoteId
                AND {SecondIdColumnName} = @QuoteLineItemId
                AND Active = 1";

            var parameters = new
            {
                QuoteId = quoteId,
                QuoteLineItemId = quoteLineItemId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByQuoteAndQuoteLineItemIdAsync",
                new { QuoteId = quoteId, QuoteLineItemId = quoteLineItemId, EntityType = nameof(Quote_QuoteLineItem) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all quote-quoteLineItem relationships for a specific quote
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
                new { QuoteId = quoteId, EntityType = nameof(Quote_QuoteLineItem) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all quote-quoteLineItem relationships for a specific quote line item
        /// </summary>
        /// <param name="quoteLineItemId">The quote line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByQuoteLineItemIdAsync(Guid quoteLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @QuoteLineItemId
                AND Active = 1";

            var parameters = new
            {
                QuoteLineItemId = quoteLineItemId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByQuoteLineItemIdAsync",
                new { QuoteLineItemId = quoteLineItemId, EntityType = nameof(Quote_QuoteLineItem) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}