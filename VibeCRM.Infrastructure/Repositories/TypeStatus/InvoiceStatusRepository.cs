using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing InvoiceStatus entities
    /// </summary>
    public class InvoiceStatusRepository : BaseRepository<InvoiceStatus, Guid>, IInvoiceStatusRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "InvoiceStatus";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "InvoiceStatusId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "InvoiceStatusId", "Status", "Description", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the InvoiceStatusRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        public InvoiceStatusRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<InvoiceStatusRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets invoice statuses ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice statuses ordered by their ordinal position</returns>
        public async Task<IEnumerable<InvoiceStatus>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<InvoiceStatus>>(
                async (connection) => await connection.QueryAsync<InvoiceStatus>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetByOrdinalPositionAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets invoice statuses by status name
        /// </summary>
        /// <param name="status">The status name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice statuses with the specified status name</returns>
        public async Task<IEnumerable<InvoiceStatus>> GetByStatusAsync(string status, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Status = @Status
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<InvoiceStatus>>(
                async (connection) => await connection.QueryAsync<InvoiceStatus>(
                    new CommandDefinition(
                        sql,
                        new { Status = status },
                        cancellationToken: cancellationToken)),
                "GetByStatusAsync",
                new { Status = status, TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets the default invoice status
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default invoice status, or null if no invoice statuses exist</returns>
        public async Task<InvoiceStatus?> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            // Typically the default status would have the lowest ordinal position
            var sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<InvoiceStatus?>(
                async (connection) => await connection.QueryFirstOrDefaultAsync<InvoiceStatus>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetDefaultAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new invoice status
        /// </summary>
        /// <param name="entity">The invoice status to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added invoice status</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<InvoiceStatus> AddAsync(InvoiceStatus entity, CancellationToken cancellationToken = default)
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
                INSERT INTO InvoiceStatus (
                    InvoiceStatusId,
                    Status,
                    Description,
                    OrdinalPosition,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @InvoiceStatusId,
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
                InvoiceStatusId = entity.Id,
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
                new { ErrorMessage = $"Error adding {typeof(InvoiceStatus).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(InvoiceStatus) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing invoice status
        /// </summary>
        /// <param name="entity">The invoice status to update</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated invoice status</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<InvoiceStatus> UpdateAsync(InvoiceStatus entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE InvoiceStatus
                SET Status = @Status,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE InvoiceStatusId = @InvoiceStatusId
                AND Active = 1";

            var parameters = new
            {
                InvoiceStatusId = entity.Id,
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
                new { ErrorMessage = $"Error updating {typeof(InvoiceStatus).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(InvoiceStatus) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Checks if an invoice status with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier of the invoice status to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if an invoice status with the specified identifier exists; otherwise, false</returns>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM InvoiceStatus
                    WHERE InvoiceStatusId = @Id
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