using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing NoteType entities
    /// </summary>
    public class NoteTypeRepository : BaseTypeStatusRepository<NoteType>, INoteTypeRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "NoteType";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "NoteTypeId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "NoteTypeId", "Type", "Description", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the NoteTypeRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public NoteTypeRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<NoteTypeRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets note types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of note types ordered by their ordinal position</returns>
        public async Task<IEnumerable<NoteType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<NoteType>>(
                async (connection) => await connection.QueryAsync<NoteType>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetByOrdinalPositionAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets note types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of note types with the specified type name</returns>
        public async Task<IEnumerable<NoteType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Type = @Type
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<NoteType>>(
                async (connection) => await connection.QueryAsync<NoteType>(
                    new CommandDefinition(
                        sql,
                        new { Type = type },
                        cancellationToken: cancellationToken)),
                "GetByTypeAsync",
                new { Type = type, TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets the default note type (typically the one with the lowest ordinal position)
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default note type or null if not found</returns>
        public async Task<NoteType?> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<NoteType?>(
                async (connection) => await connection.QueryFirstOrDefaultAsync<NoteType>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetDefaultAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new note type
        /// </summary>
        /// <param name="entity">The note type to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added note type</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<NoteType> AddAsync(NoteType entity, CancellationToken cancellationToken = default)
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
                INSERT INTO NoteType (
                    NoteTypeId,
                    Type,
                    Description,
                    OrdinalPosition,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @NoteTypeId,
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
                NoteTypeId = entity.Id,
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
                new { ErrorMessage = $"Error adding {typeof(NoteType).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(NoteType) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing note type
        /// </summary>
        /// <param name="entity">The note type to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated note type</returns>
        public override async Task<NoteType> UpdateAsync(NoteType entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE NoteType
                SET Type = @Type,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE NoteTypeId = @NoteTypeId";

            var parameters = new
            {
                NoteTypeId = entity.Id,
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
                new { ErrorMessage = $"Error updating {typeof(NoteType).Name} with ID {entity.Id}", EntityId = entity.Id.ToString(), EntityType = nameof(NoteType) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Checks if a note type with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a note type with the specified ID exists, otherwise false</returns>
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
                new { ErrorMessage = $"Error checking existence of {typeof(NoteType).Name} with ID {id}", EntityId = id.ToString(), EntityType = nameof(NoteType) },
                cancellationToken);

            return count == 1;
        }

        /// <summary>
        /// Gets note types by color code
        /// </summary>
        /// <param name="colorCode">The color code to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of note types with the specified color code</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<IEnumerable<NoteType>> GetByColorCodeAsync(string colorCode, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(colorCode))
                throw new ArgumentException("Color code cannot be null or empty", nameof(colorCode));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE ColorCode = @ColorCode
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<NoteType>>(
                async connection => await connection.QueryAsync<NoteType>(
                    new CommandDefinition(
                        sql,
                        new { ColorCode = colorCode },
                        cancellationToken: cancellationToken)),
                "GetByColorCodeAsync",
                new { ErrorMessage = $"Error retrieving {typeof(NoteType).Name} with color code {colorCode}", EntityType = nameof(NoteType) },
                cancellationToken);
        }

        /// <summary>
        /// Gets note types by icon name
        /// </summary>
        /// <param name="iconName">The icon name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of note types with the specified icon name</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<IEnumerable<NoteType>> GetByIconNameAsync(string iconName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(iconName))
                throw new ArgumentException("Icon name cannot be null or empty", nameof(iconName));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE IconName = @IconName
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<NoteType>>(
                async connection => await connection.QueryAsync<NoteType>(
                    new CommandDefinition(
                        sql,
                        new { IconName = iconName },
                        cancellationToken: cancellationToken)),
                "GetByIconNameAsync",
                new { ErrorMessage = $"Error retrieving {typeof(NoteType).Name} with icon name {iconName}", EntityType = nameof(NoteType) },
                cancellationToken);
        }
    }
}