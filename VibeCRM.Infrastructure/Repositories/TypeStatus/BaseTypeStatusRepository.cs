using Dapper;
using Microsoft.Extensions.Logging;
using Polly;
using System.Data.Common;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Base implementation for TypeStatus repositories providing common functionality for type/status entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public abstract class BaseTypeStatusRepository<TEntity> where TEntity : class, IEntity, ISoftDelete
    {
        private readonly ISQLConnectionFactory _connectionFactory;
        protected readonly ILogger<BaseTypeStatusRepository<TEntity>> _logger;
        private readonly AsyncPolicy _resiliencePolicy;

        /// <summary>
        /// Gets the name of the database table for the entity
        /// </summary>
        protected abstract string TableName { get; }

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected abstract string IdColumnName { get; }

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected abstract string[] SelectColumns { get; }

        /// <summary>
        /// Initializes a new instance of the BaseTypeStatusRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        protected BaseTypeStatusRepository(ISQLConnectionFactory connectionFactory, ILogger<BaseTypeStatusRepository<TEntity>> logger)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // Configure resilience policy with retry
            _resiliencePolicy = Policy
                .Handle<DbException>()
                .Or<TimeoutException>()
                .WaitAndRetryAsync(
                    3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning(
                            exception,
                            "Error during database operation (Attempt {RetryCount}). Waiting {RetryTimeSpan} before next retry. Details: {ExceptionMessage}",
                            retryCount, timeSpan, exception.Message);
                    });
        }

        /// <summary>
        /// Gets an entity by its ID
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The entity or null if not found</returns>
        public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {IdColumnName} = @Id
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QuerySingleOrDefaultAsync<TEntity>(
                    sql, new { Id = id }),
                "GetByIdAsync",
                new { Id = id, EntityType = typeof(TEntity).Name },
                cancellationToken);
        }

        /// <summary>
        /// Gets all active entities
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of all active entities</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<TEntity>(sql),
                "GetAllAsync",
                new { EntityType = typeof(TEntity).Name },
                cancellationToken);
        }

        /// <summary>
        /// Gets entities ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of entities ordered by their ordinal position</returns>
        public virtual async Task<IEnumerable<TEntity>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<TEntity>(sql),
                "GetByOrdinalPositionAsync",
                new { EntityType = typeof(TEntity).Name },
                cancellationToken);
        }

        /// <summary>
        /// Gets entities by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of entities with the specified type name</returns>
        public virtual async Task<IEnumerable<TEntity>> GetByTypeAsync(string type, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Type = @Type
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<TEntity>(
                    sql, new { Type = type }),
                "GetByTypeAsync",
                new { Type = type, EntityType = typeof(TEntity).Name },
                cancellationToken);
        }

        /// <summary>
        /// Gets the default entity (typically the one with the lowest ordinal position)
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The default entity or null if none is found</returns>
        public virtual async Task<TEntity?> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QuerySingleOrDefaultAsync<TEntity>(sql),
                "GetDefaultAsync",
                new { EntityType = typeof(TEntity).Name },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new entity
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added entity</returns>
        public abstract Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing entity
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated entity</returns>
        public abstract Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an entity by its ID (soft delete)
        /// </summary>
        /// <param name="id">The ID of the entity to delete</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>True if the entity was deleted successfully; otherwise, false</returns>
        public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {IdColumnName} = @Id";

            var affected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    sql, new { Id = id, ModifiedDate = DateTime.UtcNow }),
                "DeleteAsync",
                new { Id = id, EntityType = typeof(TEntity).Name },
                cancellationToken);

            return affected > 0;
        }

        /// <summary>
        /// Executes a database operation with resilience and logging
        /// </summary>
        /// <typeparam name="TResult">The type of the result</typeparam>
        /// <param name="operation">The database operation to execute</param>
        /// <param name="operationName">The name of the operation for logging purposes</param>
        /// <param name="context">The context information for logging</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The result of the database operation</returns>
        /// <exception cref="Exception">Thrown when the operation fails after all retries</exception>
        protected async Task<TResult> ExecuteWithResilienceAndLoggingAsync<TResult>(
            Func<System.Data.IDbConnection, Task<TResult>> operation,
            string operationName,
            object context,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogDebug("Executing {OperationName} for {EntityType}",
                    operationName, typeof(TEntity).Name);

                return await _resiliencePolicy.ExecuteAsync(async (ct) =>
                {
                    using var connection = _connectionFactory.CreateConnection();
                    return await operation(connection);
                }, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {OperationName} for {EntityType}: {ExceptionMessage}",
                    operationName, typeof(TEntity).Name, ex.Message);
                throw;
            }
        }
    }
}