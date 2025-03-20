using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing Attachment entities
    /// </summary>
    public class AttachmentRepository : BaseRepository<Attachment, Guid>, IAttachmentRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Attachment";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "AttachmentId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "AttachmentId", "AttachmentTypeId", "Subject", "Path",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public AttachmentRepository(ISQLConnectionFactory connectionFactory, ILogger<AttachmentRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new attachment to the repository
        /// </summary>
        /// <param name="entity">The attachment entity to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added attachment with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when AttachmentId is empty</exception>
        public override async Task<Attachment> AddAsync(Attachment entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AttachmentId == Guid.Empty) throw new ArgumentException("The Attachment ID cannot be empty", nameof(entity));
            if (string.IsNullOrWhiteSpace(entity.Subject)) throw new ArgumentException("Attachment Subject cannot be null or empty", nameof(entity));

            const string sql = @"
                INSERT INTO Attachment (
                    AttachmentId, AttachmentTypeId, Subject, Path,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @AttachmentId, @AttachmentTypeId, @Subject, @Path,
                    @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active
                );";

            await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding Attachment with ID {entity.AttachmentId}", AttachmentId = entity.AttachmentId, EntityType = nameof(Attachment) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing attachment in the repository
        /// </summary>
        /// <param name="entity">The attachment to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated attachment</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when AttachmentId is empty</exception>
        public override async Task<Attachment> UpdateAsync(Attachment entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AttachmentId == Guid.Empty) throw new ArgumentException("The Attachment ID cannot be empty", nameof(entity));
            if (string.IsNullOrWhiteSpace(entity.Subject)) throw new ArgumentException("Attachment Subject cannot be null or empty", nameof(entity));

            const string sql = @"
                UPDATE Attachment
                SET
                    AttachmentTypeId = @AttachmentTypeId,
                    Subject = @Subject,
                    Path = @Path,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate,
                    Active = @Active
                WHERE AttachmentId = @AttachmentId";

            var affectedRows = await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating Attachment with ID {entity.AttachmentId}", AttachmentId = entity.AttachmentId, EntityType = nameof(Attachment) },
                cancellationToken);

            if (affectedRows == 0)
            {
                _logger.LogWarning("No Attachment was updated for ID {AttachmentId}", entity.AttachmentId);
            }

            return entity;
        }

        /// <summary>
        /// Deletes an attachment by its unique identifier (soft delete)
        /// </summary>
        /// <param name="id">The unique identifier of the attachment to delete</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the attachment was successfully deleted, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Attachment ID cannot be empty", nameof(id));

            // Use the BaseRepository soft delete pattern
            var success = await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            $"UPDATE {TableName} SET Active = 0 WHERE {IdColumnName} = @Id",
                            new { Id = id },
                            cancellationToken: cancellationToken)),
                "DeleteAsync",
                new { ErrorMessage = $"Error soft-deleting {typeof(Attachment).Name}", EntityType = nameof(Attachment), EntityId = id },
                cancellationToken);

            return success > 0;
        }

        /// <summary>
        /// Checks if an attachment with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if an attachment with the specified ID exists, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Attachment ID cannot be empty", nameof(id));

            const string sql = "SELECT COUNT(1) FROM Attachment WHERE AttachmentId = @AttachmentId AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            new { AttachmentId = id },
                            cancellationToken: cancellationToken)),
                "ExistsAsync",
                new { ErrorMessage = $"Error checking existence of {typeof(Attachment).Name}", EntityType = nameof(Attachment), EntityId = id },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Gets all attachment entities from the repository
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of all attachment entities in the repository</returns>
        public override async Task<IEnumerable<Attachment>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<Attachment>(
                        new CommandDefinition(
                            sql,
                            cancellationToken: cancellationToken)),
                "GetAllAsync",
                new { ErrorMessage = "Error retrieving all Attachments", EntityType = nameof(Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets an attachment by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the attachment</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The attachment if found, otherwise null</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<Attachment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Attachment ID cannot be empty", nameof(id));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE AttachmentId = @AttachmentId AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryFirstOrDefaultAsync<Attachment>(
                        new CommandDefinition(
                            sql,
                            new { AttachmentId = id },
                            cancellationToken: cancellationToken)),
                "GetByIdAsync",
                new { ErrorMessage = $"Error retrieving Attachment with ID {id}", AttachmentId = id, EntityType = nameof(Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets attachments by their type
        /// </summary>
        /// <param name="attachmentTypeId">The attachment type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachments with the specified type</returns>
        /// <exception cref="ArgumentException">Thrown when attachmentTypeId is empty</exception>
        public async Task<IEnumerable<Attachment>> GetByAttachmentTypeAsync(Guid attachmentTypeId, CancellationToken cancellationToken = default)
        {
            if (attachmentTypeId == Guid.Empty) throw new ArgumentException("The Attachment Type ID cannot be empty", nameof(attachmentTypeId));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE AttachmentTypeId = @AttachmentTypeId AND Active = 1
                ORDER BY CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<Attachment>(
                        new CommandDefinition(
                            sql,
                            new { AttachmentTypeId = attachmentTypeId },
                            cancellationToken: cancellationToken)),
                "GetByAttachmentTypeAsync",
                new { ErrorMessage = $"Error retrieving Attachments with Type ID {attachmentTypeId}", AttachmentTypeId = attachmentTypeId, EntityType = nameof(Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets attachments associated with a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachments associated with the specified activity</returns>
        /// <exception cref="ArgumentException">Thrown when activityId is empty</exception>
        public async Task<IEnumerable<Attachment>> GetByActivityAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            if (activityId == Guid.Empty) throw new ArgumentException("The Activity ID cannot be empty", nameof(activityId));

            const string sql = @"
                SELECT a.AttachmentId, a.AttachmentTypeId, a.Subject, a.Path,
                       a.CreatedBy, a.CreatedDate, a.ModifiedBy, a.ModifiedDate, a.Active
                FROM Attachment a
                INNER JOIN Activity_Attachment aa ON a.AttachmentId = aa.AttachmentId
                WHERE aa.ActivityId = @ActivityId AND a.Active = 1
                ORDER BY a.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<Attachment>(
                        new CommandDefinition(
                            sql,
                            new { ActivityId = activityId },
                            cancellationToken: cancellationToken)),
                "GetByActivityAsync",
                new { ErrorMessage = $"Error retrieving Attachments associated with Activity ID {activityId}", ActivityId = activityId, EntityType = nameof(Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets attachments associated with a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachments associated with the specified company</returns>
        /// <exception cref="ArgumentException">Thrown when companyId is empty</exception>
        public async Task<IEnumerable<Attachment>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            if (companyId == Guid.Empty) throw new ArgumentException("The Company ID cannot be empty", nameof(companyId));

            const string sql = @"
                SELECT a.AttachmentId, a.AttachmentTypeId, a.Subject, a.Path,
                       a.CreatedBy, a.CreatedDate, a.ModifiedBy, a.ModifiedDate, a.Active
                FROM Attachment a
                INNER JOIN Company_Attachment ca ON a.AttachmentId = ca.AttachmentId
                WHERE ca.CompanyId = @CompanyId AND a.Active = 1
                ORDER BY a.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Attachment>>(
                async connection =>
                    await connection.QueryAsync<Attachment>(
                        new CommandDefinition(
                            sql,
                            new { CompanyId = companyId },
                            cancellationToken: cancellationToken)),
                "GetByCompanyIdAsync",
                new { CompanyId = companyId, EntityType = nameof(Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets attachments associated with a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachments associated with the specified note</returns>
        /// <exception cref="ArgumentException">Thrown when noteId is empty</exception>
        public async Task<IEnumerable<Attachment>> GetByNoteAsync(Guid noteId, CancellationToken cancellationToken = default)
        {
            if (noteId == Guid.Empty) throw new ArgumentException("The Note ID cannot be empty", nameof(noteId));

            const string sql = @"
                SELECT a.AttachmentId, a.AttachmentTypeId, a.Subject, a.Path,
                       a.CreatedBy, a.CreatedDate, a.ModifiedBy, a.ModifiedDate, a.Active
                FROM Attachment a
                INNER JOIN Note_Attachment na ON a.AttachmentId = na.AttachmentId
                WHERE na.NoteId = @NoteId AND a.Active = 1
                ORDER BY a.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<Attachment>(
                        new CommandDefinition(
                            sql,
                            new { NoteId = noteId },
                            cancellationToken: cancellationToken)),
                "GetByNoteAsync",
                new { ErrorMessage = $"Error retrieving Attachments associated with Note ID {noteId}", NoteId = noteId, EntityType = nameof(Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets attachments associated with a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachments associated with the specified person</returns>
        /// <exception cref="ArgumentException">Thrown when personId is empty</exception>
        public async Task<IEnumerable<Attachment>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            if (personId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(personId));

            const string sql = @"
                SELECT a.AttachmentId, a.AttachmentTypeId, a.Subject, a.Path,
                       a.CreatedBy, a.CreatedDate, a.ModifiedBy, a.ModifiedDate, a.Active
                FROM Attachment a
                INNER JOIN Person_Attachment pa ON a.AttachmentId = pa.AttachmentId
                WHERE pa.PersonId = @PersonId AND a.Active = 1
                ORDER BY a.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Attachment>>(
                async connection =>
                    await connection.QueryAsync<Attachment>(
                        new CommandDefinition(
                            sql,
                            new { PersonId = personId },
                            cancellationToken: cancellationToken)),
                "GetByPersonIdAsync",
                new { PersonId = personId, EntityType = nameof(Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets attachments by subject search
        /// </summary>
        /// <param name="searchTerm">The search term to filter by</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachments whose subject contains the search term</returns>
        /// <exception cref="ArgumentException">Thrown when searchTerm is null or empty</exception>
        public async Task<IEnumerable<Attachment>> SearchBySubjectAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Search term cannot be null or whitespace", nameof(searchTerm));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Subject LIKE @SearchPattern AND Active = 1
                ORDER BY CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<Attachment>(
                        new CommandDefinition(
                            sql,
                            new { SearchPattern = $"%{searchTerm}%" },
                            cancellationToken: cancellationToken)),
                "SearchBySubjectAsync",
                new { ErrorMessage = $"Error searching Attachments with term '{searchTerm}'", SearchTerm = searchTerm, EntityType = nameof(Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets attachments by file name
        /// </summary>
        /// <param name="fileName">The file name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachments with the specified file name</returns>
        /// <exception cref="ArgumentException">Thrown when fileName is null or empty</exception>
        public async Task<IEnumerable<Attachment>> GetByFileNameAsync(string fileName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be null or whitespace", nameof(fileName));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Path LIKE @SearchPattern AND Active = 1
                ORDER BY CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Attachment>>(
                async connection =>
                    await connection.QueryAsync<Attachment>(
                        new CommandDefinition(
                            sql,
                            new { SearchPattern = $"%{fileName}%" },
                            cancellationToken: cancellationToken)),
                "GetByFileNameAsync",
                new { FileName = fileName, EntityType = nameof(Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets attachments by file extension
        /// </summary>
        /// <param name="fileExtension">The file extension to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachments with the specified file extension</returns>
        /// <exception cref="ArgumentException">Thrown when fileExtension is null or empty</exception>
        public async Task<IEnumerable<Attachment>> GetByFileExtensionAsync(string fileExtension, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fileExtension))
                throw new ArgumentException("File extension cannot be null or whitespace", nameof(fileExtension));

            // If the extension doesn't start with a dot, add it
            if (!fileExtension.StartsWith("."))
                fileExtension = "." + fileExtension;

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Path LIKE @SearchPattern AND Active = 1
                ORDER BY CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<Attachment>(
                        new CommandDefinition(
                            sql,
                            new { SearchPattern = $"%{fileExtension}" },
                            cancellationToken: cancellationToken)),
                "GetByFileExtensionAsync",
                new { ErrorMessage = $"Error retrieving Attachments with file extension '{fileExtension}'", FileExtension = fileExtension, EntityType = nameof(Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets attachments created within a date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachments created within the specified date range</returns>
        public async Task<IEnumerable<Attachment>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            if (endDate < startDate)
                throw new ArgumentException("End date must be greater than or equal to start date", nameof(endDate));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE CreatedDate >= @StartDate AND CreatedDate <= @EndDate AND Active = 1
                ORDER BY CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<Attachment>(
                        new CommandDefinition(
                            sql,
                            new { StartDate = startDate, EndDate = endDate },
                            cancellationToken: cancellationToken)),
                "GetByCreatedDateRangeAsync",
                new { ErrorMessage = $"Error retrieving Attachments with creation date between {startDate:yyyy-MM-dd} and {endDate:yyyy-MM-dd}", StartDate = startDate, EndDate = endDate, EntityType = nameof(Attachment) },
                cancellationToken);
        }
    }
}