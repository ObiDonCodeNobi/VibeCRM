using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing Note entities in the database.
    /// Provides methods to create, read, update, and soft delete Note records,
    /// as well as specialized queries for filtering notes by various criteria.
    /// </summary>
    public class NoteRepository : BaseRepository<Note, Guid>, INoteRepository
    {
        /// <summary>
        /// Gets the database table name for Note entities
        /// </summary>
        protected override string TableName => "Note";

        /// <summary>
        /// Gets the primary key column name for Note entities
        /// </summary>
        protected override string IdColumnName => "NoteId";

        /// <summary>
        /// Gets the columns to select when retrieving Note entities
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "NoteId",
            "NoteTypeId",
            "Subject",
            "NoteText",
            "CreatedBy",
            "CreatedDate",
            "ModifiedBy",
            "ModifiedDate",
            "Active"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public NoteRepository(ISQLConnectionFactory connectionFactory, ILogger<NoteRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new note to the repository
        /// </summary>
        /// <param name="entity">The note entity to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added note entity with its ID</returns>
        /// <exception cref="ArgumentNullException">Thrown when the entity is null</exception>
        public override async Task<Note> AddAsync(Note entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            const string sql = @"
                INSERT INTO Note (
                    NoteId, NoteTypeId, Subject, NoteText,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @NoteId, @NoteTypeId, @Subject, @NoteText,
                    @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active
                );";

            var parameters = new
            {
                entity.NoteId,
                entity.NoteTypeId,
                entity.Subject,
                entity.NoteText,
                entity.CreatedBy,
                entity.CreatedDate,
                entity.ModifiedBy,
                entity.ModifiedDate,
                entity.Active
            };

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            parameters,
                            cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding Note with ID {entity.NoteId}", NoteId = entity.NoteId, EntityType = nameof(Note) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Deletes a note by its unique identifier (soft delete)
        /// </summary>
        /// <param name="id">The unique identifier of the note to delete</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the note was successfully deleted, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Note ID cannot be empty", nameof(id));

            // Use the BaseRepository soft delete pattern
            const string sql = @"
                UPDATE Note
                SET Active = 0,
                    ModifiedDate = @ModifiedDate
                WHERE NoteId = @NoteId";

            var affectedRows = await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            new { NoteId = id, ModifiedDate = DateTime.UtcNow },
                            cancellationToken: cancellationToken)),
                "DeleteAsync",
                new { ErrorMessage = $"Error deleting Note with ID {id}", NoteId = id, EntityType = nameof(Note) },
                cancellationToken);
            return affectedRows > 0;
        }

        /// <summary>
        /// Checks if a note with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a note with the specified ID exists, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Note ID cannot be empty", nameof(id));

            const string sql = "SELECT COUNT(1) FROM Note WHERE NoteId = @NoteId";

            var count = await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            new { NoteId = id },
                            cancellationToken: cancellationToken)),
                "ExistsAsync",
                new { ErrorMessage = $"Error checking if Note with ID {id} exists", NoteId = id, EntityType = nameof(Note) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Gets all notes from the repository
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of all notes in the repository</returns>
        public override async Task<IEnumerable<Note>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Note>>(
                async (connection) =>
                    await connection.QueryAsync<Note>(
                        new CommandDefinition(
                            sql,
                            cancellationToken: cancellationToken)),
                "GetAllAsync",
                new { ErrorMessage = "Error retrieving all Notes", EntityType = nameof(Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a note by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the note</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The note if found, otherwise null</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<Note?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Note ID cannot be empty", nameof(id));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {IdColumnName} = @Id AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<Note?>(
                async (connection) =>
                    await connection.QueryFirstOrDefaultAsync<Note>(
                        new CommandDefinition(
                            sql,
                            new { Id = id },
                            cancellationToken: cancellationToken)),
                "GetByIdAsync",
                new { ErrorMessage = $"Error retrieving Note with ID {id}", NoteId = id, EntityType = nameof(Note) },
                cancellationToken);
        }

        /// <summary>
        /// Updates an existing note in the repository
        /// </summary>
        /// <param name="entity">The note to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated note</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when entity.NoteId is empty</exception>
        public override async Task<Note> UpdateAsync(Note entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.NoteId == Guid.Empty) throw new ArgumentException("The Note ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE Note
                SET NoteTypeId = @NoteTypeId,
                    Subject = @Subject,
                    NoteText = @NoteText,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate,
                    Active = @Active
                WHERE NoteId = @NoteId";

            var parameters = new
            {
                entity.NoteId,
                entity.NoteTypeId,
                entity.Subject,
                entity.NoteText,
                entity.ModifiedBy,
                entity.ModifiedDate,
                entity.Active
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            parameters,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating Note with ID {entity.NoteId}", NoteId = entity.NoteId, EntityType = nameof(Note) },
                cancellationToken);

            if (rowsAffected == 0)
            {
                _logger.LogWarning("No Note was updated for ID {NoteId}", entity.NoteId);
            }

            return entity;
        }

        /// <summary>
        /// Gets notes by their type
        /// </summary>
        /// <param name="noteTypeId">The note type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of notes with the specified type</returns>
        /// <exception cref="ArgumentException">Thrown when noteTypeId is empty</exception>
        public async Task<IEnumerable<Note>> GetByNoteTypeAsync(Guid noteTypeId, CancellationToken cancellationToken = default)
        {
            if (noteTypeId == Guid.Empty) throw new ArgumentException("The NoteType ID cannot be empty", nameof(noteTypeId));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE NoteTypeId = @NoteTypeId AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Note>>(
                async (connection) =>
                    await connection.QueryAsync<Note>(
                        new CommandDefinition(
                            sql,
                            new { NoteTypeId = noteTypeId },
                            cancellationToken: cancellationToken)),
                "GetByNoteTypeAsync",
                new { ErrorMessage = $"Error retrieving Notes by type {noteTypeId}", NoteTypeId = noteTypeId, EntityType = nameof(Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets notes for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of notes associated with the specified company</returns>
        /// <exception cref="ArgumentException">Thrown when companyId is empty</exception>
        public async Task<IEnumerable<Note>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            if (companyId == Guid.Empty) throw new ArgumentException("The Company ID cannot be empty", nameof(companyId));

            const string sql = @"
                SELECT n.NoteId, n.NoteTypeId, n.Subject, n.NoteText,
                       n.CreatedBy, n.CreatedDate, n.ModifiedBy, n.ModifiedDate, n.Active
                FROM Note n
                JOIN Company_Note cn ON n.NoteId = cn.NoteId
                WHERE cn.CompanyId = @CompanyId AND n.Active = 1 AND cn.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Note>>(
                async (connection) =>
                    await connection.QueryAsync<Note>(
                        new CommandDefinition(
                            sql,
                            new { CompanyId = companyId },
                            cancellationToken: cancellationToken)),
                "GetByCompanyAsync",
                new { ErrorMessage = $"Error retrieving Notes for company {companyId}", CompanyId = companyId, EntityType = nameof(Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets notes for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of notes associated with the specified person</returns>
        /// <exception cref="ArgumentException">Thrown when personId is empty</exception>
        public async Task<IEnumerable<Note>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            if (personId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(personId));

            const string sql = @"
                SELECT n.NoteId, n.NoteTypeId, n.Subject, n.NoteText,
                       n.CreatedBy, n.CreatedDate, n.ModifiedBy, n.ModifiedDate, n.Active
                FROM Note n
                JOIN Person_Note pn ON n.NoteId = pn.NoteId
                WHERE pn.PersonId = @PersonId AND n.Active = 1 AND pn.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Note>>(
                async (connection) =>
                    await connection.QueryAsync<Note>(
                        new CommandDefinition(
                            sql,
                            new { PersonId = personId },
                            cancellationToken: cancellationToken)),
                "GetByPersonAsync",
                new { ErrorMessage = $"Error retrieving Notes for person {personId}", PersonId = personId, EntityType = nameof(Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets notes created by a specific user
        /// </summary>
        /// <param name="createdById">The identifier of the user who created the notes</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of notes created by the specified user</returns>
        /// <exception cref="ArgumentException">Thrown when createdById is empty</exception>
        public async Task<IEnumerable<Note>> GetByCreatedByAsync(Guid createdById, CancellationToken cancellationToken = default)
        {
            if (createdById == Guid.Empty) throw new ArgumentException("The Creator ID cannot be empty", nameof(createdById));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE CreatedBy = @CreatedBy AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Note>>(
                async (connection) =>
                    await connection.QueryAsync<Note, NoteType, Note>(
                        new CommandDefinition(
                            sql,
                            new { CreatedBy = createdById },
                            cancellationToken: cancellationToken),
                        (note, noteType) =>
                        {
                            note.NoteType = noteType;
                            return note;
                        },
                        splitOn: "NoteTypeId"),
                "GetByCreatedByAsync",
                new { ErrorMessage = $"Error retrieving Notes created by {createdById}", CreatedBy = createdById, EntityType = nameof(Note) },
                cancellationToken);
        }

        /// <summary>
        /// Searches notes by content
        /// </summary>
        /// <param name="searchText">The text to search for in the note content</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of notes containing the specified text</returns>
        /// <exception cref="ArgumentException">Thrown when searchText is null or empty</exception>
        public async Task<IEnumerable<Note>> SearchByContentAsync(string searchText, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                throw new ArgumentException("Search text cannot be null or empty", nameof(searchText));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE (NoteText LIKE '%' + @SearchText + '%' OR Subject LIKE '%' + @SearchText + '%')
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Note>>(
                async (connection) =>
                    await connection.QueryAsync<Note, NoteType, Note>(
                        new CommandDefinition(
                            sql,
                            new { SearchText = $"%{searchText}%" },
                            cancellationToken: cancellationToken),
                        (note, noteType) =>
                        {
                            note.NoteType = noteType;
                            return note;
                        },
                        splitOn: "NoteTypeId"),
                "SearchByContentAsync",
                new { ErrorMessage = $"Error searching Notes with content '{searchText}'", SearchText = searchText, EntityType = nameof(Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets notes by creation date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of notes created within the specified date range</returns>
        /// <exception cref="ArgumentException">Thrown when startDate is greater than endDate</exception>
        public async Task<IEnumerable<Note>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            if (startDate > endDate)
                throw new ArgumentException("Start date cannot be greater than end date", nameof(startDate));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE CreatedDate >= @StartDate
                AND CreatedDate <= @EndDate
                AND Active = 1";

            var parameters = new
            {
                StartDate = startDate,
                EndDate = endDate
            };

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Note>>(
                async (connection) =>
                    await connection.QueryAsync<Note>(
                        new CommandDefinition(
                            sql,
                            parameters,
                            cancellationToken: cancellationToken)),
                "GetByCreatedDateRangeAsync",
                new { ErrorMessage = $"Error retrieving Notes created between {startDate} and {endDate}", StartDate = startDate, EndDate = endDate, EntityType = nameof(Note) },
                cancellationToken);
        }
    }
}