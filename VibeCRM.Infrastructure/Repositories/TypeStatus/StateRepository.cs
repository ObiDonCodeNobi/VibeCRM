using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.TypeStatus
{
    /// <summary>
    /// Repository implementation for managing State entities
    /// </summary>
    public class StateRepository : BaseRepository<State, Guid>, IStateRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "State";

        /// <summary>
        /// Gets the name of the ID column for the entity
        /// </summary>
        protected override string IdColumnName => "StateId";

        /// <summary>
        /// Gets the columns to select in basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "StateId", "Name", "Abbreviation", "CountryCode", "OrdinalPosition",
            "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the StateRepository class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory</param>
        /// <param name="logger">The logger</param>
        public StateRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<StateRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets states ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of states ordered by their ordinal position</returns>
        public async Task<IEnumerable<State>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<State>>(
                async (connection) => await connection.QueryAsync<State>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetByOrdinalPositionAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets states by name
        /// </summary>
        /// <param name="name">The state name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of states with the specified name</returns>
        public async Task<IEnumerable<State>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Name = @Name
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<State>>(
                async (connection) => await connection.QueryAsync<State>(
                    new CommandDefinition(
                        sql,
                        new { Name = name },
                        cancellationToken: cancellationToken)),
                "GetByNameAsync",
                new { Name = name, TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets states by abbreviation
        /// </summary>
        /// <param name="abbreviation">The state abbreviation to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of states with the specified abbreviation</returns>
        public async Task<IEnumerable<State>> GetByAbbreviationAsync(string abbreviation, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Abbreviation = @Abbreviation
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<State>>(
                async (connection) => await connection.QueryAsync<State>(
                    new CommandDefinition(
                        sql,
                        new { Abbreviation = abbreviation },
                        cancellationToken: cancellationToken)),
                "GetByAbbreviationAsync",
                new { Abbreviation = abbreviation, TableName },
                cancellationToken);
        }

        /// <summary>
        /// Gets the default state
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default state, or null if no states exist</returns>
        public async Task<State?> GetDefaultAsync(CancellationToken cancellationToken = default)
        {
            // Typically the default state would have the lowest ordinal position
            var sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY OrdinalPosition ASC";

            return await ExecuteWithResilienceAndLoggingAsync<State?>(
                async (connection) => await connection.QueryFirstOrDefaultAsync<State>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetDefaultAsync",
                new { TableName },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new state
        /// </summary>
        /// <param name="entity">The state to add</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The added state</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<State> AddAsync(State entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Ensure ID is set
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            // Set audit fields if not already set
            if (entity.CreatedDate == default)
            {
                entity.CreatedDate = DateTime.UtcNow;
            }

            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = entity.CreatedDate;
            }

            // Ensure Active is set
            entity.Active = true;

            var sql = @"
                INSERT INTO State (
                    StateId,
                    Name,
                    Abbreviation,
                    CountryCode,
                    OrdinalPosition,
                    CreatedDate,
                    CreatedBy,
                    ModifiedDate,
                    ModifiedBy,
                    Active
                ) VALUES (
                    @StateId,
                    @Name,
                    @Abbreviation,
                    @CountryCode,
                    @OrdinalPosition,
                    @CreatedDate,
                    @CreatedBy,
                    @ModifiedDate,
                    @ModifiedBy,
                    @Active
                )";

            var parameters = new
            {
                StateId = entity.Id,
                entity.Name,
                entity.Abbreviation,
                entity.CountryCode,
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
                new { ErrorMessage = $"Error adding {typeof(State).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(State) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing state
        /// </summary>
        /// <param name="entity">The state to update</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated state</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<State> UpdateAsync(State entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Update modified date if not already updated
            if (entity.ModifiedDate == default)
            {
                entity.ModifiedDate = DateTime.UtcNow;
            }

            var sql = @"
                UPDATE State
                SET Name = @Name,
                    Abbreviation = @Abbreviation,
                    CountryCode = @CountryCode,
                    OrdinalPosition = @OrdinalPosition,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    Active = @Active
                WHERE StateId = @StateId
                AND Active = 1";

            var parameters = new
            {
                StateId = entity.Id,
                entity.Name,
                entity.Abbreviation,
                entity.CountryCode,
                entity.OrdinalPosition,
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
                "UpdateAsync",
                new { ErrorMessage = $"Error updating {typeof(State).Name}", EntityId = entity.Id.ToString(), EntityType = nameof(State) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Checks if a state with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier of the state to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a state with the specified identifier exists; otherwise, false</returns>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM State
                    WHERE StateId = @Id
                    AND Active = 1
                ) THEN 1 ELSE 0 END";

            return await ExecuteWithResilienceAndLoggingAsync<bool>(
                async (connection) => await connection.ExecuteScalarAsync<bool>(
                    new CommandDefinition(
                        sql,
                        new { Id = id },
                        cancellationToken: cancellationToken)),
                "ExistsAsync",
                new { Id = id, TableName },
                cancellationToken);
        }
    }
}
