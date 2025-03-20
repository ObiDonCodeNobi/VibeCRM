using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Person_Note junction entities
    /// </summary>
    public class PersonNoteRepository : BaseJunctionRepository<Person_Note, Guid, Guid>, IPersonNoteRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Person_Note";

        /// <summary>
        /// Gets the name of the first ID column (PersonId)
        /// </summary>
        protected override string FirstIdColumnName => "PersonId";

        /// <summary>
        /// Gets the name of the second ID column (NoteId)
        /// </summary>
        protected override string SecondIdColumnName => "NoteId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "PersonId", "NoteId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonNoteRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public PersonNoteRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PersonNoteRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all person-note relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-note relationships for the specified person</returns>
        public async Task<IEnumerable<Person_Note>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Note>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetByPersonIdAsync",
                new { PersonId = personId, EntityType = nameof(Person_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all person-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-note relationships for the specified note</returns>
        public async Task<IEnumerable<Person_Note>> GetByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @NoteId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Note>(
                    new CommandDefinition(
                        sql,
                        new { NoteId = noteId },
                        cancellationToken: cancellationToken)),
                "GetByNoteIdAsync",
                new { NoteId = noteId, EntityType = nameof(Person_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific person-note relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person-note relationship if found, otherwise null</returns>
        public async Task<Person_Note?> GetByPersonAndNoteIdAsync(Guid personId, Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @NoteId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QuerySingleOrDefaultAsync<Person_Note>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, NoteId = noteId },
                        cancellationToken: cancellationToken)),
                "GetByPersonAndNoteIdAsync",
                new { PersonId = personId, NoteId = noteId, EntityType = nameof(Person_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Adds a relationship between a person and a note
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created person-note relationship</returns>
        public async Task<Person_Note> AddRelationshipAsync(Guid personId, Guid noteId, CancellationToken cancellationToken = default)
        {
            var entity = new Person_Note
            {
                PersonId = personId,
                NoteId = noteId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            var sql = $@"
                INSERT INTO {TableName} ({string.Join(", ", SelectColumns)})
                VALUES (@PersonId, @NoteId, @Active, @ModifiedDate)";

            var parameters = new
            {
                PersonId = personId,
                NoteId = noteId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddRelationshipAsync",
                new { PersonId = personId, NoteId = noteId, EntityType = nameof(Person_Note) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Removes a relationship between a person and a note
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid personId, Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @NoteId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                NoteId = noteId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveRelationshipAsync",
                new { PersonId = personId, NoteId = noteId, EntityType = nameof(Person_Note) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-note relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPersonIdAsync",
                new { PersonId = personId, EntityType = nameof(Person_Note) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @NoteId
                AND Active = 1";

            var parameters = new
            {
                NoteId = noteId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByNoteIdAsync",
                new { NoteId = noteId, EntityType = nameof(Person_Note) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Removes all relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                ModifiedDate = DateTime.UtcNow
            };

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveAllForPersonAsync",
                new { PersonId = personId, EntityType = nameof(Person_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Removes all relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForNoteAsync(Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @NoteId
                AND Active = 1";

            var parameters = new
            {
                NoteId = noteId,
                ModifiedDate = DateTime.UtcNow
            };

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveAllForNoteAsync",
                new { NoteId = noteId, EntityType = nameof(Person_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets person-note relationships by note type
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="noteTypeId">The note type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-note relationships with the specified note type</returns>
        public async Task<IEnumerable<Person_Note>> GetByNoteTypeAsync(Guid personId, Guid noteTypeId, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT pn.PersonId, pn.NoteId, pn.Active, pn.ModifiedDate
                FROM Person_Note pn
                JOIN Note n ON pn.NoteId = n.Id
                WHERE pn.PersonId = @PersonId
                  AND n.NoteTypeId = @NoteTypeId
                  AND pn.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Note>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, NoteTypeId = noteTypeId },
                        cancellationToken: cancellationToken)),
                "GetByNoteTypeAsync",
                new { PersonId = personId, NoteTypeId = noteTypeId, EntityType = nameof(Person_Note) },
                cancellationToken);
        }
    }
}