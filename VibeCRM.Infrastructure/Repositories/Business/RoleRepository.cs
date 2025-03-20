using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing Role entities
    /// </summary>
    public class RoleRepository : BaseRepository<Role, Guid>, IRoleRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Role";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "RoleId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "RoleId", "Name", "Description",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Base SELECT query for Role entities
        /// </summary>
        private const string BaseSelectQuery = @"
            SELECT r.RoleId AS Id, r.Name, r.Description,
                   r.CreatedBy, r.CreatedDate, r.ModifiedBy, r.ModifiedDate, r.Active
            FROM Role r
            WHERE r.Active = 1";

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public RoleRepository(ISQLConnectionFactory connectionFactory, ILogger<RoleRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new role to the repository
        /// </summary>
        /// <param name="entity">The role to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added role with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when Id is empty</exception>
        public override async Task<Role> AddAsync(Role entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Id == Guid.Empty) throw new ArgumentException("The Role ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO Role (
                    RoleId, Name, Description,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @Id, @Name, @Description,
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
                new { ErrorMessage = $"Error adding Role with ID {entity.Id}", RoleId = entity.Id, EntityType = nameof(Role) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing role in the repository
        /// </summary>
        /// <param name="entity">The role to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated role</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when Id is empty</exception>
        public override async Task<Role> UpdateAsync(Role entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Id == Guid.Empty) throw new ArgumentException("The Role ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE Role SET
                    Name = @Name,
                    Description = @Description,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                WHERE RoleId = @Id AND Active = 1;";

            int rowsAffected = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating Role with ID {entity.Id}", RoleId = entity.Id, EntityType = nameof(Role) },
                cancellationToken);

            if (rowsAffected == 0)
            {
                _logger.LogWarning("Role with ID {RoleId} not found for update or already inactive", entity.Id);
            }

            return entity;
        }

        /// <summary>
        /// Gets a role by its name
        /// </summary>
        /// <param name="name">The name of the role</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The role if found, otherwise null</returns>
        public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND r.Name = @Name";

            return await ExecuteWithResilienceAndLoggingAsync<Role?>(
                async connection => await connection.QuerySingleOrDefaultAsync<Role>(
                    new CommandDefinition(
                        sql,
                        new { Name = name },
                        cancellationToken: cancellationToken)),
                "GetByNameAsync",
                new { Name = name },
                cancellationToken);
        }

        /// <summary>
        /// Gets all roles assigned to a specific user
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of roles assigned to the specified user</returns>
        public async Task<IEnumerable<Role>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT r.RoleId AS Id, r.Name, r.Description,
                       r.CreatedBy, r.CreatedDate, r.ModifiedBy, r.ModifiedDate, r.Active
                FROM Role r
                JOIN User_Role ur ON r.RoleId = ur.RoleId
                WHERE ur.UserId = @UserId
                  AND r.Active = 1
                  AND ur.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Role>>(
                async connection => await connection.QueryAsync<Role>(
                    new CommandDefinition(
                        sql,
                        new { UserId = userId },
                        cancellationToken: cancellationToken)),
                "GetByUserIdAsync",
                new { UserId = userId },
                cancellationToken);
        }
    }
}