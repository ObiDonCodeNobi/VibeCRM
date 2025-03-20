using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing PaymentStatus entities
    /// </summary>
    public class PaymentStatusRepository : BaseRepository<PaymentStatus, Guid>, IPaymentStatusRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "PaymentStatus";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "PaymentStatusId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "PaymentStatusId", "Status", "Description", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the PaymentStatusRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        public PaymentStatusRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PaymentStatusRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new payment status
        /// </summary>
        /// <param name="entity">The payment status to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added payment status</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<PaymentStatus> AddAsync(PaymentStatus entity, CancellationToken cancellationToken = default)
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
                INSERT INTO PaymentStatus (
                    PaymentStatusId,
                    Status,
                    Description,
                    OrdinalPosition,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @PaymentStatusId,
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
                PaymentStatusId = entity.Id,
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
                new { ErrorMessage = $"Error adding {typeof(PaymentStatus).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(PaymentStatus) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing payment status
        /// </summary>
        /// <param name="entity">The payment status to update</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated payment status</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<PaymentStatus> UpdateAsync(PaymentStatus entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE PaymentStatus
                SET Status = @Status,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE PaymentStatusId = @PaymentStatusId
                AND Active = 1";

            var parameters = new
            {
                PaymentStatusId = entity.Id,
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
                new { ErrorMessage = $"Error updating {typeof(PaymentStatus).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(PaymentStatus) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Checks if a payment status with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier of the payment status to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a payment status with the specified identifier exists; otherwise, false</returns>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM PaymentStatus
                    WHERE PaymentStatusId = @Id
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

        /// <summary>
        /// Gets a payment status by name
        /// </summary>
        /// <param name="status">The name of the payment status to retrieve</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The payment status if found; otherwise, null</returns>
        public async Task<PaymentStatus?> GetByNameAsync(string status, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Status = @Status
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<PaymentStatus?>(
                async (connection) => await connection.QueryFirstOrDefaultAsync<PaymentStatus>(
                    new CommandDefinition(
                        sql,
                        new { Status = status },
                        cancellationToken: cancellationToken)),
                "GetByNameAsync",
                new { Status = status, TableName },
                cancellationToken);
        }
    }
}