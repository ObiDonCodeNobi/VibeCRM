using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Company_Address junction entities
    /// </summary>
    public class CompanyAddressRepository : BaseJunctionRepository<Company_Address, Guid, Guid>, ICompanyAddressRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Company_Address";

        /// <summary>
        /// Gets the name of the first ID column (CompanyId)
        /// </summary>
        protected override string FirstIdColumnName => "CompanyId";

        /// <summary>
        /// Gets the name of the second ID column (AddressId)
        /// </summary>
        protected override string SecondIdColumnName => "AddressId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "CompanyId", "AddressId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyAddressRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public CompanyAddressRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<CompanyAddressRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all company-address relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-address relationships for the specified company</returns>
        public async Task<IEnumerable<Company_Address>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Address>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyIdAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all company-address relationships for a specific address
        /// </summary>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-address relationships for the specified address</returns>
        public async Task<IEnumerable<Company_Address>> GetByAddressIdAsync(Guid addressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @AddressId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Address>(
                    new CommandDefinition(
                        sql,
                        new { AddressId = addressId },
                        cancellationToken: cancellationToken)),
                "GetByAddressIdAsync",
                new { AddressId = addressId, EntityType = nameof(Company_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a company-address relationship by company and address identifiers
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-address relationship if found, otherwise null</returns>
        public async Task<Company_Address?> GetByCompanyAndAddressIdAsync(Guid companyId, Guid addressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @AddressId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Company_Address>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId, AddressId = addressId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyAndAddressIdAsync",
                new { CompanyId = companyId, AddressId = addressId, EntityType = nameof(Company_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a company and an address
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByCompanyAndAddressAsync(Guid companyId, Guid addressId, CancellationToken cancellationToken = default)
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
                        new { FirstId = companyId, SecondId = addressId },
                        cancellationToken: cancellationToken)),
                "ExistsByCompanyAndAddressAsync",
                new { CompanyId = companyId, AddressId = addressId, EntityType = nameof(Company_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Deletes a specific company-address relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByCompanyAndAddressIdAsync(Guid companyId, Guid addressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @AddressId
                AND Active = 1";

            var parameters = new
            {
                CompanyId = companyId,
                AddressId = addressId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByCompanyAndAddressIdAsync",
                new { CompanyId = companyId, AddressId = addressId, EntityType = nameof(Company_Address) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Gets company-address relationships for a specified company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The collection of relationships</returns>
        public async Task<IEnumerable<Company_Address>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Address>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Updates the timestamp on a company-address relationship to indicate when it was last modified
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated company-address relationship, or null if the relationship doesn't exist</returns>
        public async Task<Company_Address?> UpdateTimestampAsync(Guid companyId, Guid addressId, CancellationToken cancellationToken = default)
        {
            // Then set the specified address as primary
            var sql = $@"
                UPDATE {TableName}
                SET ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @AddressId
                AND Active = 1";

            var parameters = new
            {
                CompanyId = companyId,
                AddressId = addressId,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "UpdateTimestampAsync",
                new { CompanyId = companyId, AddressId = addressId, EntityType = nameof(Company_Address) },
                cancellationToken);

            // Return the updated entity
            return await GetByCompanyAndAddressIdAsync(companyId, addressId, cancellationToken);
        }

        /// <summary>
        /// Adds a relationship between a company and an address
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created company-address relationship</returns>
        public async Task<Company_Address> AddRelationshipAsync(
            Guid companyId,
            Guid addressId,
            CancellationToken cancellationToken = default)
        {
            var entity = new Company_Address
            {
                CompanyId = companyId,
                AddressId = addressId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            var sql = $@"
                INSERT INTO {TableName} ({string.Join(", ", SelectColumns)})
                VALUES (@CompanyId, @AddressId, @Active, @ModifiedDate)";

            var parameters = new
            {
                CompanyId = companyId,
                AddressId = addressId,
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
                new { CompanyId = companyId, AddressId = addressId, EntityType = nameof(Company_Address) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Removes a relationship between a company and an address
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid companyId, Guid addressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @AddressId
                AND Active = 1";

            var parameters = new
            {
                CompanyId = companyId,
                AddressId = addressId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveRelationshipAsync",
                new { CompanyId = companyId, AddressId = addressId, EntityType = nameof(Company_Address) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all company-address relationships for a specific company
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
                new { CompanyId = companyId, EntityType = nameof(Company_Address) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all company-address relationships for a specific address
        /// </summary>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByAddressIdAsync(Guid addressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @AddressId
                AND Active = 1";

            var parameters = new
            {
                AddressId = addressId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByAddressIdAsync",
                new { AddressId = addressId, EntityType = nameof(Company_Address) },
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
                new { CompanyId = companyId, EntityType = nameof(Company_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Removes all relationships for a specific address
        /// </summary>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForAddressAsync(Guid addressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @AddressId
                AND Active = 1";

            var parameters = new
            {
                AddressId = addressId,
                ModifiedDate = DateTime.UtcNow
            };

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveAllForAddressAsync",
                new { AddressId = addressId, EntityType = nameof(Company_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all addresses for a company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses for the company</returns>
        public async Task<IEnumerable<Address>> GetAddressesForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT a.*
                FROM Address a
                JOIN Company_Address ca ON a.Id = ca.AddressId
                WHERE ca.CompanyId = @CompanyId
                AND ca.Active = 1
                AND a.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Address>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetAddressesForCompanyAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets the primary address for a company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The primary company-address relationship if found, otherwise null</returns>
        /// <remarks>
        /// This implementation is a simplification as the Company_Address junction doesn't have a
        /// concept of "primary", so it returns the first address relationship found
        /// </remarks>
        public async Task<Company_Address?> GetPrimaryAddressForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Company_Address>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetPrimaryAddressForCompanyAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Sets an address as the primary address for a company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated company-address relationship, or null if the relationship doesn't exist</returns>
        /// <remarks>
        /// This implementation is a simplification as the Company_Address junction doesn't have a
        /// concept of "primary", so it just updates the timestamp on the record
        /// </remarks>
        public async Task<Company_Address?> SetPrimaryAddressAsync(Guid companyId, Guid addressId, CancellationToken cancellationToken = default)
        {
            // This functionality is now handled by UpdateTimestampAsync
            return await UpdateTimestampAsync(companyId, addressId, cancellationToken);
        }

        /// <summary>
        /// Gets all companies for a specific address
        /// </summary>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies for the address</returns>
        public async Task<IEnumerable<Company>> GetCompaniesForAddressAsync(Guid addressId, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT c.*
                FROM Company c
                JOIN Company_Address ca ON c.Id = ca.CompanyId
                WHERE ca.AddressId = @AddressId
                AND ca.Active = 1
                AND c.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company>(
                    new CommandDefinition(
                        sql,
                        new { AddressId = addressId },
                        cancellationToken: cancellationToken)),
                "GetCompaniesForAddressAsync",
                new { AddressId = addressId, EntityType = nameof(Company_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets company-address relationships by address type, implemented to maintain interface compatibility
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="addressTypeId">The address type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>Company-address relationships for the specified company</returns>
        /// <remarks>
        /// This method is required by the interface but the Company_Address entity doesn't have a
        /// concept of "address type", so it returns all address relationships for the company
        /// </remarks>
        public async Task<IEnumerable<Company_Address>> GetByAddressTypeAsync(Guid companyId, Guid addressTypeId, CancellationToken cancellationToken = default)
        {
            // Since Company_Address doesn't have address type, return all addresses for the company
            return await GetByCompanyIdAsync(companyId, cancellationToken);
        }
    }
}