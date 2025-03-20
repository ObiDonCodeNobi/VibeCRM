using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing SalesOrder_Note junction entities
    /// </summary>
    public class SalesOrderNoteRepository : BaseJunctionRepository<SalesOrder_Note, Guid, Guid>, ISalesOrderNoteRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "SalesOrder_Note";

        /// <summary>
        /// Gets the name of the first ID column (SalesOrderId)
        /// </summary>
        protected override string FirstIdColumnName => "SalesOrderId";

        /// <summary>
        /// Gets the name of the second ID column (NoteId)
        /// </summary>
        protected override string SecondIdColumnName => "NoteId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "SalesOrderId", "NoteId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderNoteRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public SalesOrderNoteRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<SalesOrderNoteRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all salesOrder-note relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrder-note relationships for the specified sales order</returns>
        public async Task<IEnumerable<SalesOrder_Note>> GetBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @SalesOrderId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<SalesOrder_Note>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderId = salesOrderId },
                        cancellationToken: cancellationToken)),
                "GetBySalesOrderIdAsync",
                new { SalesOrderId = salesOrderId, EntityType = nameof(SalesOrder_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all salesOrder-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrder-note relationships for the specified note</returns>
        public async Task<IEnumerable<SalesOrder_Note>> GetByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @NoteId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<SalesOrder_Note>(
                    new CommandDefinition(
                        sql,
                        new { NoteId = noteId },
                        cancellationToken: cancellationToken)),
                "GetByNoteIdAsync",
                new { NoteId = noteId, EntityType = nameof(SalesOrder_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific salesOrder-note relationship
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The salesOrder-note relationship if found, otherwise null</returns>
        public async Task<SalesOrder_Note?> GetBySalesOrderAndNoteIdAsync(Guid salesOrderId, Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @SalesOrderId
                AND {SecondIdColumnName} = @NoteId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<SalesOrder_Note>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderId = salesOrderId, NoteId = noteId },
                        cancellationToken: cancellationToken)),
                "GetBySalesOrderAndNoteIdAsync",
                new { SalesOrderId = salesOrderId, NoteId = noteId, EntityType = nameof(SalesOrder_Note) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a sales order and a note
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsBySalesOrderAndNoteAsync(Guid salesOrderId, Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @SalesOrderId
                AND {SecondIdColumnName} = @NoteId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { SalesOrderId = salesOrderId, NoteId = noteId },
                        cancellationToken: cancellationToken)),
                "ExistsBySalesOrderAndNoteAsync",
                new { SalesOrderId = salesOrderId, NoteId = noteId, EntityType = nameof(SalesOrder_Note) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Adds a new relationship between a sales order and a note
        /// </summary>
        /// <param name="entity">The entity containing the sales order and note identifiers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created salesOrder-note relationship</returns>
        public override async Task<SalesOrder_Note> AddAsync(SalesOrder_Note entity, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                IF NOT EXISTS (
                    SELECT 1 FROM {TableName}
                    WHERE {FirstIdColumnName} = @SalesOrderId
                    AND {SecondIdColumnName} = @NoteId
                    AND Active = 1
                )
                BEGIN
                    INSERT INTO {TableName} ({FirstIdColumnName}, {SecondIdColumnName}, Active, ModifiedDate)
                    VALUES (@SalesOrderId, @NoteId, 1, @ModifiedDate)
                END
                ELSE
                BEGIN
                    UPDATE {TableName}
                    SET Active = 1, ModifiedDate = @ModifiedDate
                    WHERE {FirstIdColumnName} = @SalesOrderId
                    AND {SecondIdColumnName} = @NoteId
                END";

            var parameters = new
            {
                SalesOrderId = entity.SalesOrderId,
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
                new { entity.SalesOrderId, entity.NoteId, EntityType = nameof(SalesOrder_Note) },
                cancellationToken);

            entity.ModifiedDate = parameters.ModifiedDate;
            entity.Active = true;

            return entity;
        }

        /// <summary>
        /// Deletes a specific salesOrder-note relationship
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteBySalesOrderAndNoteIdAsync(Guid salesOrderId, Guid noteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @SalesOrderId
                AND {SecondIdColumnName} = @NoteId
                AND Active = 1";

            var parameters = new
            {
                SalesOrderId = salesOrderId,
                NoteId = noteId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteBySalesOrderAndNoteIdAsync",
                new { SalesOrderId = salesOrderId, NoteId = noteId, EntityType = nameof(SalesOrder_Note) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all salesOrder-note relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @SalesOrderId
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
                new { SalesOrderId = salesOrderId, EntityType = nameof(SalesOrder_Note) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all salesOrder-note relationships for a specific note
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
                new { NoteId = noteId, EntityType = nameof(SalesOrder_Note) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}