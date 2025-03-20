using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Company_Quote junction entities
    /// </summary>
    public class CompanyQuoteRepository : BaseJunctionRepository<Company_Quote, Guid, Guid>, ICompanyQuoteRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Company_Quote";

        /// <summary>
        /// Gets the name of the first ID column (CompanyId)
        /// </summary>
        protected override string FirstIdColumnName => "CompanyId";

        /// <summary>
        /// Gets the name of the second ID column (QuoteId)
        /// </summary>
        protected override string SecondIdColumnName => "QuoteId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "CompanyId", "QuoteId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyQuoteRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public CompanyQuoteRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<CompanyQuoteRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all company-quote relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-quote relationships for the specified company</returns>
        public async Task<IEnumerable<Company_Quote>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                  AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Quote>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyIdAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Quote) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all company-quote relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-quote relationships for the specified quote</returns>
        public async Task<IEnumerable<Company_Quote>> GetByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @QuoteId
                  AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Quote>(
                    new CommandDefinition(
                        sql,
                        new { QuoteId = quoteId },
                        cancellationToken: cancellationToken)),
                "GetByQuoteIdAsync",
                new { QuoteId = quoteId, EntityType = nameof(Company_Quote) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific company-quote relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-quote relationship if found, otherwise null</returns>
        public async Task<Company_Quote?> GetByCompanyAndQuoteIdAsync(Guid companyId, Guid quoteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                  AND {SecondIdColumnName} = @QuoteId
                  AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Company_Quote>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId, QuoteId = quoteId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyAndQuoteIdAsync",
                new { CompanyId = companyId, QuoteId = quoteId, EntityType = nameof(Company_Quote) },
                cancellationToken);
        }

        /// <summary>
        /// Adds a relationship between a company and a quote
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created company-quote relationship</returns>
        public async Task<Company_Quote> AddRelationshipAsync(Guid companyId, Guid quoteId, CancellationToken cancellationToken = default)
        {
            var entity = new Company_Quote
            {
                CompanyId = companyId,
                QuoteId = quoteId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            var sql = $@"
                INSERT INTO {TableName} ({string.Join(", ", SelectColumns)})
                VALUES (@CompanyId, @QuoteId, @Active, @ModifiedDate)";

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        entity,
                        cancellationToken: cancellationToken)),
                "AddRelationshipAsync",
                new { CompanyId = companyId, QuoteId = quoteId, EntityType = nameof(Company_Quote) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Removes a relationship between a company and a quote
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid companyId, Guid quoteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0
                WHERE {FirstIdColumnName} = @CompanyId
                  AND {SecondIdColumnName} = @QuoteId";

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId, QuoteId = quoteId },
                        cancellationToken: cancellationToken)),
                "RemoveRelationshipAsync",
                new { CompanyId = companyId, QuoteId = quoteId, EntityType = nameof(Company_Quote) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all company-quote relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0
                WHERE {FirstIdColumnName} = @CompanyId";

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "DeleteByCompanyIdAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Quote) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all company-quote relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0
                WHERE {SecondIdColumnName} = @QuoteId";

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        new { QuoteId = quoteId },
                        cancellationToken: cancellationToken)),
                "DeleteByQuoteIdAsync",
                new { QuoteId = quoteId, EntityType = nameof(Company_Quote) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Removes all relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            if (await DeleteByCompanyIdAsync(companyId, cancellationToken))
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
                            new { FirstId = companyId },
                            cancellationToken: cancellationToken)),
                    "RemoveAllForCompanyAsync",
                    new { CompanyId = companyId, EntityType = nameof(Company_Quote) },
                    cancellationToken);
            }

            return 0;
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
                    WHERE {SecondIdColumnName} = @SecondId
                      AND Active = 0";

                return await ExecuteWithResilienceAndLoggingAsync(
                    async connection => await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            new { SecondId = quoteId },
                            cancellationToken: cancellationToken)),
                    "RemoveAllForQuoteAsync",
                    new { QuoteId = quoteId, EntityType = nameof(Company_Quote) },
                    cancellationToken);
            }

            return 0;
        }

        /// <summary>
        /// Gets company-quote relationships by date range
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-quote relationships within the specified date range</returns>
        public async Task<IEnumerable<Company_Quote>> GetByDateRangeAsync(Guid companyId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT cq.{string.Join(", cq.", SelectColumns)}
                FROM {TableName} cq
                INNER JOIN Quote q ON cq.QuoteId = q.Id
                WHERE cq.{FirstIdColumnName} = @CompanyId
                  AND q.QuoteDate >= @StartDate
                  AND q.QuoteDate <= @EndDate
                  AND cq.Active = 1
                  AND q.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Quote>(
                    new CommandDefinition(
                        sql,
                        new
                        {
                            CompanyId = companyId,
                            StartDate = startDate,
                            EndDate = endDate
                        },
                        cancellationToken: cancellationToken)),
                "GetByDateRangeAsync",
                new { CompanyId = companyId, StartDate = startDate, EndDate = endDate, EntityType = nameof(Company_Quote) },
                cancellationToken);
        }
    }
}