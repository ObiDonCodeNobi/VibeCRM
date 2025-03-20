using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Payment_Note junction entities
    /// </summary>
    public class PaymentNoteRepository : BaseJunctionRepository<Payment_Note, Guid, Guid>, IPaymentNoteRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Payment_Note";

        /// <summary>
        /// Gets the name of the first ID column (PaymentId)
        /// </summary>
        protected override string FirstIdColumnName => "PaymentId";

        /// <summary>
        /// Gets the name of the second ID column (NoteId)
        /// </summary>
        protected override string SecondIdColumnName => "NoteId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "PaymentId", "NoteId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentNoteRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public PaymentNoteRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PaymentNoteRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all payment-note relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-note relationships for the specified payment</returns>
        public async Task<IEnumerable<Payment_Note>> GetByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PaymentId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Payment_Note>(
                    new CommandDefinition(
                        sql,
                        new { PaymentId = paymentId },
                        cancellationToken: cancellationToken)),
                "GetByPaymentIdAsync",
                new { PaymentId = paymentId, EntityType = nameof(Payment_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all payment-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-note relationships for the specified note</returns>
        public async Task<IEnumerable<Payment_Note>> GetByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @NoteId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Payment_Note>(
                    new CommandDefinition(
                        sql,
                        new { NoteId = noteId },
                        cancellationToken: cancellationToken)),
                "GetByNoteIdAsync",
                new { NoteId = noteId, EntityType = nameof(Payment_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific payment-note relationship
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The payment-note relationship if found, otherwise null</returns>
        public async Task<Payment_Note?> GetByPaymentAndNoteIdAsync(Guid paymentId, Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PaymentId
                AND {SecondIdColumnName} = @NoteId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Payment_Note>(
                    new CommandDefinition(
                        sql,
                        new { PaymentId = paymentId, NoteId = noteId },
                        cancellationToken: cancellationToken)),
                "GetByPaymentAndNoteIdAsync",
                new { PaymentId = paymentId, NoteId = noteId, EntityType = nameof(Payment_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a payment and a note
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByPaymentAndNoteAsync(Guid paymentId, Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PaymentId
                AND {SecondIdColumnName} = @NoteId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { PaymentId = paymentId, NoteId = noteId },
                        cancellationToken: cancellationToken)),
                "ExistsByPaymentAndNoteAsync",
                new { PaymentId = paymentId, NoteId = noteId, EntityType = nameof(Payment_Note) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Adds a new relationship between a payment and a note
        /// </summary>
        /// <param name="entity">The entity containing the payment and note identifiers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created payment-note relationship</returns>
        public override async Task<Payment_Note> AddAsync(Payment_Note entity, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                IF NOT EXISTS (
                    SELECT 1 FROM {TableName}
                    WHERE {FirstIdColumnName} = @PaymentId
                    AND {SecondIdColumnName} = @NoteId
                    AND Active = 1
                )
                BEGIN
                    INSERT INTO {TableName} ({FirstIdColumnName}, {SecondIdColumnName}, Active, ModifiedDate)
                    VALUES (@PaymentId, @NoteId, 1, @ModifiedDate)
                END
                ELSE
                BEGIN
                    UPDATE {TableName}
                    SET Active = 1, ModifiedDate = @ModifiedDate
                    WHERE {FirstIdColumnName} = @PaymentId
                    AND {SecondIdColumnName} = @NoteId
                END";

            var parameters = new
            {
                PaymentId = entity.PaymentId,
                NoteId = entity.NoteId,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { entity.PaymentId, entity.NoteId, EntityType = nameof(Payment_Note) },
                cancellationToken);

            entity.ModifiedDate = parameters.ModifiedDate;
            entity.Active = true;

            return entity;
        }

        /// <summary>
        /// Deletes a specific payment-note relationship
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByPaymentAndNoteIdAsync(Guid paymentId, Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PaymentId
                AND {SecondIdColumnName} = @NoteId
                AND Active = 1";

            var parameters = new
            {
                PaymentId = paymentId,
                NoteId = noteId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPaymentAndNoteIdAsync",
                new { PaymentId = paymentId, NoteId = noteId, EntityType = nameof(Payment_Note) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all payment-note relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PaymentId
                AND Active = 1";

            var parameters = new
            {
                PaymentId = paymentId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPaymentIdAsync",
                new { PaymentId = paymentId, EntityType = nameof(Payment_Note) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all payment-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @NoteId
                AND Active = 1";

            var parameters = new
            {
                NoteId = noteId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByNoteIdAsync",
                new { NoteId = noteId, EntityType = nameof(Payment_Note) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}