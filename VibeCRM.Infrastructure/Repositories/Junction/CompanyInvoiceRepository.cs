using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Company_Invoice junction entities
    /// </summary>
    public class CompanyInvoiceRepository : BaseJunctionRepository<Company_Invoice, Guid, Guid>, ICompanyInvoiceRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Company_Invoice";

        /// <summary>
        /// Gets the name of the first ID column (CompanyId)
        /// </summary>
        protected override string FirstIdColumnName => "CompanyId";

        /// <summary>
        /// Gets the name of the second ID column (InvoiceId)
        /// </summary>
        protected override string SecondIdColumnName => "InvoiceId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "CompanyId", "InvoiceId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyInvoiceRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public CompanyInvoiceRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<CompanyInvoiceRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all company-invoice relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-invoice relationships for the specified company</returns>
        public async Task<IEnumerable<Company_Invoice>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Invoice>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyIdAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Invoice) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all company-invoice relationships for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-invoice relationships for the specified invoice</returns>
        public async Task<IEnumerable<Company_Invoice>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @InvoiceId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Invoice>(
                    new CommandDefinition(
                        sql,
                        new { InvoiceId = invoiceId },
                        cancellationToken: cancellationToken)),
                "GetByInvoiceIdAsync",
                new { InvoiceId = invoiceId, EntityType = nameof(Company_Invoice) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific company-invoice relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-invoice relationship if found, otherwise null</returns>
        public async Task<Company_Invoice?> GetByCompanyAndInvoiceIdAsync(Guid companyId, Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @InvoiceId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Company_Invoice>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId, InvoiceId = invoiceId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyAndInvoiceIdAsync",
                new { CompanyId = companyId, InvoiceId = invoiceId, EntityType = nameof(Company_Invoice) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a company and an invoice
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByCompanyAndInvoiceAsync(Guid companyId, Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @InvoiceId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId, InvoiceId = invoiceId },
                        cancellationToken: cancellationToken)),
                "ExistsByCompanyAndInvoiceAsync",
                new { CompanyId = companyId, InvoiceId = invoiceId, EntityType = nameof(Company_Invoice) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Adds a new relationship between a company and an invoice
        /// </summary>
        /// <param name="entity">The entity containing the company and invoice identifiers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created company-invoice relationship</returns>
        public override async Task<Company_Invoice> AddAsync(Company_Invoice entity, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                IF NOT EXISTS (
                    SELECT 1 FROM {TableName}
                    WHERE {FirstIdColumnName} = @CompanyId
                    AND {SecondIdColumnName} = @InvoiceId
                    AND Active = 1
                )
                BEGIN
                    INSERT INTO {TableName} ({FirstIdColumnName}, {SecondIdColumnName}, Active, ModifiedDate)
                    VALUES (@CompanyId, @InvoiceId, 1, @ModifiedDate)
                END
                ELSE
                BEGIN
                    UPDATE {TableName}
                    SET Active = 1, ModifiedDate = @ModifiedDate
                    WHERE {FirstIdColumnName} = @CompanyId
                    AND {SecondIdColumnName} = @InvoiceId
                END";

            var parameters = new
            {
                CompanyId = entity.CompanyId,
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
                new { entity.CompanyId, entity.InvoiceId, EntityType = nameof(Company_Invoice) },
                cancellationToken);

            entity.ModifiedDate = parameters.ModifiedDate;
            entity.Active = true;

            return entity;
        }

        /// <summary>
        /// Deletes a specific company-invoice relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByCompanyAndInvoiceIdAsync(Guid companyId, Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @InvoiceId
                AND Active = 1";

            var parameters = new
            {
                CompanyId = companyId,
                InvoiceId = invoiceId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByCompanyAndInvoiceIdAsync",
                new { CompanyId = companyId, InvoiceId = invoiceId, EntityType = nameof(Company_Invoice) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all company-invoice relationships for a specific company
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
                new { CompanyId = companyId, EntityType = nameof(Company_Invoice) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all company-invoice relationships for a specific invoice
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
                new { InvoiceId = invoiceId, EntityType = nameof(Company_Invoice) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}