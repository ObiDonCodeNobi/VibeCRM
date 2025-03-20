using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Person_SalesOrder junction entities
    /// </summary>
    public class PersonSalesOrderRepository : BaseJunctionRepository<Person_SalesOrder, Guid, Guid>, IPersonSalesOrderRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Person_SalesOrder";

        /// <summary>
        /// Gets the name of the first ID column (PersonId)
        /// </summary>
        protected override string FirstIdColumnName => "PersonId";

        /// <summary>
        /// Gets the name of the second ID column (SalesOrderId)
        /// </summary>
        protected override string SecondIdColumnName => "SalesOrderId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "PersonId", "SalesOrderId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonSalesOrderRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public PersonSalesOrderRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PersonSalesOrderRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all person-salesOrder relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-salesOrder relationships for the specified person</returns>
        public async Task<IEnumerable<Person_SalesOrder>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_SalesOrder>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetByPersonIdAsync",
                new { PersonId = personId, EntityType = nameof(Person_SalesOrder) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all person-salesOrder relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-salesOrder relationships for the specified sales order</returns>
        public async Task<IEnumerable<Person_SalesOrder>> GetBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @SalesOrderId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_SalesOrder>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderId = salesOrderId },
                        cancellationToken: cancellationToken)),
                "GetBySalesOrderIdAsync",
                new { SalesOrderId = salesOrderId, EntityType = nameof(Person_SalesOrder) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific person-salesOrder relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person-salesOrder relationship if found, otherwise null</returns>
        public async Task<Person_SalesOrder?> GetByPersonAndSalesOrderIdAsync(Guid personId, Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @SalesOrderId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Person_SalesOrder>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, SalesOrderId = salesOrderId },
                        cancellationToken: cancellationToken)),
                "GetByPersonAndSalesOrderIdAsync",
                new { PersonId = personId, SalesOrderId = salesOrderId, EntityType = nameof(Person_SalesOrder) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a person and a sales order
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByPersonAndSalesOrderAsync(Guid personId, Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @SalesOrderId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, SalesOrderId = salesOrderId },
                        cancellationToken: cancellationToken)),
                "ExistsByPersonAndSalesOrderAsync",
                new { PersonId = personId, SalesOrderId = salesOrderId, EntityType = nameof(Person_SalesOrder) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Adds a new relationship between a person and a sales order
        /// </summary>
        /// <param name="entity">The entity containing the person and sales order identifiers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created person-salesOrder relationship</returns>
        public override async Task<Person_SalesOrder> AddAsync(Person_SalesOrder entity, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                IF NOT EXISTS (
                    SELECT 1 FROM {TableName}
                    WHERE {FirstIdColumnName} = @PersonId
                    AND {SecondIdColumnName} = @SalesOrderId
                    AND Active = 1
                )
                BEGIN
                    INSERT INTO {TableName} ({FirstIdColumnName}, {SecondIdColumnName}, Active, ModifiedDate)
                    VALUES (@PersonId, @SalesOrderId, 1, @ModifiedDate)
                END
                ELSE
                BEGIN
                    UPDATE {TableName}
                    SET Active = 1, ModifiedDate = @ModifiedDate
                    WHERE {FirstIdColumnName} = @PersonId
                    AND {SecondIdColumnName} = @SalesOrderId
                END";

            var parameters = new
            {
                PersonId = entity.PersonId,
                SalesOrderId = entity.SalesOrderId,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { entity.PersonId, entity.SalesOrderId, EntityType = nameof(Person_SalesOrder) },
                cancellationToken);

            entity.ModifiedDate = parameters.ModifiedDate;
            entity.Active = true;

            return entity;
        }

        /// <summary>
        /// Deletes a specific person-salesOrder relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByPersonAndSalesOrderIdAsync(Guid personId, Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @SalesOrderId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                SalesOrderId = salesOrderId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPersonAndSalesOrderIdAsync",
                new { PersonId = personId, SalesOrderId = salesOrderId, EntityType = nameof(Person_SalesOrder) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-salesOrder relationships for a specific person
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
                new { PersonId = personId, EntityType = nameof(Person_SalesOrder) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-salesOrder relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @SalesOrderId
                AND Active = 1";

            var parameters = new
            {
                SalesOrderId = salesOrderId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteBySalesOrderIdAsync",
                new { SalesOrderId = salesOrderId, EntityType = nameof(Person_SalesOrder) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}