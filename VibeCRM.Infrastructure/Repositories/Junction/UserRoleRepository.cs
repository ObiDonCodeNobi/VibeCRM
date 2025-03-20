using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing User_Role junction entities
    /// </summary>
    public class UserRoleRepository : BaseJunctionRepository<User_Role, Guid, Guid>, IUserRoleRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "User_Role";

        /// <summary>
        /// Gets the name of the first ID column (UserId)
        /// </summary>
        protected override string FirstIdColumnName => "UserId";

        /// <summary>
        /// Gets the name of the second ID column (RoleId)
        /// </summary>
        protected override string SecondIdColumnName => "RoleId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "UserId", "RoleId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public UserRoleRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<UserRoleRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all user-role relationships for a specific user
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of user-role relationships for the specified user</returns>
        public async Task<IEnumerable<User_Role>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @UserId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<User_Role>(
                    new CommandDefinition(
                        sql,
                        new { UserId = userId },
                        cancellationToken: cancellationToken)),
                "GetByUserIdAsync",
                new { UserId = userId, EntityType = nameof(User_Role) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all user-role relationships for a specific role
        /// </summary>
        /// <param name="roleId">The role identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of user-role relationships for the specified role</returns>
        public async Task<IEnumerable<User_Role>> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @RoleId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<User_Role>(
                    new CommandDefinition(
                        sql,
                        new { RoleId = roleId },
                        cancellationToken: cancellationToken)),
                "GetByRoleIdAsync",
                new { RoleId = roleId, EntityType = nameof(User_Role) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific user-role relationship
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="roleId">The role identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The user-role relationship if found, otherwise null</returns>
        public async Task<User_Role?> GetByUserAndRoleIdAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @UserId
                AND {SecondIdColumnName} = @RoleId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<User_Role>(
                    new CommandDefinition(
                        sql,
                        new { UserId = userId, RoleId = roleId },
                        cancellationToken: cancellationToken)),
                "GetByUserAndRoleIdAsync",
                new { UserId = userId, RoleId = roleId, EntityType = nameof(User_Role) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship exists between a user and a role
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="roleId">The role identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByUserAndRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT COUNT(1)
                FROM {TableName}
                WHERE {FirstIdColumnName} = @UserId
                AND {SecondIdColumnName} = @RoleId
                AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { UserId = userId, RoleId = roleId },
                        cancellationToken: cancellationToken)),
                "ExistsByUserAndRoleAsync",
                new { UserId = userId, RoleId = roleId, EntityType = nameof(User_Role) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Deletes a specific user-role relationship
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="roleId">The role identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByUserAndRoleIdAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @UserId
                AND {SecondIdColumnName} = @RoleId
                AND Active = 1";

            var parameters = new
            {
                UserId = userId,
                RoleId = roleId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByUserAndRoleIdAsync",
                new { UserId = userId, RoleId = roleId, EntityType = nameof(User_Role) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all user-role relationships for a specific user
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @UserId
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
                new { UserId = userId, EntityType = nameof(User_Role) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all user-role relationships for a specific role
        /// </summary>
        /// <param name="roleId">The role identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @RoleId
                AND Active = 1";

            var parameters = new
            {
                RoleId = roleId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByRoleIdAsync",
                new { RoleId = roleId, EntityType = nameof(User_Role) },
                cancellationToken);

            return rowsAffected > 0;
        }
    }
}