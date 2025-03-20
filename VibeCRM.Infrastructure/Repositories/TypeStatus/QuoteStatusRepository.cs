using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing QuoteStatus entities
    /// </summary>
    public class QuoteStatusRepository : BaseRepository<QuoteStatus, Guid>, IQuoteStatusRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "QuoteStatus";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "QuoteStatusId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "QuoteStatusId", "Status", "Description", "OrdinalPosition",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Base SELECT query for QuoteStatus entities
        /// </summary>
        private const string BaseSelectQuery = @"
            SELECT qs.QuoteStatusId AS Id, qs.Status, qs.Description, qs.OrdinalPosition,
                   qs.CreatedBy, qs.CreatedDate, qs.ModifiedBy, qs.ModifiedDate, qs.Active
            FROM QuoteStatus qs
            WHERE qs.Active = 1";

        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteStatusRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public QuoteStatusRepository(ISQLConnectionFactory connectionFactory, ILogger<QuoteStatusRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets quote statuses ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote statuses ordered by their ordinal position</returns>
        public async Task<IEnumerable<QuoteStatus>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default)
        {
            string sql = $@"{BaseSelectQuery}
                ORDER BY qs.OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<QuoteStatus>>(
                async connection => await connection.QueryAsync<QuoteStatus>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetByOrdinalPositionAsync",
                new { },
                cancellationToken);
        }

        /// <summary>
        /// Gets quote statuses by status name
        /// </summary>
        /// <param name="status">The status name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote statuses with the specified status name</returns>
        public async Task<IEnumerable<QuoteStatus>> GetByStatusAsync(string status, CancellationToken cancellationToken = default)
        {
            string sql = $@"{BaseSelectQuery}
                AND qs.Status = @Status
                ORDER BY qs.OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<QuoteStatus>>(
                async connection => await connection.QueryAsync<QuoteStatus>(
                    new CommandDefinition(
                        sql,
                        new { Status = status },
                        cancellationToken: cancellationToken)),
                "GetByStatusAsync",
                new { Status = status },
                cancellationToken);
        }

        /// <summary>
        /// Gets the default quote status
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default quote status, or null if no quote statuses exist</returns>
        public async Task<QuoteStatus?> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            // Typically the default status would have the lowest ordinal position
            string sql = $@"{BaseSelectQuery}
                ORDER BY qs.OrdinalPosition ASC
                OFFSET 0 ROWS
                FETCH NEXT 1 ROWS ONLY";

            return await ExecuteWithResilienceAndLoggingAsync<QuoteStatus?>(
                async connection => await connection.QueryFirstOrDefaultAsync<QuoteStatus>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetDefaultAsync",
                new { },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new quote status to the database
        /// </summary>
        /// <param name="entity">The quote status entity to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added quote status with its assigned ID</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<QuoteStatus> AddAsync(QuoteStatus entity, CancellationToken cancellationToken = default)
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
                INSERT INTO QuoteStatus (
                    QuoteStatusId,
                    Status,
                    Description,
                    OrdinalPosition,
                    CreatedBy,
                    CreatedDate,
                    ModifiedBy,
                    ModifiedDate,
                    Active
                ) VALUES (
                    @QuoteStatusId,
                    @Status,
                    @Description,
                    @OrdinalPosition,
                    @CreatedBy,
                    @CreatedDate,
                    @ModifiedBy,
                    @ModifiedDate,
                    @Active
                )";

            var parameters = new
            {
                QuoteStatusId = entity.Id,
                entity.Status,
                entity.Description,
                entity.OrdinalPosition,
                entity.CreatedBy,
                entity.CreatedDate,
                entity.ModifiedBy,
                entity.ModifiedDate,
                entity.Active
            };

            try
            {
                await ExecuteWithResilienceAndLoggingAsync<int>(
                    async connection => await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            parameters,
                            cancellationToken: cancellationToken)),
                    "AddAsync",
                    new { ErrorMessage = $"Error adding {typeof(QuoteStatus).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(QuoteStatus) },
                    cancellationToken);

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding {EntityType} with ID {EntityId}: {ErrorMessage}",
                    nameof(QuoteStatus), entity.Id, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing quote status in the database
        /// </summary>
        /// <param name="entity">The quote status entity to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated quote status</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<QuoteStatus> UpdateAsync(QuoteStatus entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE QuoteStatus
                SET Status = @Status,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate,
                    Active = @Active
                WHERE QuoteStatusId = @QuoteStatusId
                AND Active = 1";

            var parameters = new
            {
                QuoteStatusId = entity.Id,
                entity.Status,
                entity.Description,
                entity.OrdinalPosition,
                entity.ModifiedBy,
                entity.ModifiedDate,
                entity.Active
            };

            try
            {
                await ExecuteWithResilienceAndLoggingAsync<int>(
                    async connection => await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            parameters,
                            cancellationToken: cancellationToken)),
                    "UpdateAsync",
                    new { ErrorMessage = $"Error updating {typeof(QuoteStatus).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(QuoteStatus) },
                    cancellationToken);

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating {EntityType} with ID {EntityId}: {ErrorMessage}",
                    nameof(QuoteStatus), entity.Id, ex.Message);
                throw;
            }
        }
    }
}