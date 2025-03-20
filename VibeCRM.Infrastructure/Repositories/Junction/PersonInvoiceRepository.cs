using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Person_Invoice junction entities
    /// </summary>
    public class PersonInvoiceRepository : BaseJunctionRepository<Person_Invoice, Guid, Guid>, IPersonInvoiceRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Person_Invoice";

        /// <summary>
        /// Gets the name of the first ID column (PersonId)
        /// </summary>
        protected override string FirstIdColumnName => "PersonId";

        /// <summary>
        /// Gets the name of the second ID column (InvoiceId)
        /// </summary>
        protected override string SecondIdColumnName => "InvoiceId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "PersonId", "InvoiceId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonInvoiceRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public PersonInvoiceRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PersonInvoiceRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all person-invoice relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-invoice relationships for the specified person</returns>
        public async Task<IEnumerable<Person_Invoice>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Invoice>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetByPersonIdAsync",
                new { PersonId = personId, EntityType = nameof(Person_Invoice) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all person-invoice relationships for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-invoice relationships for the specified invoice</returns>
        public async Task<IEnumerable<Person_Invoice>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @InvoiceId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Invoice>(
                    new CommandDefinition(
                        sql,
                        new { InvoiceId = invoiceId },
                        cancellationToken: cancellationToken)),
                "GetByInvoiceIdAsync",
                new { InvoiceId = invoiceId, EntityType = nameof(Person_Invoice) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific person-invoice relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person-invoice relationship if found, otherwise null</returns>
        public async Task<Person_Invoice?> GetByPersonAndInvoiceIdAsync(Guid personId, Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @InvoiceId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Person_Invoice>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, InvoiceId = invoiceId },
                        cancellationToken: cancellationToken)),
                "GetByPersonAndInvoiceIdAsync",
                new { PersonId = personId, InvoiceId = invoiceId, EntityType = nameof(Person_Invoice) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a person and an invoice
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByPersonAndInvoiceAsync(Guid personId, Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @InvoiceId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, InvoiceId = invoiceId },
                        cancellationToken: cancellationToken)),
                "ExistsByPersonAndInvoiceAsync",
                new { PersonId = personId, InvoiceId = invoiceId, EntityType = nameof(Person_Invoice) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Adds a new relationship between a person and an invoice
        /// </summary>
        /// <param name="entity">The entity containing the person and invoice identifiers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created person-invoice relationship</returns>
        public override async Task<Person_Invoice> AddAsync(Person_Invoice entity, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                IF NOT EXISTS (
                    SELECT 1 FROM {TableName}
                    WHERE {FirstIdColumnName} = @PersonId
                    AND {SecondIdColumnName} = @InvoiceId
                    AND Active = 1
                )
                BEGIN
                    INSERT INTO {TableName} ({FirstIdColumnName}, {SecondIdColumnName}, Active, ModifiedDate)
                    VALUES (@PersonId, @InvoiceId, 1, @ModifiedDate)
                END
                ELSE
                BEGIN
                    UPDATE {TableName}
                    SET Active = 1, ModifiedDate = @ModifiedDate
                    WHERE {FirstIdColumnName} = @PersonId
                    AND {SecondIdColumnName} = @InvoiceId
                END";

            var parameters = new
            {
                PersonId = entity.PersonId,
                InvoiceId = entity.InvoiceId,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { entity.PersonId, entity.InvoiceId, EntityType = nameof(Person_Invoice) },
                cancellationToken);

            entity.ModifiedDate = parameters.ModifiedDate;
            entity.Active = true;

            return entity;
        }

        /// <summary>
        /// Deletes a specific person-invoice relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByPersonAndInvoiceIdAsync(Guid personId, Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @InvoiceId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                InvoiceId = invoiceId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPersonAndInvoiceIdAsync",
                new { PersonId = personId, InvoiceId = invoiceId, EntityType = nameof(Person_Invoice) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-invoice relationships for a specific person
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
                new { PersonId = personId, EntityType = nameof(Person_Invoice) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-invoice relationships for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @InvoiceId
                AND Active = 1";

            var parameters = new
            {
                InvoiceId = invoiceId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByInvoiceIdAsync",
                new { InvoiceId = invoiceId, EntityType = nameof(Person_Invoice) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}