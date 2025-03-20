using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing PersonStatus entities
    /// </summary>
    public class PersonStatusRepository : BaseRepository<PersonStatus, Guid>, IPersonStatusRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "PersonStatus";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "PersonStatusId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "PersonStatusId", "Status", "Description", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the PersonStatusRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        public PersonStatusRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PersonStatusRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets person statuses ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person statuses ordered by their ordinal position</returns>
        public async Task<IEnumerable<PersonStatus>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<PersonStatus>>(
                async (connection) => await connection.QueryAsync<PersonStatus>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetByOrdinalPositionAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets person statuses by status name
        /// </summary>
        /// <param name="status">The status name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person statuses with the specified status name</returns>
        public async Task<IEnumerable<PersonStatus>> GetByStatusAsync(string status, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Status = @Status
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<PersonStatus>>(
                async (connection) => await connection.QueryAsync<PersonStatus>(
                    new CommandDefinition(
                        sql,
                        new { Status = status },
                        cancellationToken: cancellationToken)),
                "GetByStatusAsync",
                new { Status = status, TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets the default person status
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default person status, or null if no person statuses exist</returns>
        public async Task<PersonStatus?> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            // Typically the default status would have the lowest ordinal position
            // or a specific name like "Active" or "New"
            var sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<PersonStatus?>(
                async (connection) => await connection.QueryFirstOrDefaultAsync<PersonStatus>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetDefaultAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new person status
        /// </summary>
        /// <param name="entity">The person status to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added person status</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<PersonStatus> AddAsync(PersonStatus entity, CancellationToken cancellationToken = default)
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
                INSERT INTO PersonStatus (
                    PersonStatusId,
                    Status,
                    Description,
                    OrdinalPosition,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @PersonStatusId,
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
                PersonStatusId = entity.Id,
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
                new { ErrorMessage = $"Error adding {typeof(PersonStatus).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(PersonStatus) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing person status
        /// </summary>
        /// <param name="entity">The person status to update</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated person status</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<PersonStatus> UpdateAsync(PersonStatus entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE PersonStatus
                SET Status = @Status,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE PersonStatusId = @PersonStatusId
                AND Active = 1";

            var parameters = new
            {
                PersonStatusId = entity.Id,
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
                new { ErrorMessage = $"Error updating {typeof(PersonStatus).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(PersonStatus) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Checks if a person status with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier of the person status to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a person status with the specified identifier exists; otherwise, false</returns>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM PersonStatus
                    WHERE PersonStatusId = @Id
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
        /// Soft deletes a person status by setting its Active flag to 0
        /// </summary>
        /// <param name="id">The unique identifier of the person status to delete</param>
        /// <param name="modifiedBy">The identifier of the user who is performing the delete operation</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the person status was deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, Guid modifiedBy, CancellationToken cancellationToken = default)
        {
            string sql = $@"
                UPDATE {TableName} 
                SET Active = 0, 
                    ModifiedDate = GETUTCDATE(),
                    ModifiedBy = @ModifiedBy
                WHERE {IdColumnName} = @Id 
                AND Active = 1";

            int affected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        new { Id = id, ModifiedBy = modifiedBy },
                        cancellationToken: cancellationToken)),
                "DeleteAsync",
                new { Id = id, ModifiedBy = modifiedBy, TableName },
                cancellationToken);

            return affected > 0;
        }
    }
}