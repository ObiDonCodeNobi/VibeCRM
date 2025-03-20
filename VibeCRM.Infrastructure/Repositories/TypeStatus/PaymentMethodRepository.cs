using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing PaymentMethod entities
    /// </summary>
    public class PaymentMethodRepository : BaseRepository<PaymentMethod, Guid>, IPaymentMethodRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "PaymentMethod";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "PaymentMethodId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "PaymentMethodId", "Name", "Description", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the PaymentMethodRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        public PaymentMethodRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PaymentMethodRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new payment method to the database
        /// </summary>
        /// <param name="entity">The payment method entity to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added payment method with its assigned ID</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<PaymentMethod> AddAsync(PaymentMethod entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // Ensure ID is set
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            // Set audit fields if not already set
            var now = DateTime.UtcNow;
            if (entity.CreatedDate == default)
            {
                entity.CreatedDate = now;
            }
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = now;
            }

            // Ensure Active is set to true for new entities
            entity.Active = true;

            var sql = @"
                INSERT INTO PaymentMethod (
                    PaymentMethodId, Name, Description, OrdinalPosition,
                    CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, Active
                ) VALUES (
                    @PaymentMethodId, @Name, @Description, @OrdinalPosition,
                    @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy, @Active
                )";

            var parameters = new
            {
                PaymentMethodId = entity.Id,
                entity.Name,
                entity.Description,
                entity.OrdinalPosition,
                entity.CreatedDate,
                entity.CreatedBy,
                entity.ModifiedDate,
                entity.ModifiedBy,
                entity.Active
            };

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding {typeof(PaymentMethod).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(PaymentMethod) },
                cancellationToken);

            return await GetByIdAsync(entity.Id, cancellationToken);
        }

        /// <summary>
        /// Updates an existing payment method in the database
        /// </summary>
        /// <param name="entity">The payment method entity to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated payment method</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<PaymentMethod> UpdateAsync(PaymentMethod entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // Update the modified date
            entity.ModifiedDate = DateTime.UtcNow;

            var sql = @"
                UPDATE PaymentMethod SET
                    Name = @Name,
                    Description = @Description,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy
                WHERE PaymentMethodId = @PaymentMethodId AND Active = 1";

            var parameters = new
            {
                PaymentMethodId = entity.Id,
                entity.Name,
                entity.Description,
                entity.OrdinalPosition,
                entity.ModifiedDate,
                entity.ModifiedBy
            };

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating {typeof(PaymentMethod).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(PaymentMethod) },
                cancellationToken);

            return await GetByIdAsync(entity.Id, cancellationToken);
        }

        /// <summary>
        /// Retrieves payment methods ordered by their ordinal position.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of payment methods ordered by ordinal position.</returns>
        public async Task<IEnumerable<PaymentMethod>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT * FROM PaymentMethod
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<PaymentMethod>>(
                async (connection) => await connection.QueryAsync<PaymentMethod>(sql, transaction: null, commandTimeout: null),
                "GetByOrdinalPositionAsync",
                cancellationToken);
        }

        /// <summary>
        /// Retrieves payment methods by their name
        /// </summary>
        /// <param name="method">The payment method name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment methods matching the specified name</returns>
        public async Task<IEnumerable<PaymentMethod>> GetByMethodAsync(string method, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT * FROM PaymentMethod
                WHERE Name = @Method AND Active = 1";

            var parameters = new { Method = method };

            return await ExecuteWithResilienceAndLoggingAsync(
                async (connection) => await connection.QueryAsync<PaymentMethod>(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "GetByMethodAsync",
                new { ErrorMessage = $"Error retrieving payment methods with name: {method}", EntityType = nameof(PaymentMethod) },
                cancellationToken);
        }

        /// <summary>
        /// Retrieves the default payment method
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default payment method or null if none is found</returns>
        public async Task<PaymentMethod?> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT TOP 1 * FROM PaymentMethod
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async (connection) => await connection.QueryFirstOrDefaultAsync<PaymentMethod>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetDefaultAsync",
                new { ErrorMessage = "Error retrieving default payment method", EntityType = nameof(PaymentMethod) },
                cancellationToken);
        }
    }
}
