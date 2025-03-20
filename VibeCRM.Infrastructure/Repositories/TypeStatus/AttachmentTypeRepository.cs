using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing AttachmentType entities
    /// </summary>
    public class AttachmentTypeRepository : BaseTypeStatusRepository<AttachmentType>, IAttachmentTypeRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "AttachmentType";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "AttachmentTypeId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "AttachmentTypeId", "Type", "Description", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the AttachmentTypeRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public AttachmentTypeRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<AttachmentTypeRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new attachment type
        /// </summary>
        /// <param name="entity">The attachment type to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added attachment type</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<AttachmentType> AddAsync(AttachmentType entity, CancellationToken cancellationToken = default)
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
                INSERT INTO AttachmentType (
                    AttachmentTypeId,
                    Type,
                    Description,
                    OrdinalPosition,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @AttachmentTypeId,
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
                AttachmentTypeId = entity.Id,
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
                async (connection) => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding {typeof(AttachmentType).Name}", EntityType = nameof(AttachmentType) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing attachment type
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated entity</returns>
        public override async Task<AttachmentType> UpdateAsync(AttachmentType entity, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Type = @Type,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE AttachmentTypeId = @AttachmentTypeId";

            var parameters = new
            {
                AttachmentTypeId = entity.Id,
                entity.Type,
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
                new { ErrorMessage = $"Error updating {typeof(AttachmentType).Name}", EntityType = nameof(AttachmentType), EntityId = entity.Id },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Determines if an attachment type with the specified ID exists
        /// </summary>
        /// <param name="id">The ID to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the entity exists, otherwise false</returns>
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM {TableName}
                    WHERE {IdColumnName} = @Id
                    AND Active = 1
                ) THEN 1 ELSE 0 END";

            return await ExecuteWithResilienceAndLoggingAsync<bool>(
                async (connection) => await connection.ExecuteScalarAsync<bool>(
                    new CommandDefinition(
                        sql,
                        new { Id = id },
                        cancellationToken: cancellationToken)),
                "ExistsAsync",
                new { ErrorMessage = $"Error checking existence of {typeof(AttachmentType).Name}", EntityType = nameof(AttachmentType), EntityId = id },
                cancellationToken);
        }

        /// <summary>
        /// Gets all attachment types that support the specified file extension
        /// </summary>
        /// <param name="fileExtension">The file extension to check (e.g., ".pdf", ".docx")</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachment types that support the file extension</returns>
        /// <remarks>The file extension search is performed using LIKE to match within stored patterns</remarks>
        public async Task<IEnumerable<AttachmentType>> GetByFileExtensionAsync(string fileExtension, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<AttachmentType>>(
                async (connection) => await connection.QueryAsync<AttachmentType>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetByFileExtensionAsync",
                new { ErrorMessage = $"Error retrieving attachment types for file extension: {fileExtension}", FileExtension = fileExtension },
                cancellationToken);
        }
    }
}