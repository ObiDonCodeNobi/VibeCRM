using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Person_Quote junction entities
    /// </summary>
    public class PersonQuoteRepository : BaseJunctionRepository<Person_Quote, Guid, Guid>, IPersonQuoteRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Person_Quote";

        /// <summary>
        /// Gets the name of the first ID column (PersonId)
        /// </summary>
        protected override string FirstIdColumnName => "PersonId";

        /// <summary>
        /// Gets the name of the second ID column (QuoteId)
        /// </summary>
        protected override string SecondIdColumnName => "QuoteId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "PersonId", "QuoteId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonQuoteRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public PersonQuoteRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PersonQuoteRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all person-quote relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-quote relationships for the specified person</returns>
        public async Task<IEnumerable<Person_Quote>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Quote>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetByPersonIdAsync",
                new { PersonId = personId, EntityType = nameof(Person_Quote) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all person-quote relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-quote relationships for the specified quote</returns>
        public async Task<IEnumerable<Person_Quote>> GetByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @QuoteId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Quote>(
                    new CommandDefinition(
                        sql,
                        new { QuoteId = quoteId },
                        cancellationToken: cancellationToken)),
                "GetByQuoteIdAsync",
                new { QuoteId = quoteId, EntityType = nameof(Person_Quote) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific person-quote relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person-quote relationship if found, otherwise null</returns>
        public async Task<Person_Quote?> GetByPersonAndQuoteIdAsync(Guid personId, Guid quoteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @QuoteId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Person_Quote>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, QuoteId = quoteId },
                        cancellationToken: cancellationToken)),
                "GetByPersonAndQuoteIdAsync",
                new { PersonId = personId, QuoteId = quoteId, EntityType = nameof(Person_Quote) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a person and a quote
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByPersonAndQuoteAsync(Guid personId, Guid quoteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @QuoteId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, QuoteId = quoteId },
                        cancellationToken: cancellationToken)),
                "ExistsByPersonAndQuoteAsync",
                new { PersonId = personId, QuoteId = quoteId, EntityType = nameof(Person_Quote) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Adds a new relationship between a person and a quote
        /// </summary>
        /// <param name="entity">The entity containing the person and quote identifiers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created person-quote relationship</returns>
        public override async Task<Person_Quote> AddAsync(Person_Quote entity, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                IF NOT EXISTS (
                    SELECT 1 FROM {TableName}
                    WHERE {FirstIdColumnName} = @PersonId
                    AND {SecondIdColumnName} = @QuoteId
                    AND Active = 1
                )
                BEGIN
                    INSERT INTO {TableName} ({FirstIdColumnName}, {SecondIdColumnName}, Active, ModifiedDate)
                    VALUES (@PersonId, @QuoteId, 1, @ModifiedDate)
                END
                ELSE
                BEGIN
                    UPDATE {TableName}
                    SET Active = 1, ModifiedDate = @ModifiedDate
                    WHERE {FirstIdColumnName} = @PersonId
                    AND {SecondIdColumnName} = @QuoteId
                END";

            var parameters = new
            {
                PersonId = entity.PersonId,
                QuoteId = entity.QuoteId,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { entity.PersonId, entity.QuoteId, EntityType = nameof(Person_Quote) },
                cancellationToken);

            entity.ModifiedDate = parameters.ModifiedDate;
            entity.Active = true;

            return entity;
        }

        /// <summary>
        /// Deletes a specific person-quote relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByPersonAndQuoteIdAsync(Guid personId, Guid quoteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @QuoteId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                QuoteId = quoteId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPersonAndQuoteIdAsync",
                new { PersonId = personId, QuoteId = quoteId, EntityType = nameof(Person_Quote) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-quote relationships for a specific person
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
                new { PersonId = personId, EntityType = nameof(Person_Quote) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-quote relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @QuoteId
                AND Active = 1";

            var parameters = new
            {
                QuoteId = quoteId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByQuoteIdAsync",
                new { QuoteId = quoteId, EntityType = nameof(Person_Quote) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}