using Dapper;
using Microsoft.Extensions.Logging;
using System.Text;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing SalesOrderStatus entities
    /// </summary>
    public class SalesOrderStatusRepository : BaseRepository<SalesOrderStatus, Guid>, ISalesOrderStatusRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "SalesOrderStatus";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "SalesOrderStatusId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "SalesOrderStatusId", "Status", "Description", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Predefined open status names for consistent business rule application
        /// </summary>
        private static readonly string[] OpenStatuses =
        {
            "New", "Open", "In Progress", "Processing", "Pending"
        };

        /// <summary>
        /// Predefined completed status names for consistent business rule application
        /// </summary>
        private static readonly string[] CompletedStatuses =
        {
            "Completed", "Delivered", "Closed", "Fulfilled", "Shipped"
        };

        /// <summary>
        /// Initializes a new instance of the SalesOrderStatusRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        public SalesOrderStatusRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<SalesOrderStatusRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets sales order statuses ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order statuses ordered by their ordinal position</returns>
        public async Task<IEnumerable<SalesOrderStatus>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrderStatus>>(
                async (connection) => await connection.QueryAsync<SalesOrderStatus>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetByOrdinalPositionAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets sales order statuses by status name
        /// </summary>
        /// <param name="status">The status name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order statuses with the specified status name</returns>
        public async Task<IEnumerable<SalesOrderStatus>> GetByStatusAsync(string status, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Status = @Status
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrderStatus>>(
                async (connection) => await connection.QueryAsync<SalesOrderStatus>(
                    new CommandDefinition(
                        sql,
                        new { Status = status },
                        cancellationToken: cancellationToken)),
                "GetByStatusAsync",
                new { Status = status, TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets the default sales order status
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default sales order status, or null if no sales order statuses exist</returns>
        public async Task<SalesOrderStatus?> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            // Typically the default status would have the lowest ordinal position
            var sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<SalesOrderStatus?>(
                async (connection) => await connection.QueryFirstOrDefaultAsync<SalesOrderStatus>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetDefaultAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets open sales order statuses that represent active orders
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order statuses that represent open orders</returns>
        public async Task<IEnumerable<SalesOrderStatus>> GetOpenStatusesAsync(CancellationToken cancellationToken = default)
        {
            // Create SQL IN clause parameters dynamically to avoid SQL injection
            var parameters = new DynamicParameters();
            var sqlBuilder = new StringBuilder();

            sqlBuilder.AppendLine($"SELECT {string.Join(", ", SelectColumns)}");
            sqlBuilder.AppendLine($"FROM {TableName}");
            sqlBuilder.AppendLine("WHERE Active = 1");

            // Only add the IN clause if we have open statuses defined
            if (OpenStatuses.Length > 0)
            {
                sqlBuilder.Append("AND Status IN (");

                for (int i = 0; i < OpenStatuses.Length; i++)
                {
                    string paramName = $"@Status{i}";
                    parameters.Add(paramName, OpenStatuses[i]);

                    sqlBuilder.Append(paramName);
                    if (i < OpenStatuses.Length - 1)
                        sqlBuilder.Append(", ");
                }

                sqlBuilder.AppendLine(")");
            }

            sqlBuilder.AppendLine("ORDER BY OrdinalPosition ASC");

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrderStatus>>(
                async (connection) => await connection.QueryAsync<SalesOrderStatus>(
                    new CommandDefinition(
                        sqlBuilder.ToString(),
                        parameters,
                        cancellationToken: cancellationToken)),
                "GetOpenStatusesAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets completed sales order statuses
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order statuses that represent completed orders</returns>
        public async Task<IEnumerable<SalesOrderStatus>> GetCompletedStatusesAsync(CancellationToken cancellationToken = default)
        {
            // Create SQL IN clause parameters dynamically to avoid SQL injection
            var parameters = new DynamicParameters();
            var sqlBuilder = new StringBuilder();

            sqlBuilder.AppendLine($"SELECT {string.Join(", ", SelectColumns)}");
            sqlBuilder.AppendLine($"FROM {TableName}");
            sqlBuilder.AppendLine("WHERE Active = 1");

            // Only add the IN clause if we have completed statuses defined
            if (CompletedStatuses.Length > 0)
            {
                sqlBuilder.Append("AND Status IN (");

                for (int i = 0; i < CompletedStatuses.Length; i++)
                {
                    string paramName = $"@Status{i}";
                    parameters.Add(paramName, CompletedStatuses[i]);

                    sqlBuilder.Append(paramName);
                    if (i < CompletedStatuses.Length - 1)
                        sqlBuilder.Append(", ");
                }

                sqlBuilder.AppendLine(")");
            }

            sqlBuilder.AppendLine("ORDER BY OrdinalPosition ASC");

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrderStatus>>(
                async (connection) => await connection.QueryAsync<SalesOrderStatus>(
                    new CommandDefinition(
                        sqlBuilder.ToString(),
                        parameters,
                        cancellationToken: cancellationToken)),
                "GetCompletedStatusesAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new sales order status
        /// </summary>
        /// <param name="entity">The sales order status to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added sales order status</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<SalesOrderStatus> AddAsync(SalesOrderStatus entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Ensure ID is set
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            // Set audit fields if not already set
            if (entity.CreatedDate == default)
            {
                entity.CreatedDate = DateTime.UtcNow;
            }

            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = entity.CreatedDate;
            }

            // Ensure Active is set
            entity.Active = true;

            var sql = @"
                INSERT INTO SalesOrderStatus (
                    SalesOrderStatusId,
                    Status,
                    Description,
                    OrdinalPosition,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @SalesOrderStatusId,
                    @Status,
                    @Description,
                    @OrdinalPosition,
                    @CreatedDate,
                    @CreatedBy,
                    @ModifiedDate,
                    @ModifiedBy,
                    @Active
                )";

            var parameters = new
            {
                SalesOrderStatusId = entity.Id,
                entity.Status,
                entity.Description,
                entity.OrdinalPosition,
                entity.CreatedDate,
                entity.CreatedBy,
                entity.ModifiedDate,
                entity.ModifiedBy,
                entity.Active
            };

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding {typeof(SalesOrderStatus).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(SalesOrderStatus) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing sales order status
        /// </summary>
        /// <param name="entity">The sales order status to update</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated sales order status</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<SalesOrderStatus> UpdateAsync(SalesOrderStatus entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE SalesOrderStatus
                SET Status = @Status,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE SalesOrderStatusId = @SalesOrderStatusId
                AND Active = 1";

            var parameters = new
            {
                SalesOrderStatusId = entity.Id,
                entity.Status,
                entity.Description,
                entity.OrdinalPosition,
                entity.ModifiedDate,
                entity.ModifiedBy,
                entity.Active
            };

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating {typeof(SalesOrderStatus).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(SalesOrderStatus) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Checks if a sales order status with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier of the sales order status to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a sales order status with the specified identifier exists; otherwise, false</returns>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM SalesOrderStatus
                    WHERE SalesOrderStatusId = @Id
                    AND Active = 1
                ) THEN 1 ELSE 0 END";

            return await ExecuteWithResilienceAndLoggingAsync<bool>(
                async (connection) => await connection.ExecuteScalarAsync<bool>(
                    new CommandDefinition(
                        sql,
                        new { Id = id },
                        cancellationToken: cancellationToken)),
                "ExistsAsync",
                new { Id = id, TableName },
                cancellationToken);
        }
    }
}