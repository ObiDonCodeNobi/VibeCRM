using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System.Data;
using VibeCRM.Domain.Interfaces.Repositories;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories
{
    /// <summary>
    /// Base repository implementation that provides common functionality for all repositories
    /// with resilience using Polly
    /// </summary>
    /// <typeparam name="TEntity">The entity type this repository operates on</typeparam>
    /// <typeparam name="TId">The type of the entity's primary key</typeparam>
    public abstract class BaseRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        private readonly ISQLConnectionFactory _connectionFactory;
        protected readonly ILogger _logger;
        private readonly AsyncRetryPolicy _retryPolicy;

        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected abstract string TableName { get; }

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected abstract string IdColumnName { get; }

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected abstract string[] SelectColumns { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TEntity, TId}"/> class.
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for capturing errors and information</param>
        protected BaseRepository(ISQLConnectionFactory connectionFactory, ILogger logger)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // Configure Polly retry policy for SQL transient errors
            _retryPolicy = Policy
                .Handle<SqlException>(ex => IsTransientError(ex))
                .Or<TimeoutException>()
                .WaitAndRetryAsync(
                    3, // Retry 3 times
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // Exponential backoff
                    (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning(
                            exception,
                            "Error executing database command in {Repository}. Retrying {RetryCount}/3 after {RetryTimeSpan}s",
                            GetType().Name,
                            retryCount,
                            timeSpan.TotalSeconds);
                    });
        }

        /// <summary>
        /// Creates a new database connection using the connection factory.
        /// </summary>
        /// <returns>A database connection instance</returns>
        protected IDbConnection CreateConnection()
        {
            return _connectionFactory.CreateConnection();
        }

        /// <summary>
        /// Executes a database query with resilience.
        /// </summary>
        /// <typeparam name="T">The type of result to return</typeparam>
        /// <param name="query">The asynchronous query function to execute</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The result of the query function</returns>
        protected async Task<T> ExecuteWithResilienceAsync<T>(Func<IDbConnection, Task<T>> query, CancellationToken cancellationToken = default)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                // Create a new connection for each request
                using var connection = CreateConnection();
                return await query(connection);
            });
        }

        /// <summary>
        /// Executes a database command with resilience and logs any exceptions.
        /// </summary>
        /// <typeparam name="T">The type of result to return</typeparam>
        /// <param name="query">The asynchronous query function to execute</param>
        /// <param name="operationName">Name of the operation for logging</param>
        /// <param name="additionalLogDetails">Additional details to log with any exceptions</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The result of the query function</returns>
        protected async Task<T> ExecuteWithResilienceAndLoggingAsync<T>(
            Func<IDbConnection, Task<T>> query,
            string operationName,
            object? additionalLogDetails = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await ExecuteWithResilienceAsync(query, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during {OperationName} in {Repository}. {AdditionalDetails}",
                    operationName,
                    GetType().Name,
                    additionalLogDetails ?? new { });
                throw;
            }
        }

        /// <summary>
        /// Determines if the SQL exception is a transient error that should be retried.
        /// </summary>
        /// <param name="ex">The SQL exception to check</param>
        /// <returns>True if the error is transient, otherwise false</returns>
        private bool IsTransientError(SqlException ex)
        {
            // SQL Server transient error numbers
            int[] transientErrorNumbers = {
                -2, // Timeout
                4060, // Cannot open database
                40197, // The service has encountered an error processing your request
                40501, // The service is currently busy
                40613, // Database is currently unavailable
                49918, // Cannot process request
                49919, // Cannot process create or update request
                49920, // Service is busy
                11001, // Host not found
            };

            return Array.IndexOf(transientErrorNumbers, ex.Number) >= 0;
        }

        /// <summary>
        /// Builds a comma-separated list of columns for use in SELECT statements
        /// </summary>
        /// <returns>A comma-separated list of column names</returns>
        protected virtual string GetColumnList()
        {
            return string.Join(", ", SelectColumns);
        }

        /// <summary>
        /// Gets all entities from the repository
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of all entities in the repository</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            string sql = $"SELECT {GetColumnList()} FROM {TableName} WHERE Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<TEntity>(sql),
                "GetAllAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets an entity by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the entity</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The entity if found, otherwise null</returns>
        public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            string sql = $"SELECT {GetColumnList()} FROM {TableName} WHERE {IdColumnName} = @Id AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<TEntity>(sql, new { Id = id }),
                "GetByIdAsync",
                new { Id = id, TableName },
                cancellationToken);
        }

        /// <summary>
        /// Checks if an entity with the specified ID exists in the repository
        /// </summary>
        /// <param name="id">The unique identifier of the entity</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the entity exists, otherwise false</returns>
        public virtual async Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default)
        {
            string sql = $"SELECT CASE WHEN EXISTS(SELECT 1 FROM {TableName} WHERE {IdColumnName} = @Id AND Active = 1) THEN 1 ELSE 0 END";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<bool>(sql, new { Id = id }),
                "ExistsAsync",
                new { Id = id, TableName },
                cancellationToken);
        }

        /// <summary>
        /// Soft deletes an entity by setting its Active flag to 0
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the entity was deleted, false if not found</returns>
        public virtual async Task<bool> DeleteAsync(TId id, CancellationToken cancellationToken = default)
        {
            string sql = $"UPDATE {TableName} SET Active = 0, ModifiedDate = GETUTCDATE() WHERE {IdColumnName} = @Id AND Active = 1";

            int affected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(sql, new { Id = id }),
                "DeleteAsync",
                new { Id = id, TableName },
                cancellationToken);

            return affected > 0;
        }

        /// <summary>
        /// Adds a new entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added entity with any system-generated values (like IDs) populated</returns>
        public abstract Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated entity</returns>
        public abstract Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}