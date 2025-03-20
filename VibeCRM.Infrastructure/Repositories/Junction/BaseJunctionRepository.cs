using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Interfaces.Repositories;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Base implementation for repositories that manage junction entities with composite keys
    /// </summary>
    /// <typeparam name="TEntity">The type of entity managed by the repository</typeparam>
    /// <typeparam name="TFirstId">The type of the first part of the composite key</typeparam>
    /// <typeparam name="TSecondId">The type of the second part of the composite key</typeparam>
    public abstract class BaseJunctionRepository<TEntity, TFirstId, TSecondId> : IJunctionRepository<TEntity, TFirstId, TSecondId>
        where TEntity : class, IEntity, ISoftDelete
    {
        /// <summary>
        /// SQL Connection factory for creating database connections
        /// </summary>
        protected readonly ISQLConnectionFactory ConnectionFactory;

        /// <summary>
        /// Logger for capturing errors and information
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected abstract string TableName { get; }

        /// <summary>
        /// Gets the name of the first ID column
        /// </summary>
        protected abstract string FirstIdColumnName { get; }

        /// <summary>
        /// Gets the name of the second ID column
        /// </summary>
        protected abstract string SecondIdColumnName { get; }

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected abstract string[] SelectColumns { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseJunctionRepository{TEntity, TFirstId, TSecondId}"/> class.
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        protected BaseJunctionRepository(ISQLConnectionFactory connectionFactory, ILogger logger)
        {
            ConnectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Executes a database operation with resilience and logging
        /// </summary>
        /// <typeparam name="TResult">The type of result to return</typeparam>
        /// <param name="query">The asynchronous query function to execute</param>
        /// <param name="operationName">Name of the operation for logging</param>
        /// <param name="additionalLogDetails">Additional details to log with any exceptions</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The result of the operation</returns>
        protected async Task<TResult> ExecuteWithResilienceAndLoggingAsync<TResult>(
            Func<IDbConnection, Task<TResult>> query,
            string operationName,
            object? additionalLogDetails = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                Logger.LogDebug("Executing {OperationName} operation in {RepositoryName}",
                    operationName, GetType().Name);

                using var connection = ConnectionFactory.CreateConnection();

                var result = await query(connection);

                Logger.LogDebug("Successfully executed {OperationName} operation in {RepositoryName}",
                    operationName, GetType().Name);

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error during {OperationName} in {Repository}. {AdditionalDetails}",
                    operationName,
                    GetType().Name,
                    additionalLogDetails ?? new { });
                throw;
            }
        }

        /// <summary>
        /// Adds a new junction entity to the repository
        /// </summary>
        /// <param name="entity">The junction entity to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added junction entity with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Each implementation will need to define its own specific insert SQL
            // This is a default implementation that can be overridden
            var sql = $@"
                INSERT INTO {TableName} (
                    {FirstIdColumnName}, {SecondIdColumnName}, Active
                )
                VALUES (
                    @FirstId, @SecondId, @Active
                );";

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    sql,
                    new
                    {
                        FirstId = GetPropertyValue(entity, FirstIdColumnName),
                        SecondId = GetPropertyValue(entity, SecondIdColumnName),
                        entity.Active
                    }),
                "AddAsync",
                new { EntityType = typeof(TEntity).Name, Entity = entity },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Gets a property value from an entity by property name
        /// </summary>
        /// <param name="entity">The entity to get the property value from</param>
        /// <param name="propertyName">The name of the property to get</param>
        /// <returns>The property value, which may be null</returns>
        protected object? GetPropertyValue(TEntity entity, string propertyName)
        {
            var property = typeof(TEntity).GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException($"Property {propertyName} not found on type {typeof(TEntity).Name}");
            }
            return property.GetValue(entity);
        }

        /// <summary>
        /// Gets a junction entity by its composite key
        /// </summary>
        /// <param name="firstId">The first part of the composite key</param>
        /// <param name="secondId">The second part of the composite key</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The junction entity if found, otherwise null</returns>
        public virtual async Task<TEntity?> GetByCompositeIdAsync(TFirstId firstId, TSecondId secondId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @FirstId
                  AND {SecondIdColumnName} = @SecondId
                  AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<TEntity>(
                    sql, new { FirstId = firstId, SecondId = secondId }),
                "GetByCompositeIdAsync",
                new { FirstId = firstId, SecondId = secondId, EntityType = typeof(TEntity).Name },
                cancellationToken);
        }

        /// <summary>
        /// Gets a junction entity by its composite key
        /// </summary>
        /// <param name="firstId">The first part of the composite key</param>
        /// <param name="secondId">The second part of the composite key</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The junction entity if found, otherwise null</returns>
        public virtual async Task<TEntity?> GetByIdAsync(TFirstId firstId, TSecondId secondId, CancellationToken cancellationToken = default)
        {
            return await GetByCompositeIdAsync(firstId, secondId, cancellationToken);
        }

        /// <summary>
        /// Gets all junction entities associated with a specific first ID
        /// </summary>
        /// <param name="firstId">The first part of the composite key to filter by</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of junction entities with the specified first ID</returns>
        public virtual async Task<IEnumerable<TEntity>> GetByFirstIdAsync(TFirstId firstId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @FirstId AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<TEntity>(
                    sql, new { FirstId = firstId }),
                "GetByFirstIdAsync",
                new { FirstId = firstId, EntityType = typeof(TEntity).Name },
                cancellationToken);
        }

        /// <summary>
        /// Gets all junction entities associated with a specific second ID
        /// </summary>
        /// <param name="secondId">The second part of the composite key to filter by</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of junction entities with the specified second ID</returns>
        public virtual async Task<IEnumerable<TEntity>> GetBySecondIdAsync(TSecondId secondId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @SecondId AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<TEntity>(
                    sql, new { SecondId = secondId }),
                "GetBySecondIdAsync",
                new { SecondId = secondId, EntityType = typeof(TEntity).Name },
                cancellationToken);
        }

        /// <summary>
        /// Gets all junction entities in the repository
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of all junction entities</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<TEntity>(
                    sql),
                "GetAllAsync",
                new { EntityType = typeof(TEntity).Name },
                cancellationToken);
        }

        /// <summary>
        /// Deletes a junction entity by its composite key (soft delete)
        /// </summary>
        /// <param name="firstId">The first part of the composite key</param>
        /// <param name="secondId">The second part of the composite key</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the junction entity was successfully deleted, otherwise false</returns>
        public virtual async Task<bool> DeleteAsync(TFirstId firstId, TSecondId secondId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0,
                    ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @FirstId
                  AND {SecondIdColumnName} = @SecondId";

            int rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    sql,
                    new
                    {
                        FirstId = firstId,
                        SecondId = secondId,
                        ModifiedDate = DateTime.UtcNow
                    }),
                "DeleteAsync",
                new { FirstId = firstId, SecondId = secondId, EntityType = typeof(TEntity).Name },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all junction entities associated with a specific first ID (soft delete)
        /// </summary>
        /// <param name="firstId">The first part of the composite key</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if any junction entities were successfully deleted, otherwise false</returns>
        public virtual async Task<bool> DeleteByFirstIdAsync(TFirstId firstId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0,
                    ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @FirstId";

            int rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    sql,
                    new
                    {
                        FirstId = firstId,
                        ModifiedDate = DateTime.UtcNow
                    }),
                "DeleteByFirstIdAsync",
                new { FirstId = firstId, EntityType = typeof(TEntity).Name },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all junction entities associated with a specific second ID (soft delete)
        /// </summary>
        /// <param name="secondId">The second part of the composite key</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if any junction entities were successfully deleted, otherwise false</returns>
        public virtual async Task<bool> DeleteBySecondIdAsync(TSecondId secondId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0,
                    ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @SecondId";

            int rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    sql,
                    new
                    {
                        SecondId = secondId,
                        ModifiedDate = DateTime.UtcNow
                    }),
                "DeleteBySecondIdAsync",
                new { SecondId = secondId, EntityType = typeof(TEntity).Name },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Updates an existing junction entity in the repository
        /// </summary>
        /// <param name="entity">The junction entity to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated junction entity</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var sql = $@"
                UPDATE {TableName}
                SET Active = @Active,
                    ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @FirstId
                  AND {SecondIdColumnName} = @SecondId";

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    sql,
                    new
                    {
                        FirstId = GetPropertyValue(entity, FirstIdColumnName),
                        SecondId = GetPropertyValue(entity, SecondIdColumnName),
                        entity.Active,
                        ModifiedDate = DateTime.UtcNow
                    }),
                "UpdateAsync",
                new { EntityType = typeof(TEntity).Name, Entity = entity },
                cancellationToken);

            return entity;
        }
    }
}