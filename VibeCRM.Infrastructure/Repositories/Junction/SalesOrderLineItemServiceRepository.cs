using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing SalesOrderLineItem_Service junction entities
    /// </summary>
    public class SalesOrderLineItemServiceRepository : BaseJunctionRepository<SalesOrderLineItem_Service, Guid, Guid>, ISalesOrderLineItemServiceRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "SalesOrderLineItem_Service";

        /// <summary>
        /// Gets the name of the first ID column (SalesOrderLineItemId)
        /// </summary>
        protected override string FirstIdColumnName => "SalesOrderLineItemId";

        /// <summary>
        /// Gets the name of the second ID column (ServiceId)
        /// </summary>
        protected override string SecondIdColumnName => "ServiceId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "SalesOrderLineItemId", "ServiceId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderLineItemServiceRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public SalesOrderLineItemServiceRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<SalesOrderLineItemServiceRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all salesOrderLineItem-service relationships for a specific sales order line item
        /// </summary>
        /// <param name="salesOrderLineItemId">The sales order line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrderLineItem-service relationships for the specified sales order line item</returns>
        public async Task<IEnumerable<SalesOrderLineItem_Service>> GetBySalesOrderLineItemIdAsync(Guid salesOrderLineItemId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @SalesOrderLineItemId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<SalesOrderLineItem_Service>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderLineItemId = salesOrderLineItemId },
                        cancellationToken: cancellationToken)),
                "GetBySalesOrderLineItemIdAsync",
                new { SalesOrderLineItemId = salesOrderLineItemId, EntityType = nameof(SalesOrderLineItem_Service) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all salesOrderLineItem-service relationships for a specific service
        /// </summary>
        /// <param name="serviceId">The service identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrderLineItem-service relationships for the specified service</returns>
        public async Task<IEnumerable<SalesOrderLineItem_Service>> GetByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @ServiceId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<SalesOrderLineItem_Service>(
                    new CommandDefinition(
                        sql,
                        new { ServiceId = serviceId },
                        cancellationToken: cancellationToken)),
                "GetByServiceIdAsync",
                new { ServiceId = serviceId, EntityType = nameof(SalesOrderLineItem_Service) },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new relationship between a sales order line item and a service
        /// </summary>
        /// <param name="entity">The entity containing the sales order line item and service identifiers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created salesOrderLineItem-service relationship</returns>
        public override async Task<SalesOrderLineItem_Service> AddAsync(SalesOrderLineItem_Service entity, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                IF NOT EXISTS (
                    SELECT 1 FROM {TableName}
                    WHERE {FirstIdColumnName} = @SalesOrderLineItemId
                    AND {SecondIdColumnName} = @ServiceId
                    AND Active = 1
                )
                BEGIN
                    INSERT INTO {TableName} ({FirstIdColumnName}, {SecondIdColumnName}, Active, ModifiedDate)
                    VALUES (@SalesOrderLineItemId, @ServiceId, 1, @ModifiedDate)
                END
                ELSE
                BEGIN
                    UPDATE {TableName}
                    SET Active = 1, ModifiedDate = @ModifiedDate
                    WHERE {FirstIdColumnName} = @SalesOrderLineItemId
                    AND {SecondIdColumnName} = @ServiceId
                END";

            var parameters = new
            {
                SalesOrderLineItemId = entity.SalesOrderLineItemId,
                ServiceId = entity.ServiceId,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { entity.SalesOrderLineItemId, entity.ServiceId, EntityType = nameof(SalesOrderLineItem_Service) },
                cancellationToken);

            entity.ModifiedDate = parameters.ModifiedDate;
            entity.Active = true;

            return entity;
        }

        /// <summary>
        /// Deletes a specific relationship between a sales order line item and a service
        /// </summary>
        /// <param name="firstId">The sales order line item identifier</param>
        /// <param name="secondId">The service identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was deleted, otherwise false</returns>
        public override async Task<bool> DeleteAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @SalesOrderLineItemId
                AND {SecondIdColumnName} = @ServiceId
                AND Active = 1";

            var parameters = new
            {
                SalesOrderLineItemId = firstId,
                ServiceId = secondId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteAsync",
                new { SalesOrderLineItemId = firstId, ServiceId = secondId, EntityType = nameof(SalesOrderLineItem_Service) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}