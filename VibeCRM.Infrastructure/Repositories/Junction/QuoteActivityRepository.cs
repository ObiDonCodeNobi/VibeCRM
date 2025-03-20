using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Quote_Activity junction entities
    /// </summary>
    public class QuoteActivityRepository : BaseJunctionRepository<Quote_Activity, Guid, Guid>, IQuoteActivityRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Quote_Activity";

        /// <summary>
        /// Gets the name of the first ID column (QuoteId)
        /// </summary>
        protected override string FirstIdColumnName => "QuoteId";

        /// <summary>
        /// Gets the name of the second ID column (ActivityId)
        /// </summary>
        protected override string SecondIdColumnName => "ActivityId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "QuoteId", "ActivityId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteActivityRepository"/> class.
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public QuoteActivityRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<QuoteActivityRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all quote-activity relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote-activity relationships for the specified quote</returns>
        public async Task<IEnumerable<Quote_Activity>> GetByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @QuoteId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Quote_Activity>(
                    new CommandDefinition(
                        sql,
                        new { QuoteId = quoteId },
                        cancellationToken: cancellationToken)),
                "GetByQuoteIdAsync",
                new { QuoteId = quoteId, EntityType = nameof(Quote_Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all quote-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote-activity relationships for the specified activity</returns>
        public async Task<IEnumerable<Quote_Activity>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Quote_Activity>(
                    new CommandDefinition(
                        sql,
                        new { ActivityId = activityId },
                        cancellationToken: cancellationToken)),
                "GetByActivityIdAsync",
                new { ActivityId = activityId, EntityType = nameof(Quote_Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship between the specified quote and activity exists
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByQuoteAndActivityAsync(Guid quoteId, Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @QuoteId
                AND {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { QuoteId = quoteId, ActivityId = activityId },
                        cancellationToken: cancellationToken)),
                "ExistsByQuoteAndActivityAsync",
                new { QuoteId = quoteId, ActivityId = activityId, EntityType = nameof(Quote_Activity) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Adds a relationship between a quote and an activity
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created quote-activity relationship</returns>
        public async Task<Quote_Activity> AddRelationshipAsync(Guid quoteId, Guid activityId, CancellationToken cancellationToken = default)
        {
            var entity = new Quote_Activity
            {
                QuoteId = quoteId,
                ActivityId = activityId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            var sql = $@"
                INSERT INTO {TableName} ({string.Join(", ", SelectColumns)})
                VALUES (@QuoteId, @ActivityId, @Active, @ModifiedDate)";

            var parameters = new
            {
                QuoteId = quoteId,
                ActivityId = activityId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddRelationshipAsync",
                new { QuoteId = quoteId, ActivityId = activityId, EntityType = nameof(Quote_Activity) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Removes a relationship between a quote and an activity
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid quoteId, Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @QuoteId
                AND {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            var parameters = new
            {
                QuoteId = quoteId,
                ActivityId = activityId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveRelationshipAsync",
                new { QuoteId = quoteId, ActivityId = activityId, EntityType = nameof(Quote_Activity) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Removes all relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForQuoteAsync(Guid quoteId, CancellationToken cancellationToken = default)
        {
            if (await DeleteByQuoteIdAsync(quoteId, cancellationToken))
            {
                // Get approximate count by querying with Active = 0
                var sql = $@"
                    SELECT COUNT(*)
                    FROM {TableName}
                    WHERE {FirstIdColumnName} = @FirstId
                      AND Active = 0";

                return await ExecuteWithResilienceAndLoggingAsync(
                    async connection => await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            new { FirstId = quoteId },
                            cancellationToken: cancellationToken)),
                    "RemoveAllForQuoteAsync",
                    new { QuoteId = quoteId, EntityType = nameof(Quote_Activity) },
                    cancellationToken);
            }

            return 0;
        }

        /// <summary>
        /// Removes all relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForActivityAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            if (await DeleteByActivityIdAsync(activityId, cancellationToken))
            {
                // Get approximate count by querying with Active = 0
                var sql = $@"
                    SELECT COUNT(*)
                    FROM {TableName}
                    WHERE {SecondIdColumnName} = @SecondId
                      AND Active = 0";

                return await ExecuteWithResilienceAndLoggingAsync(
                    async connection => await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            new { SecondId = activityId },
                            cancellationToken: cancellationToken)),
                    "RemoveAllForActivityAsync",
                    new { ActivityId = activityId, EntityType = nameof(Quote_Activity) },
                    cancellationToken);
            }

            return 0;
        }

        /// <summary>
        /// Deletes all quote-activity relationships for a specific quote
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
                new { QuoteId = quoteId, EntityType = nameof(Quote_Activity) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all quote-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            var parameters = new
            {
                ActivityId = activityId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByActivityIdAsync",
                new { ActivityId = activityId, EntityType = nameof(Quote_Activity) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes a specific relationship between a quote and an activity
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was deleted, otherwise false</returns>
        public async Task<bool> DeleteByQuoteAndActivityAsync(Guid quoteId, Guid activityId, CancellationToken cancellationToken = default)
        {
            return await RemoveRelationshipAsync(quoteId, activityId, cancellationToken);
        }
    }
}