using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Team_User junction entities
    /// </summary>
    public class TeamUserRepository : BaseJunctionRepository<Team_User, Guid, Guid>, ITeamUserRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Team_User";

        /// <summary>
        /// Gets the name of the first ID column (TeamId)
        /// </summary>
        protected override string FirstIdColumnName => "TeamId";

        /// <summary>
        /// Gets the name of the second ID column (UserId)
        /// </summary>
        protected override string SecondIdColumnName => "UserId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "TeamId", "UserId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamUserRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public TeamUserRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<TeamUserRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all team-user relationships for a specific team
        /// </summary>
        /// <param name="teamId">The team identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of team-user relationships for the specified team</returns>
        public async Task<IEnumerable<Team_User>> GetByTeamIdAsync(Guid teamId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @TeamId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Team_User>(
                    new CommandDefinition(
                        sql,
                        new { TeamId = teamId },
                        cancellationToken: cancellationToken)),
                "GetByTeamIdAsync",
                new { TeamId = teamId, EntityType = nameof(Team_User) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all team-user relationships for a specific user
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of team-user relationships for the specified user</returns>
        public async Task<IEnumerable<Team_User>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @UserId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Team_User>(
                    new CommandDefinition(
                        sql,
                        new { UserId = userId },
                        cancellationToken: cancellationToken)),
                "GetByUserIdAsync",
                new { UserId = userId, EntityType = nameof(Team_User) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific team-user relationship
        /// </summary>
        /// <param name="teamId">The team identifier</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The team-user relationship if found, otherwise null</returns>
        public async Task<Team_User?> GetByTeamAndUserIdAsync(Guid teamId, Guid userId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @TeamId
                AND {SecondIdColumnName} = @UserId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Team_User>(
                    new CommandDefinition(
                        sql,
                        new { TeamId = teamId, UserId = userId },
                        cancellationToken: cancellationToken)),
                "GetByTeamAndUserIdAsync",
                new { TeamId = teamId, UserId = userId, EntityType = nameof(Team_User) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a team and a user
        /// </summary>
        /// <param name="teamId">The team identifier</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByTeamAndUserAsync(Guid teamId, Guid userId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @TeamId
                AND {SecondIdColumnName} = @UserId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { TeamId = teamId, UserId = userId },
                        cancellationToken: cancellationToken)),
                "ExistsByTeamAndUserAsync",
                new { TeamId = teamId, UserId = userId, EntityType = nameof(Team_User) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Deletes a specific team-user relationship
        /// </summary>
        /// <param name="teamId">The team identifier</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByTeamAndUserIdAsync(Guid teamId, Guid userId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @TeamId
                AND {SecondIdColumnName} = @UserId
                AND Active = 1";

            var parameters = new
            {
                TeamId = teamId,
                UserId = userId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByTeamAndUserIdAsync",
                new { TeamId = teamId, UserId = userId, EntityType = nameof(Team_User) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all team-user relationships for a specific team
        /// </summary>
        /// <param name="teamId">The team identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByTeamIdAsync(Guid teamId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @TeamId
                AND Active = 1";

            var parameters = new
            {
                TeamId = teamId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByTeamIdAsync",
                new { TeamId = teamId, EntityType = nameof(Team_User) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all team-user relationships for a specific user
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @UserId
                AND Active = 1";

            var parameters = new
            {
                UserId = userId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByUserIdAsync",
                new { UserId = userId, EntityType = nameof(Team_User) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}