using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Invoice_Activity junction entities
    /// </summary>
    public class InvoiceActivityRepository : BaseJunctionRepository<Invoice_Activity, Guid, Guid>, IInvoiceActivityRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Invoice_Activity";

        /// <summary>
        /// Gets the name of the first ID column (InvoiceId)
        /// </summary>
        protected override string FirstIdColumnName => "InvoiceId";

        /// <summary>
        /// Gets the name of the second ID column (ActivityId)
        /// </summary>
        protected override string SecondIdColumnName => "ActivityId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "InvoiceId", "ActivityId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceActivityRepository"/> class.
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public InvoiceActivityRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<InvoiceActivityRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all invoice-activity relationships for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice-activity relationships for the specified invoice</returns>
        public async Task<IEnumerable<Invoice_Activity>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @InvoiceId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Invoice_Activity>>(
                async (connection) => await connection.QueryAsync<Invoice_Activity>(
                    new CommandDefinition(
                        sql,
                        new { InvoiceId = invoiceId },
                        cancellationToken: cancellationToken)),
                "GetByInvoiceIdAsync",
                new { InvoiceId = invoiceId, TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets all invoice-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice-activity relationships for the specified activity</returns>
        public async Task<IEnumerable<Invoice_Activity>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Invoice_Activity>>(
                async (connection) => await connection.QueryAsync<Invoice_Activity>(
                    new CommandDefinition(
                        sql,
                        new { ActivityId = activityId },
                        cancellationToken: cancellationToken)),
                "GetByActivityIdAsync",
                new { ActivityId = activityId, TableName },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship between the specified invoice and activity exists
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists; otherwise, false</returns>
        public async Task<bool> ExistsAsync(Guid invoiceId, Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM {TableName}
                    WHERE {FirstIdColumnName} = @InvoiceId
                    AND {SecondIdColumnName} = @ActivityId
                    AND Active = 1
                ) THEN 1 ELSE 0 END";

            return await ExecuteWithResilienceAndLoggingAsync<bool>(
                async (connection) => await connection.ExecuteScalarAsync<bool>(
                    new CommandDefinition(
                        sql,
                        new { InvoiceId = invoiceId, ActivityId = activityId },
                        cancellationToken: cancellationToken)),
                "ExistsAsync",
                new { InvoiceId = invoiceId, ActivityId = activityId, TableName },
                cancellationToken);
        }
    }
}