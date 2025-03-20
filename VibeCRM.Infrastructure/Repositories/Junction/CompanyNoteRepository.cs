using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Company_Note junction entities
    /// </summary>
    public class CompanyNoteRepository : BaseJunctionRepository<Company_Note, Guid, Guid>, ICompanyNoteRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Company_Note";

        /// <summary>
        /// Gets the name of the first ID column (CompanyId)
        /// </summary>
        protected override string FirstIdColumnName => "CompanyId";

        /// <summary>
        /// Gets the name of the second ID column (NoteId)
        /// </summary>
        protected override string SecondIdColumnName => "NoteId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "CompanyId", "NoteId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyNoteRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public CompanyNoteRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<CompanyNoteRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all company-note relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-note relationships for the specified company</returns>
        public async Task<IEnumerable<Company_Note>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Note>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyIdAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all company-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-note relationships for the specified note</returns>
        public async Task<IEnumerable<Company_Note>> GetByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @NoteId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Note>(
                    new CommandDefinition(
                        sql,
                        new { NoteId = noteId },
                        cancellationToken: cancellationToken)),
                "GetByNoteIdAsync",
                new { NoteId = noteId, EntityType = nameof(Company_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific company-note relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-note relationship if found, otherwise null</returns>
        public async Task<Company_Note?> GetByCompanyAndNoteIdAsync(Guid companyId, Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @NoteId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Company_Note>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId, NoteId = noteId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyAndNoteIdAsync",
                new { CompanyId = companyId, NoteId = noteId, EntityType = nameof(Company_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Adds a relationship between a company and a note
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created company-note relationship</returns>
        public async Task<Company_Note> AddRelationshipAsync(Guid companyId, Guid noteId, CancellationToken cancellationToken = default)
        {
            var entity = new Company_Note
            {
                CompanyId = companyId,
                NoteId = noteId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            var sql = $@"
                INSERT INTO {TableName} ({string.Join(", ", SelectColumns)})
                VALUES (@CompanyId, @NoteId, @Active, @ModifiedDate)";

            var parameters = new
            {
                CompanyId = companyId,
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
                new { CompanyId = companyId, NoteId = noteId, EntityType = nameof(Company_Note) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Removes a relationship between a company and a note
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid companyId, Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @NoteId
                AND Active = 1";

            var parameters = new
            {
                CompanyId = companyId,
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
                new { CompanyId = companyId, NoteId = noteId, EntityType = nameof(Company_Note) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Removes all relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @CompanyId
                AND Active = 1";

            var parameters = new
            {
                CompanyId = companyId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveAllForCompanyAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Note) },
                cancellationToken);

            return rowsAffected;
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

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveAllForNoteAsync",
                new { NoteId = noteId, EntityType = nameof(Company_Note) },
                cancellationToken);

            return rowsAffected;
        }

        /// <summary>
        /// Deletes all company-note relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @CompanyId
                AND Active = 1";

            var parameters = new
            {
                CompanyId = companyId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByCompanyIdAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Note) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all company-note relationships for a specific note
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
                new { NoteId = noteId, EntityType = nameof(Company_Note) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Gets company-note relationships by note type
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="noteTypeId">The note type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-note relationships with the specified note type</returns>
        public async Task<IEnumerable<Company_Note>> GetByNoteTypeAsync(Guid companyId, Guid noteTypeId, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT cn.CompanyId, cn.NoteId, cn.Active, cn.ModifiedDate
                FROM Company_Note cn
                JOIN Note n ON cn.NoteId = n.Id
                WHERE cn.CompanyId = @CompanyId
                  AND n.NoteTypeId = @NoteTypeId
                  AND cn.Active = 1
                  AND n.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Note>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId, NoteTypeId = noteTypeId },
                        cancellationToken: cancellationToken)),
                "GetByNoteTypeAsync",
                new { CompanyId = companyId, NoteTypeId = noteTypeId, EntityType = nameof(Company_Note) },
                cancellationToken);
        }
    }
}