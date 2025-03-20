using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Company_EmailAddress junction entities
    /// </summary>
    public class CompanyEmailAddressRepository : BaseJunctionRepository<Company_EmailAddress, Guid, Guid>, ICompanyEmailAddressRepository
    {
        private readonly ILogger<CompanyEmailAddressRepository> _logger;

        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Company_EmailAddress";

        /// <summary>
        /// Gets the name of the first ID column (CompanyId)
        /// </summary>
        protected override string FirstIdColumnName => "CompanyId";

        /// <summary>
        /// Gets the name of the second ID column (EmailAddressId)
        /// </summary>
        protected override string SecondIdColumnName => "EmailAddressId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "CompanyId", "EmailAddressId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyEmailAddressRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public CompanyEmailAddressRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<CompanyEmailAddressRepository> logger)
            : base(connectionFactory, logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets all company-email address relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-email address relationships for the specified company</returns>
        public async Task<IEnumerable<Company_EmailAddress>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            if (companyId == Guid.Empty) throw new ArgumentException("Company ID cannot be empty", nameof(companyId));

            var query = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE CompanyId = @CompanyId AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company_EmailAddress>>(
                async connection => await connection.QueryAsync<Company_EmailAddress>(
                    new CommandDefinition(
                        query,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyIdAsync",
                new { ErrorMessage = $"Error retrieving Company_EmailAddress records for company ID {companyId}", CompanyId = companyId },
                cancellationToken);
        }

        /// <summary>
        /// Gets all company-email address relationships for a specific email address
        /// </summary>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-email address relationships for the specified email address</returns>
        public async Task<IEnumerable<Company_EmailAddress>> GetByEmailAddressIdAsync(Guid emailAddressId, CancellationToken cancellationToken = default)
        {
            if (emailAddressId == Guid.Empty) throw new ArgumentException("Email Address ID cannot be empty", nameof(emailAddressId));

            var query = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE EmailAddressId = @EmailAddressId AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company_EmailAddress>>(
                async connection => await connection.QueryAsync<Company_EmailAddress>(
                    new CommandDefinition(
                        query,
                        new { EmailAddressId = emailAddressId },
                        cancellationToken: cancellationToken)),
                "GetByEmailAddressIdAsync",
                new { ErrorMessage = $"Error retrieving Company_EmailAddress records for email address ID {emailAddressId}", EmailAddressId = emailAddressId },
                cancellationToken);
        }

        /// <summary>
        /// Gets the company-email address relationship for a specific company and email address
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-email address relationship if it exists, otherwise null</returns>
        public async Task<Company_EmailAddress?> GetByCompanyAndEmailAddressIdAsync(Guid companyId, Guid emailAddressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @EmailAddressId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<Company_EmailAddress?>(
                async connection => await connection.QueryFirstOrDefaultAsync<Company_EmailAddress>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId, EmailAddressId = emailAddressId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyAndEmailAddressIdAsync",
                new { CompanyId = companyId, EmailAddressId = emailAddressId, EntityType = nameof(Company_EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a company and an email address
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByCompanyAndEmailAddressAsync(Guid companyId, Guid emailAddressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @EmailAddressId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId, EmailAddressId = emailAddressId },
                        cancellationToken: cancellationToken)),
                "ExistsByCompanyAndEmailAddressAsync",
                new { CompanyId = companyId, EmailAddressId = emailAddressId, EntityType = nameof(Company_EmailAddress) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Gets the primary email address relationship for a specific company, if one exists
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The primary company-email address relationship for the specified company, or null if none exists</returns>
        public async Task<Company_EmailAddress?> GetPrimaryEmailForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<Company_EmailAddress?>(
                async connection => await connection.QueryFirstOrDefaultAsync<Company_EmailAddress>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetPrimaryEmailForCompanyAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Gets the primary email address for a company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The primary company-email address relationship if found, otherwise null</returns>
        public async Task<Company_EmailAddress?> GetPrimaryEmailAddressForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            return await GetPrimaryEmailForCompanyAsync(companyId, cancellationToken);
        }

        /// <summary>
        /// Adds a relationship between a company and an email address
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created company-email address relationship</returns>
        public async Task<Company_EmailAddress> AddRelationshipAsync(
            Guid companyId,
            Guid emailAddressId,
            CancellationToken cancellationToken = default)
        {
            var entity = new Company_EmailAddress
            {
                CompanyId = companyId,
                EmailAddressId = emailAddressId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            var sql = $@"
                INSERT INTO {TableName} ({string.Join(", ", SelectColumns)})
                VALUES (@CompanyId, @EmailAddressId, @Active, @ModifiedDate)";

            var parameters = new
            {
                CompanyId = companyId,
                EmailAddressId = emailAddressId,
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
                new { CompanyId = companyId, EmailAddressId = emailAddressId, EntityType = nameof(Company_EmailAddress) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Removes a relationship between a company and an email address
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid companyId, Guid emailAddressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @EmailAddressId
                AND Active = 1";

            var parameters = new
            {
                CompanyId = companyId,
                EmailAddressId = emailAddressId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveRelationshipAsync",
                new { CompanyId = companyId, EmailAddressId = emailAddressId, EntityType = nameof(Company_EmailAddress) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Soft deletes the relationship between a company and an email address
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByCompanyAndEmailAddressAsync(Guid companyId, Guid emailAddressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @EmailAddressId
                AND Active = 1";

            var parameters = new
            {
                CompanyId = companyId,
                EmailAddressId = emailAddressId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByCompanyAndEmailAddressAsync",
                new { CompanyId = companyId, EmailAddressId = emailAddressId, EntityType = nameof(Company_EmailAddress) },
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

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveAllForCompanyAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Soft deletes all relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if successful, otherwise false</returns>
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
                new { CompanyId = companyId, EntityType = nameof(Company_EmailAddress) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Removes all relationships for a specific email address
        /// </summary>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForEmailAddressAsync(Guid emailAddressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @EmailAddressId
                AND Active = 1";

            var parameters = new
            {
                EmailAddressId = emailAddressId,
                ModifiedDate = DateTime.UtcNow
            };

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveAllForEmailAddressAsync",
                new { EmailAddressId = emailAddressId, EntityType = nameof(Company_EmailAddress) },
                cancellationToken);
        }

        /// <summary>
        /// Soft deletes all relationships for a specific email address
        /// </summary>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if successful, otherwise false</returns>
        public async Task<bool> DeleteByEmailAddressIdAsync(Guid emailAddressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @EmailAddressId
                AND Active = 1";

            var parameters = new
            {
                EmailAddressId = emailAddressId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByEmailAddressIdAsync",
                new { EmailAddressId = emailAddressId, EntityType = nameof(Company_EmailAddress) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Sets a company email address as primary
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="emailAddressId">The email address identifier to set as primary</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated company-email address relationship if found, otherwise null</returns>
        /// <exception cref="ArgumentException">Thrown when companyId or emailAddressId is empty</exception>
        public async Task<Company_EmailAddress?> SetPrimaryEmailAddressAsync(Guid companyId, Guid emailAddressId, CancellationToken cancellationToken = default)
        {
            if (companyId == Guid.Empty) throw new ArgumentException("Company ID cannot be empty", nameof(companyId));
            if (emailAddressId == Guid.Empty) throw new ArgumentException("Email Address ID cannot be empty", nameof(emailAddressId));

            // Check if relationship exists
            var relationship = await GetSingleAsync(companyId, emailAddressId, cancellationToken);
            if (relationship == null)
            {
                _logger.LogWarning("Attempted to set primary email for non-existent Company-EmailAddress relationship. Company ID: {CompanyId}, EmailAddress ID: {EmailAddressId}",
                    companyId, emailAddressId);
                return null;
            }

            // Log a warning since Company_EmailAddress entity doesn't actually have an IsPrimary property
            _logger.LogWarning("SetPrimaryEmailAddressAsync called but Company_EmailAddress entity doesn't have an IsPrimary property. " +
                "Company ID: {CompanyId}, EmailAddress ID: {EmailAddressId}",
                companyId, emailAddressId);

            // Return the existing relationship since we can't update a property that doesn't exist
            return relationship;
        }

        /// <summary>
        /// Gets company-email address relationships by email address type
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="emailAddressTypeId">The email address type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>An empty collection since the Company_EmailAddress entity doesn't have an email address type concept</returns>
        /// <exception cref="ArgumentException">Thrown when companyId or emailAddressTypeId is empty</exception>
        public async Task<IEnumerable<Company_EmailAddress>> GetByEmailAddressTypeAsync(Guid companyId, Guid emailAddressTypeId, CancellationToken cancellationToken = default)
        {
            if (companyId == Guid.Empty) throw new ArgumentException("Company ID cannot be empty", nameof(companyId));
            if (emailAddressTypeId == Guid.Empty) throw new ArgumentException("Email Address Type ID cannot be empty", nameof(emailAddressTypeId));

            // Log a warning since EmailAddressTypeId is not part of the Company_EmailAddress junction entity
            _logger.LogWarning("GetByEmailAddressTypeAsync called but Company_EmailAddress entity doesn't have an EmailAddressTypeId property. " +
                "Company ID: {CompanyId}, EmailAddressTypeId: {EmailAddressTypeId}",
                companyId, emailAddressTypeId);

            // Return all company-email addresses for the company since we can't filter by EmailAddressTypeId
            // This maintains compatibility with interfaces while ensuring the method is truly async
            return await GetByCompanyIdAsync(companyId, cancellationToken);
        }

        /// <summary>
        /// Gets a single company-email address relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="emailAddressId">The email address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-email address relationship if found, otherwise null</returns>
        private async Task<Company_EmailAddress?> GetSingleAsync(Guid companyId, Guid emailAddressId, CancellationToken cancellationToken = default)
        {
            return await GetByIdAsync(companyId, emailAddressId, cancellationToken);
        }
    }
}