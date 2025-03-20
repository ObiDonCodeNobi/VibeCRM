using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing User entities
    /// </summary>
    public class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "User";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "UserId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "UserId", "PersonId", "LoginName", "LoginPassword", "LastLogin",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Base SELECT query for User entities
        /// </summary>
        private const string BaseSelectQuery = @"
            SELECT u.UserId AS Id, u.PersonId, u.LoginName, u.LoginPassword, u.LastLogin,
                   u.CreatedBy, u.CreatedDate, u.ModifiedBy, u.ModifiedDate, u.Active
            FROM [User] u
            WHERE u.Active = 1";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public UserRepository(ISQLConnectionFactory connectionFactory, ILogger<UserRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new user to the repository
        /// </summary>
        /// <param name="entity">The user to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added user with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when UserId is empty</exception>
        public override async Task<User> AddAsync(User entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.UserId == Guid.Empty) throw new ArgumentException("The User ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO [User] (
                    UserId, PersonId, LoginName, LoginPassword, LastLogin,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @UserId, @PersonId, @LoginName, @LoginPassword, @LastLogin,
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
                new { ErrorMessage = $"Error adding User with ID {entity.UserId}", UserId = entity.UserId, EntityType = nameof(User) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing user in the repository
        /// </summary>
        /// <param name="entity">The user to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated user</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when UserId is empty</exception>
        public override async Task<User> UpdateAsync(User entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.UserId == Guid.Empty) throw new ArgumentException("The User ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE [User] SET
                    PersonId = @PersonId,
                    LoginName = @LoginName,
                    LoginPassword = @LoginPassword,
                    LastLogin = @LastLogin,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                WHERE UserId = @UserId AND Active = 1;";

            int rowsAffected = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating User with ID {entity.UserId}", UserId = entity.UserId, EntityType = nameof(User) },
                cancellationToken);

            if (rowsAffected == 0)
            {
                _logger.LogWarning("User with ID {UserId} not found for update or already inactive", entity.UserId);
            }

            return entity;
        }

        /// <summary>
        /// Gets a user by their username
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The user if found, otherwise null</returns>
        public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND u.LoginName = @Username";

            return await ExecuteWithResilienceAndLoggingAsync<User?>(
                async connection => await connection.QuerySingleOrDefaultAsync<User>(
                    new CommandDefinition(
                        sql,
                        new { Username = username },
                        cancellationToken: cancellationToken)),
                "GetByUsernameAsync",
                new { Username = username },
                cancellationToken);
        }

        /// <summary>
        /// Gets a user by their email address
        /// </summary>
        /// <param name="email">The email address of the user</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The user if found, otherwise null</returns>
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT u.UserId AS Id, u.PersonId, u.LoginName, u.LoginPassword, u.LastLogin,
                       u.CreatedBy, u.CreatedDate, u.ModifiedBy, u.ModifiedDate, u.Active
                FROM [User] u
                JOIN Person p ON u.PersonId = p.PersonId
                JOIN Person_EmailAddress pe ON p.PersonId = pe.PersonId
                JOIN EmailAddress e ON pe.EmailAddressId = e.EmailAddressId
                WHERE e.Email = @Email
                  AND u.Active = 1
                  AND p.Active = 1
                  AND pe.Active = 1
                  AND e.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<User?>(
                async connection => await connection.QuerySingleOrDefaultAsync<User>(
                    new CommandDefinition(
                        sql,
                        new { Email = email },
                        cancellationToken: cancellationToken)),
                "GetByEmailAsync",
                new { Email = email },
                cancellationToken);
        }

        /// <summary>
        /// Gets all users that are members of a specific team
        /// </summary>
        /// <param name="teamId">The unique identifier of the team</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of users that are members of the specified team</returns>
        public async Task<IEnumerable<User>> GetByTeamIdAsync(Guid teamId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT u.UserId AS Id, u.PersonId, u.LoginName, u.LoginPassword, u.LastLogin,
                       u.CreatedBy, u.CreatedDate, u.ModifiedBy, u.ModifiedDate, u.Active
                FROM [User] u
                JOIN Employee e ON u.PersonId = e.PersonId
                JOIN Employee_Team et ON e.EmployeeId = et.EmployeeId
                WHERE et.TeamId = @TeamId
                  AND u.Active = 1
                  AND e.Active = 1
                  AND et.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<User>>(
                async connection => await connection.QueryAsync<User>(
                    new CommandDefinition(
                        sql,
                        new { TeamId = teamId },
                        cancellationToken: cancellationToken)),
                "GetByTeamIdAsync",
                new { TeamId = teamId },
                cancellationToken);
        }

        /// <summary>
        /// Gets all users that have a specific role
        /// </summary>
        /// <param name="roleId">The unique identifier of the role</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of users that have the specified role</returns>
        public async Task<IEnumerable<User>> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT u.UserId AS Id, u.PersonId, u.LoginName, u.LoginPassword, u.LastLogin,
                       u.CreatedBy, u.CreatedDate, u.ModifiedBy, u.ModifiedDate, u.Active
                FROM [User] u
                JOIN User_Role ur ON u.UserId = ur.UserId
                WHERE ur.RoleId = @RoleId
                  AND u.Active = 1
                  AND ur.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<User>>(
                async connection => await connection.QueryAsync<User>(
                    new CommandDefinition(
                        sql,
                        new { RoleId = roleId },
                        cancellationToken: cancellationToken)),
                "GetByRoleIdAsync",
                new { RoleId = roleId },
                cancellationToken);
        }
    }
}