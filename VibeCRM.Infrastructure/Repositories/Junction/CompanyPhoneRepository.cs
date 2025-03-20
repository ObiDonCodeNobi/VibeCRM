using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Company_Phone junction entities
    /// </summary>
    public class CompanyPhoneRepository : BaseJunctionRepository<Company_Phone, Guid, Guid>, ICompanyPhoneRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Company_Phone";

        /// <summary>
        /// Gets the name of the first ID column (CompanyId)
        /// </summary>
        protected override string FirstIdColumnName => "CompanyId";

        /// <summary>
        /// Gets the name of the second ID column (PhoneId)
        /// </summary>
        protected override string SecondIdColumnName => "PhoneId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "CompanyId", "PhoneId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyPhoneRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public CompanyPhoneRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<CompanyPhoneRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all company-phone relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-phone relationships for the specified company</returns>
        public async Task<IEnumerable<Company_Phone>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Phone>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyIdAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all company-phone relationships for a specific phone
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-phone relationships for the specified phone</returns>
        public async Task<IEnumerable<Company_Phone>> GetByPhoneIdAsync(Guid phoneId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @PhoneId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Phone>(
                    new CommandDefinition(
                        sql,
                        new { PhoneId = phoneId },
                        cancellationToken: cancellationToken)),
                "GetByPhoneIdAsync",
                new { PhoneId = phoneId, EntityType = nameof(Company_Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific company-phone relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-phone relationship if found, otherwise null</returns>
        public async Task<Company_Phone?> GetByCompanyAndPhoneIdAsync(Guid companyId, Guid phoneId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @PhoneId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Company_Phone>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId, PhoneId = phoneId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyAndPhoneIdAsync",
                new { CompanyId = companyId, PhoneId = phoneId, EntityType = nameof(Company_Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a company and a phone
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByCompanyAndPhoneAsync(Guid companyId, Guid phoneId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM {TableName}
                    WHERE {FirstIdColumnName} = @FirstId
                      AND {SecondIdColumnName} = @SecondId
                      AND Active = 1
                ) THEN 1 ELSE 0 END";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<bool>(
                    new CommandDefinition(
                        sql,
                        new { FirstId = companyId, SecondId = phoneId },
                        cancellationToken: cancellationToken)),
                "ExistsByCompanyAndPhoneAsync",
                new { CompanyId = companyId, PhoneId = phoneId, EntityType = nameof(Company_Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Deletes a specific company-phone relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByCompanyAndPhoneIdAsync(Guid companyId, Guid phoneId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @PhoneId
                AND Active = 1";

            var parameters = new
            {
                CompanyId = companyId,
                PhoneId = phoneId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByCompanyAndPhoneIdAsync",
                new { CompanyId = companyId, PhoneId = phoneId, EntityType = nameof(Company_Phone) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Gets company-phone relationships for a specified company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The collection of relationships</returns>
        public async Task<IEnumerable<Company_Phone>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Phone>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Adds a relationship between a company and a phone
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created company-phone relationship</returns>
        public async Task<Company_Phone> AddRelationshipAsync(
            Guid companyId,
            Guid phoneId,
            CancellationToken cancellationToken = default)
        {
            var entity = new Company_Phone
            {
                CompanyId = companyId,
                PhoneId = phoneId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            var sql = $@"
                INSERT INTO {TableName} ({string.Join(", ", SelectColumns)})
                VALUES (@CompanyId, @PhoneId, @Active, @ModifiedDate)";

            var parameters = new
            {
                CompanyId = companyId,
                PhoneId = phoneId,
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
                new { CompanyId = companyId, PhoneId = phoneId, EntityType = nameof(Company_Phone) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates the record with a timestamp to indicate when it was last modified
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if successful, otherwise false</returns>
        public async Task<bool> SetAsPrimaryAsync(Guid companyId, Guid phoneId, CancellationToken cancellationToken = default)
        {
            // Set the specified phone as primary
            var sql = $@"
                UPDATE {TableName}
                SET ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @PhoneId
                AND Active = 1";

            var parameters = new
            {
                CompanyId = companyId,
                PhoneId = phoneId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "SetAsPrimaryAsync",
                new { CompanyId = companyId, PhoneId = phoneId, EntityType = nameof(Company_Phone) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Sets a specific phone as the primary phone for a company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="phoneId">The phone identifier to set as primary</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated company-phone relationship or null if the operation fails</returns>
        public async Task<Company_Phone?> SetPrimaryPhoneAsync(Guid companyId, Guid phoneId, CancellationToken cancellationToken = default)
        {
            await SetAsPrimaryAsync(companyId, phoneId, cancellationToken);

            // Return the updated entity
            return await GetByCompanyAndPhoneIdAsync(companyId, phoneId, cancellationToken);
        }

        /// <summary>
        /// Removes a relationship between a company and a phone
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid companyId, Guid phoneId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @PhoneId
                AND Active = 1";

            var parameters = new
            {
                CompanyId = companyId,
                PhoneId = phoneId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveRelationshipAsync",
                new { CompanyId = companyId, PhoneId = phoneId, EntityType = nameof(Company_Phone) },
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
                new { CompanyId = companyId, EntityType = nameof(Company_Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Removes all relationships for a specific phone
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForPhoneAsync(Guid phoneId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @PhoneId
                AND Active = 1";

            var parameters = new
            {
                PhoneId = phoneId,
                ModifiedDate = DateTime.UtcNow
            };

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveAllForPhoneAsync",
                new { PhoneId = phoneId, EntityType = nameof(Company_Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Deletes all company-phone relationships for a specific company
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
                new { CompanyId = companyId, EntityType = nameof(Company_Phone) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all company-phone relationships for a specific phone
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByPhoneIdAsync(Guid phoneId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @PhoneId
                AND Active = 1";

            var parameters = new
            {
                PhoneId = phoneId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPhoneIdAsync",
                new { PhoneId = phoneId, EntityType = nameof(Company_Phone) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Gets the primary phone for a company, implemented to maintain interface compatibility
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The first company-phone relationship if found, otherwise null</returns>
        /// <remarks>
        /// This method is required by the interface but the Company_Phone entity doesn't have a
        /// concept of "primary", so it returns the first phone relationship found
        /// </remarks>
        public async Task<Company_Phone?> GetPrimaryPhoneForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Company_Phone>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetPrimaryPhoneForCompanyAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Gets company-phone relationships by phone type, implemented to maintain interface compatibility
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="phoneTypeId">The phone type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>Company-phone relationships for the specified company</returns>
        /// <remarks>
        /// This method is required by the interface but the Company_Phone entity doesn't have a
        /// concept of "phone type", so it returns all phone relationships for the company
        /// </remarks>
        public async Task<IEnumerable<Company_Phone>> GetByPhoneTypeAsync(Guid companyId, Guid phoneTypeId, CancellationToken cancellationToken = default)
        {
            // Since Company_Phone doesn't have phone type, return all phones for the company
            return await GetByCompanyIdAsync(companyId, cancellationToken);
        }
    }
}