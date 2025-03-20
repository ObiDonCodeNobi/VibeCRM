using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing Payment entities
    /// </summary>
    public class PaymentRepository : BaseRepository<Payment, Guid>, IPaymentRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Payment";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "PaymentId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "PaymentId", "InvoiceId", "PaymentTypeId", "PaymentMethodTypeId", "PaymentStatusId",
            "PaymentDate", "Amount", "ReferenceNumber", "Notes",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Base SELECT query for Payment entities
        /// </summary>
        private const string BaseSelectQuery = @"
            SELECT p.PaymentId AS Id, p.InvoiceId, p.PaymentTypeId, p.PaymentMethodTypeId, p.PaymentStatusId,
                   p.PaymentDate, p.Amount, p.ReferenceNumber, p.Notes,
                   p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
            FROM Payment p
            WHERE p.Active = 1";

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public PaymentRepository(ISQLConnectionFactory connectionFactory, ILogger<PaymentRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new payment to the repository
        /// </summary>
        /// <param name="entity">The payment to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added payment with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when PaymentId is empty</exception>
        public override async Task<Payment> AddAsync(Payment entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.PaymentId == Guid.Empty) throw new ArgumentException("The Payment ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO Payment (
                    PaymentId, InvoiceId, PaymentTypeId, PaymentMethodTypeId, PaymentStatusId,
                    PaymentDate, Amount, ReferenceNumber, Notes,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @PaymentId, @InvoiceId, @PaymentTypeId, @PaymentMethodTypeId, @PaymentStatusId,
                    @PaymentDate, @Amount, @ReferenceNumber, @Notes,
                    @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, 1
                );";

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding Payment with ID {entity.PaymentId}", PaymentId = entity.PaymentId, EntityType = nameof(Payment) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing payment in the repository
        /// </summary>
        /// <param name="entity">The payment to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated payment</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when PaymentId is empty</exception>
        public override async Task<Payment> UpdateAsync(Payment entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.PaymentId == Guid.Empty) throw new ArgumentException("The Payment ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE Payment SET
                    InvoiceId = @InvoiceId,
                    PaymentTypeId = @PaymentTypeId,
                    PaymentMethodTypeId = @PaymentMethodTypeId,
                    PaymentStatusId = @PaymentStatusId,
                    PaymentDate = @PaymentDate,
                    Amount = @Amount,
                    ReferenceNumber = @ReferenceNumber,
                    Notes = @Notes,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                WHERE PaymentId = @PaymentId AND Active = 1;";

            int rowsAffected = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating Payment with ID {entity.PaymentId}", PaymentId = entity.PaymentId, EntityType = nameof(Payment) },
                cancellationToken);

            if (rowsAffected == 0)
            {
                _logger.LogWarning("Payment with ID {PaymentId} not found for update or already inactive", entity.PaymentId);
            }

            return entity;
        }

        /// <summary>
        /// Gets payments associated with a specific invoice
        /// </summary>
        /// <param name="invoiceId">The unique identifier of the invoice</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payments associated with the specified invoice</returns>
        public async Task<IEnumerable<Payment>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND p.InvoiceId = @InvoiceId";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Payment>>(
                async connection => await connection.QueryAsync<Payment>(
                    new CommandDefinition(
                        sql,
                        new { InvoiceId = invoiceId },
                        cancellationToken: cancellationToken)),
                "GetByInvoiceIdAsync",
                new { InvoiceId = invoiceId },
                cancellationToken);
        }

        /// <summary>
        /// Gets payments made by a specific company
        /// </summary>
        /// <param name="companyId">The unique identifier of the company</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payments made by the specified company</returns>
        public async Task<IEnumerable<Payment>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT p.PaymentId AS Id, p.InvoiceId, p.PaymentTypeId, p.PaymentMethodTypeId, p.PaymentStatusId,
                       p.PaymentDate, p.Amount, p.ReferenceNumber, p.Notes,
                       p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Payment p
                JOIN Invoice i ON p.InvoiceId = i.InvoiceId
                WHERE i.CompanyId = @CompanyId
                  AND p.Active = 1
                  AND i.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Payment>>(
                async connection => await connection.QueryAsync<Payment>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyIdAsync",
                new { CompanyId = companyId },
                cancellationToken);
        }

        /// <summary>
        /// Gets payments made by a specific person
        /// </summary>
        /// <param name="personId">The unique identifier of the person</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payments made by the specified person</returns>
        public async Task<IEnumerable<Payment>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT p.PaymentId AS Id, p.InvoiceId, p.PaymentTypeId, p.PaymentMethodTypeId, p.PaymentStatusId,
                       p.PaymentDate, p.Amount, p.ReferenceNumber, p.Notes,
                       p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Payment p
                JOIN Invoice i ON p.InvoiceId = i.InvoiceId
                WHERE i.PersonId = @PersonId
                  AND p.Active = 1
                  AND i.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Payment>>(
                async connection => await connection.QueryAsync<Payment>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetByPersonIdAsync",
                new { PersonId = personId },
                cancellationToken);
        }

        /// <summary>
        /// Gets payments by their payment status
        /// </summary>
        /// <param name="paymentStatusId">The payment status identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payments with the specified status</returns>
        public async Task<IEnumerable<Payment>> GetByPaymentStatusAsync(Guid paymentStatusId, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND p.PaymentStatusId = @PaymentStatusId";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Payment>>(
                async connection => await connection.QueryAsync<Payment>(
                    new CommandDefinition(
                        sql,
                        new { PaymentStatusId = paymentStatusId },
                        cancellationToken: cancellationToken)),
                "GetByPaymentStatusAsync",
                new { PaymentStatusId = paymentStatusId },
                cancellationToken);
        }

        /// <summary>
        /// Gets payments made using a specific payment method
        /// </summary>
        /// <param name="paymentMethodId">The unique identifier of the payment method</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payments made using the specified payment method</returns>
        public async Task<IEnumerable<Payment>> GetByPaymentMethodIdAsync(Guid paymentMethodId, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND p.PaymentMethodTypeId = @PaymentMethodId";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Payment>>(
                async connection => await connection.QueryAsync<Payment>(
                    new CommandDefinition(
                        sql,
                        new { PaymentMethodId = paymentMethodId },
                        cancellationToken: cancellationToken)),
                "GetByPaymentMethodIdAsync",
                new { PaymentMethodId = paymentMethodId },
                cancellationToken);
        }

        /// <summary>
        /// Gets payments made within a specific date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payments made within the specified date range</returns>
        public async Task<IEnumerable<Payment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND p.PaymentDate >= @StartDate AND p.PaymentDate <= @EndDate";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Payment>>(
                async connection => await connection.QueryAsync<Payment>(
                    new CommandDefinition(
                        sql,
                        new { StartDate = startDate, EndDate = endDate },
                        cancellationToken: cancellationToken)),
                "GetByDateRangeAsync",
                new { StartDate = startDate, EndDate = endDate },
                cancellationToken);
        }
    }
}