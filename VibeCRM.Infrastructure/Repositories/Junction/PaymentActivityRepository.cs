using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Payment_Activity junction entities
    /// </summary>
    public class PaymentActivityRepository : BaseJunctionRepository<Payment_Activity, Guid, Guid>, IPaymentActivityRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Payment_Activity";

        /// <summary>
        /// Gets the name of the first ID column (PaymentId)
        /// </summary>
        protected override string FirstIdColumnName => "PaymentId";

        /// <summary>
        /// Gets the name of the second ID column (ActivityId)
        /// </summary>
        protected override string SecondIdColumnName => "ActivityId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "PaymentId", "ActivityId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentActivityRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public PaymentActivityRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PaymentActivityRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all payment-activity relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-activity relationships for the specified payment</returns>
        public async Task<IEnumerable<Payment_Activity>> GetByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PaymentId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Payment_Activity>(
                    new CommandDefinition(
                        sql,
                        new { PaymentId = paymentId },
                        cancellationToken: cancellationToken)),
                "GetByPaymentIdAsync",
                new { PaymentId = paymentId, EntityType = nameof(Payment_Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all payment-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment-activity relationships for the specified activity</returns>
        public async Task<IEnumerable<Payment_Activity>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Payment_Activity>(
                    new CommandDefinition(
                        sql,
                        new { ActivityId = activityId },
                        cancellationToken: cancellationToken)),
                "GetByActivityIdAsync",
                new { ActivityId = activityId, EntityType = nameof(Payment_Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new relationship between a payment and an activity
        /// </summary>
        /// <param name="entity">The entity containing the payment and activity identifiers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created payment-activity relationship</returns>
        public override async Task<Payment_Activity> AddAsync(Payment_Activity entity, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                IF NOT EXISTS (
                    SELECT 1 FROM {TableName}
                    WHERE {FirstIdColumnName} = @PaymentId
                    AND {SecondIdColumnName} = @ActivityId
                    AND Active = 1
                )
                BEGIN
                    INSERT INTO {TableName} ({FirstIdColumnName}, {SecondIdColumnName}, Active, ModifiedDate)
                    VALUES (@PaymentId, @ActivityId, 1, @ModifiedDate)
                END
                ELSE
                BEGIN
                    UPDATE {TableName}
                    SET Active = 1, ModifiedDate = @ModifiedDate
                    WHERE {FirstIdColumnName} = @PaymentId
                    AND {SecondIdColumnName} = @ActivityId
                END";

            var parameters = new
            {
                PaymentId = entity.PaymentId,
                ActivityId = entity.ActivityId,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { entity.PaymentId, entity.ActivityId, EntityType = nameof(Payment_Activity) },
                cancellationToken);

            entity.ModifiedDate = parameters.ModifiedDate;
            entity.Active = true;

            return entity;
        }

        /// <summary>
        /// Removes a relationship between a payment and an activity
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid paymentId, Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PaymentId
                AND {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            var parameters = new
            {
                PaymentId = paymentId,
                ActivityId = activityId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveRelationshipAsync",
                new { PaymentId = paymentId, ActivityId = activityId, EntityType = nameof(Payment_Activity) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Removes all relationships for a specific payment
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForPaymentAsync(Guid paymentId, CancellationToken cancellationToken = default)
        {
            if (await DeleteByPaymentIdAsync(paymentId, cancellationToken))
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
                            new { FirstId = paymentId },
                            cancellationToken: cancellationToken)),
                    "RemoveAllForPaymentAsync",
                    new { PaymentId = paymentId, EntityType = nameof(Payment_Activity) },
                    cancellationToken);
            }

            return 0;
        }

        /// <summary>
        /// Removes all relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForActivityAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            if (await DeleteByActivityIdAsync(activityId, cancellationToken))
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
                            new { SecondId = activityId },
                            cancellationToken: cancellationToken)),
                    "RemoveAllForActivityAsync",
                    new { ActivityId = activityId, EntityType = nameof(Payment_Activity) },
                    cancellationToken);
            }

            return 0;
        }

        /// <summary>
        /// Deletes all payment-activity relationships for a specific payment
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
                new { PaymentId = paymentId, EntityType = nameof(Payment_Activity) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all payment-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @ActivityId
                AND Active = 1";

            var parameters = new
            {
                ActivityId = activityId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByActivityIdAsync",
                new { ActivityId = activityId, EntityType = nameof(Payment_Activity) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes a specific relationship between a payment and an activity
        /// </summary>
        /// <param name="paymentId">The payment identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was deleted, otherwise false</returns>
        public async Task<bool> DeleteByPaymentAndActivityAsync(Guid paymentId, Guid activityId, CancellationToken cancellationToken = default)
        {
            return await RemoveRelationshipAsync(paymentId, activityId, cancellationToken);
        }
    }
}