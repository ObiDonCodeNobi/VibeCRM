using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Company_Attachment junction entities
    /// </summary>
    public class CompanyAttachmentRepository : BaseJunctionRepository<Company_Attachment, Guid, Guid>, ICompanyAttachmentRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Company_Attachment";

        /// <summary>
        /// Gets the name of the first ID column (CompanyId)
        /// </summary>
        protected override string FirstIdColumnName => "CompanyId";

        /// <summary>
        /// Gets the name of the second ID column (AttachmentId)
        /// </summary>
        protected override string SecondIdColumnName => "AttachmentId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "CompanyId", "AttachmentId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyAttachmentRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public CompanyAttachmentRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<CompanyAttachmentRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all company-attachment relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-attachment relationships for the specified company</returns>
        public async Task<IEnumerable<Company_Attachment>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            return await GetByFirstIdAsync(companyId, cancellationToken);
        }

        /// <summary>
        /// Gets all company-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-attachment relationships for the specified attachment</returns>
        public async Task<IEnumerable<Company_Attachment>> GetByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            return await GetBySecondIdAsync(attachmentId, cancellationToken);
        }

        /// <summary>
        /// Gets a specific company-attachment relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-attachment relationship if found, otherwise null</returns>
        public async Task<Company_Attachment?> GetByCompanyAndAttachmentIdAsync(Guid companyId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            return await GetByIdAsync(companyId, attachmentId, cancellationToken);
        }

        /// <summary>
        /// Adds a relationship between a company and an attachment
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created company-attachment relationship</returns>
        public async Task<Company_Attachment> AddRelationshipAsync(Guid companyId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var entity = new Company_Attachment
            {
                CompanyId = companyId,
                AttachmentId = attachmentId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            return await AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Removes a relationship between a company and an attachment
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid companyId, Guid attachmentId, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync(companyId, attachmentId, cancellationToken);
        }

        /// <summary>
        /// Removes all relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            if (await DeleteByFirstIdAsync(companyId, cancellationToken))
            {
                // Get approximate count by querying with Active = 0
                try
                {
                    string sql = $@"
                        SELECT COUNT(1)
                        FROM {TableName}
                        WHERE {FirstIdColumnName} = @FirstId
                          AND Active = 0";

                    return await ExecuteWithResilienceAndLoggingAsync(
                        async connection => await connection.ExecuteScalarAsync<int>(
                            new CommandDefinition(
                                sql,
                                new { FirstId = companyId },
                                cancellationToken: cancellationToken)),
                        "RemoveAllForCompanyAsync",
                        new { CompanyId = companyId, EntityType = nameof(Company_Attachment) },
                        cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get count of removed relationships for company {companyId}", ex);
                }
            }

            return 0;
        }

        /// <summary>
        /// Removes all relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForAttachmentAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            if (await DeleteBySecondIdAsync(attachmentId, cancellationToken))
            {
                // Get approximate count by querying with Active = 0
                try
                {
                    string sql = $@"
                        SELECT COUNT(1)
                        FROM {TableName}
                        WHERE {SecondIdColumnName} = @SecondId
                          AND Active = 0";

                    return await ExecuteWithResilienceAndLoggingAsync(
                        async connection => await connection.ExecuteScalarAsync<int>(
                            new CommandDefinition(
                                sql,
                                new { SecondId = attachmentId },
                                cancellationToken: cancellationToken)),
                        "RemoveAllForAttachmentAsync",
                        new { AttachmentId = attachmentId, EntityType = nameof(Company_Attachment) },
                        cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to get count of removed relationships for attachment {attachmentId}", ex);
                }
            }

            return 0;
        }

        /// <summary>
        /// Deletes all company-attachment relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            try
            {
                string sql = $@"
                    UPDATE {TableName}
                    SET Active = 0,
                        ModifiedBy = @ModifiedBy,
                        ModifiedDate = @ModifiedDate
                    WHERE {FirstIdColumnName} = @FirstId";

                string modifiedBy = "SYSTEM"; // Replace with actual user if available
                DateTime modifiedDate = DateTime.UtcNow;

                int rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                    async connection => await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            new { FirstId = companyId, ModifiedBy = modifiedBy, ModifiedDate = modifiedDate },
                            cancellationToken: cancellationToken)),
                    "DeleteByCompanyIdAsync",
                    new { CompanyId = companyId, EntityType = nameof(Company_Attachment) },
                    cancellationToken);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete relationships for company {companyId}", ex);
            }
        }

        /// <summary>
        /// Deletes all company-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            try
            {
                string sql = $@"
                    UPDATE {TableName}
                    SET Active = 0,
                        ModifiedBy = @ModifiedBy,
                        ModifiedDate = @ModifiedDate
                    WHERE {SecondIdColumnName} = @SecondId";

                string modifiedBy = "SYSTEM"; // Replace with actual user if available
                DateTime modifiedDate = DateTime.UtcNow;

                int rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                    async connection => await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            new { SecondId = attachmentId, ModifiedBy = modifiedBy, ModifiedDate = modifiedDate },
                            cancellationToken: cancellationToken)),
                    "DeleteByAttachmentIdAsync",
                    new { AttachmentId = attachmentId, EntityType = nameof(Company_Attachment) },
                    cancellationToken);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete relationships for attachment {attachmentId}", ex);
            }
        }

        /// <summary>
        /// Gets company-attachment relationships by attachment type
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="attachmentTypeId">The attachment type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-attachment relationships with the specified attachment type</returns>
        public async Task<IEnumerable<Company_Attachment>> GetByAttachmentTypeAsync(Guid companyId, Guid attachmentTypeId, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT ca.CompanyId, ca.AttachmentId, ca.Active, ca.ModifiedDate
                FROM Company_Attachment ca
                JOIN Attachment a ON ca.AttachmentId = a.Id
                WHERE ca.CompanyId = @CompanyId
                  AND a.AttachmentTypeId = @AttachmentTypeId
                  AND ca.Active = 1
                  AND a.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId, AttachmentTypeId = attachmentTypeId },
                        cancellationToken: cancellationToken)),
                "GetByAttachmentTypeAsync",
                new { CompanyId = companyId, AttachmentTypeId = attachmentTypeId, EntityType = nameof(Company_Attachment) },
                cancellationToken);
        }
    }
}