using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing EmailAddressType entities
    /// </summary>
    public class EmailAddressTypeRepository : BaseTypeStatusRepository<EmailAddressType>, IEmailAddressTypeRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "EmailAddressType";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "EmailAddressTypeId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "EmailAddressTypeId", "Type", "Description", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the EmailAddressTypeRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public EmailAddressTypeRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<EmailAddressTypeRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new email address type
        /// </summary>
        /// <param name="entity">The email address type to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added email address type</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<EmailAddressType> AddAsync(EmailAddressType entity, CancellationToken cancellationToken = default)
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
                INSERT INTO EmailAddressType (
                    EmailAddressTypeId,
                    Type,
                    Description,
                    OrdinalPosition,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @EmailAddressTypeId,
                    @Type,
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
                EmailAddressTypeId = entity.Id,
                entity.Type,
                entity.Description,
                entity.OrdinalPosition,
                entity.CreatedDate,
                entity.CreatedBy,
                entity.ModifiedDate,
                entity.ModifiedBy,
                entity.Active
            };

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding {typeof(EmailAddressType).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(EmailAddressType) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing email address type
        /// </summary>
        /// <param name="entity">The email address type to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated email address type</returns>
        public override async Task<EmailAddressType> UpdateAsync(EmailAddressType entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE EmailAddressType
                SET Type = @Type,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE EmailAddressTypeId = @EmailAddressTypeId";

            var parameters = new
            {
                EmailAddressTypeId = entity.Id,
                entity.Type,
                entity.Description,
                entity.OrdinalPosition,
                entity.ModifiedDate,
                entity.ModifiedBy,
                entity.Active
            };

            var affectedRows = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating {typeof(EmailAddressType).Name} with ID {entity.Id}", EntityId = entity.Id.ToString(), EntityType = nameof(EmailAddressType) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Checks if an email address type with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if an email address type with the specified ID exists, otherwise false</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM {TableName}
                    WHERE {IdColumnName} = @Id
                    AND Active = 1
                ) THEN 1 ELSE 0 END";

            var count = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { Id = id },
                        cancellationToken: cancellationToken)),
                "ExistsAsync",
                new { ErrorMessage = $"Error checking existence of {typeof(EmailAddressType).Name} with ID {id}", EntityId = id.ToString(), EntityType = nameof(EmailAddressType) },
                cancellationToken);

            return count == 1;
        }

        /// <summary>
        /// Gets email address types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of email address types ordered by their ordinal position</returns>
        public async Task<IEnumerable<EmailAddressType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<EmailAddressType>>(
                async connection => await connection.QueryAsync<EmailAddressType>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetByOrdinalPositionAsync",
                new { ErrorMessage = $"Error retrieving {typeof(EmailAddressType).Name} ordered by ordinal position", EntityType = nameof(EmailAddressType) },
                cancellationToken);
        }

        /// <summary>
        /// Gets email address types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of email address types with the specified type name</returns>
        public async Task<IEnumerable<EmailAddressType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Type LIKE @Type
                AND Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<EmailAddressType>>(
                async connection => await connection.QueryAsync<EmailAddressType>(
                    new CommandDefinition(
                        sql,
                        new { Type = $"%{type}%" },
                        cancellationToken: cancellationToken)),
                "GetByTypeAsync",
                new { ErrorMessage = $"Error retrieving {typeof(EmailAddressType).Name} by type name", TypeName = type, EntityType = nameof(EmailAddressType) },
                cancellationToken);
        }

        /// <summary>
        /// Gets the default email address type (typically the one with the lowest ordinal position)
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default email address type or null if not found</returns>
        public async Task<EmailAddressType?> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<EmailAddressType?>(
                async connection => await connection.QueryFirstOrDefaultAsync<EmailAddressType>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetDefaultAsync",
                new { ErrorMessage = $"Error retrieving default {typeof(EmailAddressType).Name}", EntityType = nameof(EmailAddressType) },
                cancellationToken);
        }
    }
}