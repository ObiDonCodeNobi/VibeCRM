using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Company_SalesOrder junction entities
    /// </summary>
    public class CompanySalesOrderRepository : BaseJunctionRepository<Company_SalesOrder, Guid, Guid>, ICompanySalesOrderRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Company_SalesOrder";

        /// <summary>
        /// Gets the name of the first ID column (CompanyId)
        /// </summary>
        protected override string FirstIdColumnName => "CompanyId";

        /// <summary>
        /// Gets the name of the second ID column (SalesOrderId)
        /// </summary>
        protected override string SecondIdColumnName => "SalesOrderId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "CompanyId", "SalesOrderId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanySalesOrderRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public CompanySalesOrderRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<CompanySalesOrderRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all company-sales order relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-sales order relationships for the specified company</returns>
        public async Task<IEnumerable<Company_SalesOrder>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT cso.{string.Join(", cso.", SelectColumns)}
                FROM {TableName} cso
                WHERE cso.{FirstIdColumnName} = @CompanyId
                  AND cso.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company_SalesOrder>>(
                async connection => await connection.QueryAsync<Company_SalesOrder>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyIdAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_SalesOrder) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all company-sales order relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-sales order relationships for the specified sales order</returns>
        public async Task<IEnumerable<Company_SalesOrder>> GetBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT cso.{string.Join(", cso.", SelectColumns)}
                FROM {TableName} cso
                WHERE cso.{SecondIdColumnName} = @SalesOrderId
                  AND cso.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company_SalesOrder>>(
                async connection => await connection.QueryAsync<Company_SalesOrder>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderId = salesOrderId },
                        cancellationToken: cancellationToken)),
                "GetBySalesOrderIdAsync",
                new { SalesOrderId = salesOrderId, EntityType = nameof(Company_SalesOrder) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific company-sales order relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-sales order relationship if found, otherwise null</returns>
        public async Task<Company_SalesOrder?> GetByCompanyAndSalesOrderIdAsync(Guid companyId, Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            return await GetByIdAsync(companyId, salesOrderId, cancellationToken);
        }

        /// <summary>
        /// Adds a relationship between a company and a sales order
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created company-sales order relationship</returns>
        public async Task<Company_SalesOrder> AddRelationshipAsync(Guid companyId, Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            var entity = new Company_SalesOrder
            {
                CompanyId = companyId,
                SalesOrderId = salesOrderId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            var sql = $@"
                INSERT INTO {TableName} ({string.Join(", ", SelectColumns)})
                VALUES (@CompanyId, @SalesOrderId, @Active, @ModifiedDate)";

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        entity,
                        cancellationToken: cancellationToken)),
                "AddRelationshipAsync",
                new { CompanyId = companyId, SalesOrderId = salesOrderId, EntityType = nameof(Company_SalesOrder) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Removes a relationship between a company and a sales order
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid companyId, Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0
                WHERE {FirstIdColumnName} = @CompanyId
                  AND {SecondIdColumnName} = @SalesOrderId";

            var parameters = new { CompanyId = companyId, SalesOrderId = salesOrderId };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveRelationshipAsync",
                new { CompanyId = companyId, SalesOrderId = salesOrderId, EntityType = nameof(Company_SalesOrder) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all company-sales order relationships for a specific company
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

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByCompanyIdAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_SalesOrder) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all company-sales order relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0
                WHERE {SecondIdColumnName} = @SalesOrderId";

            var parameters = new { SalesOrderId = salesOrderId };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteBySalesOrderIdAsync",
                new { SalesOrderId = salesOrderId, EntityType = nameof(Company_SalesOrder) },
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
            if (await DeleteByFirstIdAsync(companyId, cancellationToken))
            {
                // Get approximate count by querying with Active = 0
                var sql = $@"
                    SELECT COUNT(*)
                    FROM {TableName}
                    WHERE {FirstIdColumnName} = @FirstId
                      AND Active = 0";

                return await ExecuteWithResilienceAndLoggingAsync<int>(
                    async connection => await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            new { FirstId = companyId },
                            cancellationToken: cancellationToken)),
                    "RemoveAllForCompanyAsync",
                    new { CompanyId = companyId, EntityType = nameof(Company_SalesOrder) },
                    cancellationToken);
            }

            return 0;
        }

        /// <summary>
        /// Removes all relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForSalesOrderAsync(Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            if (await DeleteBySecondIdAsync(salesOrderId, cancellationToken))
            {
                // Get approximate count by querying with Active = 0
                var sql = $@"
                    SELECT COUNT(*)
                    FROM {TableName}
                    WHERE {SecondIdColumnName} = @SecondId
                      AND Active = 0";

                return await ExecuteWithResilienceAndLoggingAsync<int>(
                    async connection => await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            new { SecondId = salesOrderId },
                            cancellationToken: cancellationToken)),
                    "RemoveAllForSalesOrderAsync",
                    new { SalesOrderId = salesOrderId, EntityType = nameof(Company_SalesOrder) },
                    cancellationToken);
            }

            return 0;
        }

        /// <summary>
        /// Gets company-sales order relationships by date range
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-sales order relationships within the specified date range</returns>
        public async Task<IEnumerable<Company_SalesOrder>> GetByDateRangeAsync(Guid companyId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT cso.{string.Join(", cso.", SelectColumns)}
                FROM {TableName} cso
                INNER JOIN SalesOrder so ON cso.SalesOrderId = so.Id
                WHERE cso.{FirstIdColumnName} = @CompanyId
                  AND so.OrderDate >= @StartDate
                  AND so.OrderDate <= @EndDate
                  AND cso.Active = 1
                  AND so.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company_SalesOrder>>(
                async connection => await connection.QueryAsync<Company_SalesOrder>(
                    new CommandDefinition(
                        sql,
                        new
                        {
                            CompanyId = companyId,
                            StartDate = startDate,
                            EndDate = endDate
                        },
                        cancellationToken: cancellationToken)),
                "GetByDateRangeAsync",
                new { CompanyId = companyId, StartDate = startDate, EndDate = endDate, EntityType = nameof(Company_SalesOrder) },
                cancellationToken);
        }

        /// <summary>
        /// Gets company-sales order relationships by sales order status
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="salesOrderStatusId">The sales order status identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-sales order relationships with the specified status</returns>
        public async Task<IEnumerable<Company_SalesOrder>> GetBySalesOrderStatusAsync(Guid companyId, Guid salesOrderStatusId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT cso.{string.Join(", cso.", SelectColumns)}
                FROM {TableName} cso
                INNER JOIN SalesOrder so ON cso.SalesOrderId = so.Id
                WHERE cso.{FirstIdColumnName} = @CompanyId
                  AND so.SalesOrderStatusId = @SalesOrderStatusId
                  AND cso.Active = 1
                  AND so.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company_SalesOrder>>(
                async connection => await connection.QueryAsync<Company_SalesOrder>(
                    new CommandDefinition(
                        sql,
                        new
                        {
                            CompanyId = companyId,
                            SalesOrderStatusId = salesOrderStatusId
                        },
                        cancellationToken: cancellationToken)),
                "GetBySalesOrderStatusAsync",
                new { CompanyId = companyId, SalesOrderStatusId = salesOrderStatusId, EntityType = nameof(Company_SalesOrder) },
                cancellationToken);
        }
    }
}