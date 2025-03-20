using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing Activity entities
    /// </summary>
    public class ActivityRepository : BaseRepository<Activity, Guid>, IActivityRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Activity";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "ActivityId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "ActivityId", "ActivityTypeId", "ActivityStatusId", "AssignedUserId", "AssignedTeamId",
            "Subject", "Description", "DueDate", "StartDate", "CompletedDate", "CompletedBy",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public ActivityRepository(ISQLConnectionFactory connectionFactory, ILogger<ActivityRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new activity to the repository
        /// </summary>
        /// <param name="entity">The activity to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added activity with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when ActivityId is empty</exception>
        public override async Task<Activity> AddAsync(Activity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.ActivityId == Guid.Empty) throw new ArgumentException("The Activity ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO Activity (
                    ActivityId, ActivityTypeId, ActivityStatusId, AssignedUserId, AssignedTeamId,
                    Subject, Description, DueDate, StartDate, CompletedDate, CompletedBy,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @ActivityId, @ActivityTypeId, @ActivityStatusId, @AssignedUserId, @AssignedTeamId,
                    @Subject, @Description, @DueDate, @StartDate, @CompletedDate, @CompletedBy,
                    @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active
                );";

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding Activity with ID {entity.ActivityId}", ActivityId = entity.ActivityId, EntityType = nameof(Activity) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing activity in the repository
        /// </summary>
        /// <param name="entity">The activity to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated activity</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when ActivityId is empty</exception>
        public override async Task<Activity> UpdateAsync(Activity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.ActivityId == Guid.Empty) throw new ArgumentException("The Activity ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE Activity
                SET
                    ActivityTypeId = @ActivityTypeId,
                    ActivityStatusId = @ActivityStatusId,
                    AssignedUserId = @AssignedUserId,
                    AssignedTeamId = @AssignedTeamId,
                    Subject = @Subject,
                    Description = @Description,
                    DueDate = @DueDate,
                    StartDate = @StartDate,
                    CompletedDate = @CompletedDate,
                    CompletedBy = @CompletedBy,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate,
                    Active = @Active
                WHERE ActivityId = @ActivityId";

            var affectedRows = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating Activity with ID {entity.ActivityId}", ActivityId = entity.ActivityId, EntityType = nameof(Activity) },
                cancellationToken);

            if (affectedRows == 0)
            {
                _logger.LogWarning("No Activity was updated for ID {ActivityId}", entity.ActivityId);
            }

            return entity;
        }

        /// <summary>
        /// Gets activities by activity type
        /// </summary>
        /// <param name="activityTypeId">The activity type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activities with the specified activity type</returns>
        /// <exception cref="ArgumentException">Thrown when activityTypeId is empty</exception>
        public async Task<IEnumerable<Activity>> GetByActivityTypeAsync(Guid activityTypeId, CancellationToken cancellationToken = default)
        {
            if (activityTypeId == Guid.Empty) throw new ArgumentException("The Activity Type ID cannot be empty", nameof(activityTypeId));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE ActivityTypeId = @ActivityTypeId AND Active = 1
                ORDER BY CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Activity>>(
                async connection => await connection.QueryAsync<Activity>(
                    new CommandDefinition(
                        sql,
                        new { ActivityTypeId = activityTypeId },
                        cancellationToken: cancellationToken)),
                "GetByActivityTypeAsync",
                new { ErrorMessage = $"Error retrieving Activities with Activity Type ID {activityTypeId}", ActivityTypeId = activityTypeId, EntityType = nameof(Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets activities by activity status
        /// </summary>
        /// <param name="activityStatusId">The activity status identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activities with the specified activity status</returns>
        /// <exception cref="ArgumentException">Thrown when activityStatusId is empty</exception>
        public async Task<IEnumerable<Activity>> GetByActivityStatusAsync(Guid activityStatusId, CancellationToken cancellationToken = default)
        {
            if (activityStatusId == Guid.Empty) throw new ArgumentException("The Activity Status ID cannot be empty", nameof(activityStatusId));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE ActivityStatusId = @ActivityStatusId AND Active = 1
                ORDER BY CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Activity>>(
                async connection => await connection.QueryAsync<Activity>(
                    new CommandDefinition(
                        sql,
                        new { ActivityStatusId = activityStatusId },
                        cancellationToken: cancellationToken)),
                "GetByActivityStatusAsync",
                new { ErrorMessage = $"Error retrieving Activities with Activity Status ID {activityStatusId}", ActivityStatusId = activityStatusId, EntityType = nameof(Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets activities by assigned user
        /// </summary>
        /// <param name="assignedUserId">The assigned user identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activities assigned to the specified user</returns>
        /// <exception cref="ArgumentException">Thrown when assignedUserId is empty</exception>
        public async Task<IEnumerable<Activity>> GetByAssignedUserAsync(Guid assignedUserId, CancellationToken cancellationToken = default)
        {
            if (assignedUserId == Guid.Empty) throw new ArgumentException("The Assigned User ID cannot be empty", nameof(assignedUserId));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE AssignedUserId = @AssignedUserId AND Active = 1
                ORDER BY DueDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Activity>>(
                async connection => await connection.QueryAsync<Activity>(
                    new CommandDefinition(
                        sql,
                        new { AssignedUserId = assignedUserId },
                        cancellationToken: cancellationToken)),
                "GetByAssignedUserAsync",
                new { ErrorMessage = $"Error retrieving Activities assigned to user {assignedUserId}", AssignedUserId = assignedUserId, EntityType = nameof(Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets activities by assigned team
        /// </summary>
        /// <param name="assignedTeamId">The assigned team identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activities assigned to the specified team</returns>
        /// <exception cref="ArgumentException">Thrown when assignedTeamId is empty</exception>
        public async Task<IEnumerable<Activity>> GetByAssignedTeamAsync(Guid assignedTeamId, CancellationToken cancellationToken = default)
        {
            if (assignedTeamId == Guid.Empty) throw new ArgumentException("The Assigned Team ID cannot be empty", nameof(assignedTeamId));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE AssignedTeamId = @AssignedTeamId AND Active = 1
                ORDER BY DueDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Activity>>(
                async connection => await connection.QueryAsync<Activity>(
                    new CommandDefinition(
                        sql,
                        new { AssignedTeamId = assignedTeamId },
                        cancellationToken: cancellationToken)),
                "GetByAssignedTeamAsync",
                new { ErrorMessage = $"Error retrieving Activities assigned to team {assignedTeamId}", AssignedTeamId = assignedTeamId, EntityType = nameof(Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets activities associated with a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activities associated with the specified company</returns>
        /// <exception cref="ArgumentException">Thrown when companyId is empty</exception>
        public async Task<IEnumerable<Activity>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            if (companyId == Guid.Empty) throw new ArgumentException("The Company ID cannot be empty", nameof(companyId));

            const string sql = @"
                SELECT a.ActivityId, a.ActivityTypeId, a.ActivityStatusId, a.AssignedUserId, a.AssignedTeamId,
                       a.Subject, a.Description, a.DueDate, a.StartDate, a.CompletedDate, a.CompletedBy,
                       a.CreatedBy, a.CreatedDate, a.ModifiedBy, a.ModifiedDate, a.Active
                FROM Activity a
                INNER JOIN Company_Activity ca ON a.ActivityId = ca.ActivityId
                WHERE ca.CompanyId = @CompanyId AND a.Active = 1
                ORDER BY a.DueDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Activity>>(
                async connection => await connection.QueryAsync<Activity>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyAsync",
                new { ErrorMessage = $"Error retrieving Activities associated with Company ID {companyId}", CompanyId = companyId, EntityType = nameof(Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets activities associated with a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activities associated with the specified person</returns>
        /// <exception cref="ArgumentException">Thrown when personId is empty</exception>
        public async Task<IEnumerable<Activity>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            if (personId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(personId));

            const string sql = @"
                SELECT a.ActivityId, a.ActivityTypeId, a.ActivityStatusId, a.AssignedUserId, a.AssignedTeamId,
                       a.Subject, a.Description, a.DueDate, a.StartDate, a.CompletedDate, a.CompletedBy,
                       a.CreatedBy, a.CreatedDate, a.ModifiedBy, a.ModifiedDate, a.Active
                FROM Activity a
                INNER JOIN Person_Activity pa ON a.ActivityId = pa.ActivityId
                WHERE pa.PersonId = @PersonId AND a.Active = 1
                ORDER BY a.DueDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Activity>>(
                async connection => await connection.QueryAsync<Activity>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetByPersonAsync",
                new { ErrorMessage = $"Error retrieving Activities associated with Person ID {personId}", PersonId = personId, EntityType = nameof(Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets activities by due date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activities with due dates within the specified range</returns>
        /// <exception cref="ArgumentException">Thrown when startDate is later than endDate</exception>
        public async Task<IEnumerable<Activity>> GetByDueDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date cannot be later than end date", nameof(startDate));
            }

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE DueDate >= @StartDate AND DueDate <= @EndDate AND Active = 1
                ORDER BY DueDate ASC";

            var parameters = new
            {
                StartDate = startDate,
                EndDate = endDate
            };

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Activity>>(
                async connection => await connection.QueryAsync<Activity>(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "GetByDueDateRangeAsync",
                new { ErrorMessage = $"Error retrieving Activities with due dates between {startDate} and {endDate}", StartDate = startDate, EndDate = endDate, EntityType = nameof(Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets activities that were completed within a specified date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activities completed within the specified date range</returns>
        /// <exception cref="ArgumentException">Thrown when startDate is later than endDate</exception>
        public async Task<IEnumerable<Activity>> GetByCompletedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date cannot be later than end date", nameof(startDate));
            }

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE CompletedDate IS NOT NULL
                      AND CompletedDate >= @StartDate
                      AND CompletedDate <= @EndDate
                      AND Active = 1
                ORDER BY CompletedDate DESC";

            var parameters = new
            {
                StartDate = startDate,
                EndDate = endDate
            };

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Activity>>(
                async connection => await connection.QueryAsync<Activity>(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "GetByCompletedDateRangeAsync",
                new { ErrorMessage = $"Error retrieving Activities completed between {startDate} and {endDate}", StartDate = startDate, EndDate = endDate, EntityType = nameof(Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets activities by subject or description search
        /// </summary>
        /// <param name="searchTerm">The search term to filter by</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activities whose subject or description contain the search term</returns>
        /// <exception cref="ArgumentException">Thrown when searchTerm is null or empty</exception>
        public async Task<IEnumerable<Activity>> GetBySearchTermAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                throw new ArgumentException("Search term cannot be null or empty", nameof(searchTerm));
            }

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE (Subject LIKE @SearchPattern OR Description LIKE @SearchPattern) AND Active = 1
                ORDER BY CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Activity>>(
                async connection => await connection.QueryAsync<Activity>(
                    new CommandDefinition(
                        sql,
                        new { SearchPattern = $"%{searchTerm}%" },
                        cancellationToken: cancellationToken)),
                "GetBySearchTermAsync",
                new { ErrorMessage = $"Error searching Activities with term '{searchTerm}'", SearchTerm = searchTerm, EntityType = nameof(Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets completed activities.
        /// Only returns activities where Active = true (not soft-deleted).
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of completed activities.</returns>
        public async Task<IEnumerable<Activity>> GetCompletedActivitiesAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE CompletedDate IS NOT NULL AND Active = 1
                ORDER BY CompletedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Activity>>(
                async connection => await connection.QueryAsync<Activity>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetCompletedActivitiesAsync",
                new { ErrorMessage = "Error retrieving completed Activities", EntityType = nameof(Activity) },
                cancellationToken);
        }

        /// <summary>
        /// Gets incomplete activities.
        /// Only returns activities where Active = true (not soft-deleted).
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of incomplete activities.</returns>
        public async Task<IEnumerable<Activity>> GetIncompleteActivitiesAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE CompletedDate IS NULL AND Active = 1
                ORDER BY DueDate ASC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Activity>>(
                async connection => await connection.QueryAsync<Activity>(
                    new CommandDefinition(
                        sql,
                        cancellationToken: cancellationToken)),
                "GetIncompleteActivitiesAsync",
                new { ErrorMessage = "Error retrieving incomplete Activities", EntityType = nameof(Activity) },
                cancellationToken);
        }
    }
}