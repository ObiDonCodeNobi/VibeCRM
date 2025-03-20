using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing Team entities
    /// </summary>
    public class TeamRepository : BaseRepository<Team, Guid>, ITeamRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Team";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "TeamId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "TeamId", "TeamLeadEmployeeId", "Name", "Description",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Base SELECT query for Team entities
        /// </summary>
        private const string BaseSelectQuery = @"
            SELECT t.TeamId AS Id, t.TeamLeadEmployeeId, t.Name, t.Description,
                   t.CreatedBy, t.CreatedDate, t.ModifiedBy, t.ModifiedDate, t.Active
            FROM Team t
            WHERE t.Active = 1";

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public TeamRepository(ISQLConnectionFactory connectionFactory, ILogger<TeamRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new team to the repository
        /// </summary>
        /// <param name="entity">The team to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added team with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when TeamId is empty</exception>
        public override async Task<Team> AddAsync(Team entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.TeamId == Guid.Empty) throw new ArgumentException("The Team ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO Team (
                    TeamId, TeamLeadEmployeeId, Name, Description,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @TeamId, @TeamLeadEmployeeId, @Name, @Description,
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
                new { ErrorMessage = $"Error adding Team with ID {entity.TeamId}", TeamId = entity.TeamId, EntityType = nameof(Team) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing team in the repository
        /// </summary>
        /// <param name="entity">The team to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated team</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when TeamId is empty</exception>
        public override async Task<Team> UpdateAsync(Team entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.TeamId == Guid.Empty) throw new ArgumentException("The Team ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE Team SET
                    TeamLeadEmployeeId = @TeamLeadEmployeeId,
                    Name = @Name,
                    Description = @Description,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate
                WHERE TeamId = @TeamId AND Active = 1;";

            int rowsAffected = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating Team with ID {entity.TeamId}", TeamId = entity.TeamId, EntityType = nameof(Team) },
                cancellationToken);

            if (rowsAffected == 0)
            {
                _logger.LogWarning("Team with ID {TeamId} not found for update or already inactive", entity.TeamId);
            }

            return entity;
        }

        /// <summary>
        /// Gets a team by its name
        /// </summary>
        /// <param name="name">The name of the team</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The team if found, otherwise null</returns>
        public async Task<Team?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            string sql = $"{BaseSelectQuery} AND t.Name = @Name";

            return await ExecuteWithResilienceAndLoggingAsync<Team?>(
                async connection => await connection.QuerySingleOrDefaultAsync<Team>(
                    new CommandDefinition(
                        sql,
                        new { Name = name },
                        cancellationToken: cancellationToken)),
                "GetByNameAsync",
                new { Name = name },
                cancellationToken);
        }

        /// <summary>
        /// Gets all teams that a specific user is a member of
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of teams the specified user is a member of</returns>
        public async Task<IEnumerable<Team>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT t.TeamId AS Id, t.TeamLeadEmployeeId, t.Name, t.Description,
                       t.CreatedBy, t.CreatedDate, t.ModifiedBy, t.ModifiedDate, t.Active
                FROM Team t
                JOIN Employee_Team et ON t.TeamId = et.TeamId
                JOIN Employee e ON et.EmployeeId = e.EmployeeId
                JOIN [User] u ON e.UserId = u.UserId
                WHERE u.UserId = @UserId
                  AND t.Active = 1
                  AND et.Active = 1
                  AND e.Active = 1
                  AND u.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Team>>(
                async connection => await connection.QueryAsync<Team>(
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