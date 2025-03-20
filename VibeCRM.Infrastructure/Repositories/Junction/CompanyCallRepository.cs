using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Company_Call junction entities
    /// </summary>
    public class CompanyCallRepository : BaseJunctionRepository<Company_Call, Guid, Guid>, ICompanyCallRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Company_Call";

        /// <summary>
        /// Gets the name of the first ID column (CompanyId)
        /// </summary>
        protected override string FirstIdColumnName => "CompanyId";

        /// <summary>
        /// Gets the name of the second ID column (CallId)
        /// </summary>
        protected override string SecondIdColumnName => "CallId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "CompanyId", "CallId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyCallRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public CompanyCallRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<CompanyCallRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all company-call relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-call relationships for the specified company</returns>
        public async Task<IEnumerable<Company_Call>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            return await GetByFirstIdAsync(companyId, cancellationToken);
        }

        /// <summary>
        /// Gets all company-call relationships for a specific call
        /// </summary>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-call relationships for the specified call</returns>
        public async Task<IEnumerable<Company_Call>> GetByCallIdAsync(Guid callId, CancellationToken cancellationToken = default)
        {
            return await GetBySecondIdAsync(callId, cancellationToken);
        }

        /// <summary>
        /// Gets a specific company-call relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-call relationship if found, otherwise null</returns>
        public async Task<Company_Call?> GetByCompanyAndCallIdAsync(Guid companyId, Guid callId, CancellationToken cancellationToken = default)
        {
            return await GetByIdAsync(companyId, callId, cancellationToken);
        }

        /// <summary>
        /// Adds a relationship between a company and a call
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created company-call relationship</returns>
        public async Task<Company_Call> AddRelationshipAsync(Guid companyId, Guid callId, CancellationToken cancellationToken = default)
        {
            var entity = new Company_Call
            {
                CompanyId = companyId,
                CallId = callId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            var sql = $@"
                INSERT INTO {TableName} ({string.Join(", ", SelectColumns)})
                VALUES (@CompanyId, @CallId, @Active, @ModifiedDate)";

            var parameters = new { CompanyId = companyId, CallId = callId, Active = true, ModifiedDate = DateTime.UtcNow };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddRelationshipAsync",
                new { CompanyId = companyId, CallId = callId, EntityType = nameof(Company_Call) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Removes a relationship between a company and a call
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid companyId, Guid callId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0
                WHERE {FirstIdColumnName} = @CompanyId
                  AND {SecondIdColumnName} = @CallId";

            var parameters = new { CompanyId = companyId, CallId = callId };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveRelationshipAsync",
                new { CompanyId = companyId, CallId = callId, EntityType = nameof(Company_Call) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all company-call relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0
                WHERE {FirstIdColumnName} = @CompanyId";

            var parameters = new { CompanyId = companyId };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByCompanyIdAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Call) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all company-call relationships for a specific call
        /// </summary>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByCallIdAsync(Guid callId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0
                WHERE {SecondIdColumnName} = @CallId";

            var parameters = new { CallId = callId };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByCallIdAsync",
                new { CallId = callId, EntityType = nameof(Company_Call) },
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
            if (await DeleteByCompanyIdAsync(companyId, cancellationToken))
            {
                // Get approximate count by querying with Active = 0
                var sql = $@"
                    SELECT COUNT(*)
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
                    new { CompanyId = companyId, EntityType = nameof(Company_Call) },
                    cancellationToken);
            }

            return 0;
        }

        /// <summary>
        /// Removes all relationships for a specific call
        /// </summary>
        /// <param name="callId">The call identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForCallAsync(Guid callId, CancellationToken cancellationToken = default)
        {
            if (await DeleteByCallIdAsync(callId, cancellationToken))
            {
                // Get approximate count by querying with Active = 0
                var sql = $@"
                    SELECT COUNT(*)
                    FROM {TableName}
                    WHERE {SecondIdColumnName} = @SecondId
                      AND Active = 0";

                return await ExecuteWithResilienceAndLoggingAsync(
                    async connection => await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            new { SecondId = callId },
                            cancellationToken: cancellationToken)),
                    "RemoveAllForCallAsync",
                    new { CallId = callId, EntityType = nameof(Company_Call) },
                    cancellationToken);
            }

            return 0;
        }

        /// <summary>
        /// Gets company-call relationships by call type
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="callTypeId">The call type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-call relationships with the specified call type</returns>
        public async Task<IEnumerable<Company_Call>> GetByCallTypeAsync(Guid companyId, Guid callTypeId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT cc.{string.Join(", cc.", SelectColumns)}
                FROM {TableName} cc
                INNER JOIN Call c ON cc.CallId = c.Id
                WHERE cc.{FirstIdColumnName} = @CompanyId
                  AND c.CallTypeId = @CallTypeId
                  AND cc.Active = 1
                  AND c.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Call>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId, CallTypeId = callTypeId },
                        cancellationToken: cancellationToken)),
                "GetByCallTypeAsync",
                new { CompanyId = companyId, CallTypeId = callTypeId, EntityType = nameof(Company_Call) },
                cancellationToken);
        }
    }
}