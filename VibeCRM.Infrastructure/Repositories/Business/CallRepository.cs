using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing Call entities
    /// </summary>
    public class CallRepository : BaseRepository<Call, Guid>, ICallRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public CallRepository(ISQLConnectionFactory connectionFactory, ILogger<CallRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Call";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "CallId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "CallId", "TypeId", "StatusId", "DirectionId", "Description", "Duration",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Adds a new call to the repository
        /// </summary>
        /// <param name="entity">The call entity to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added call with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when CallId is empty</exception>
        public override async Task<Call> AddAsync(Call entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.CallId == Guid.Empty) throw new ArgumentException("The Call ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO Call (
                    CallId, TypeId, StatusId, DirectionId, Description, Duration,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @CallId, @TypeId, @StatusId, @DirectionId, @Description, @Duration,
                    @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active
                );";

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(sql, entity),
                "AddAsync",
                new { CallId = entity.CallId, EntityType = nameof(Call) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing call in the repository
        /// </summary>
        /// <param name="entity">The call to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated call</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when CallId is empty</exception>
        public override async Task<Call> UpdateAsync(Call entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.CallId == Guid.Empty) throw new ArgumentException("The Call ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE Call
                SET
                    TypeId = @TypeId,
                    StatusId = @StatusId,
                    DirectionId = @DirectionId,
                    Description = @Description,
                    Duration = @Duration,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate,
                    Active = @Active
                WHERE CallId = @CallId";

            var affectedRows = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(sql, entity),
                "UpdateAsync",
                new { CallId = entity.CallId, EntityType = nameof(Call) },
                cancellationToken);

            if (affectedRows == 0)
            {
                _logger.LogWarning("No Call was updated for ID {CallId}", entity.CallId);
            }

            return entity;
        }

        /// <summary>
        /// Deletes a call by its unique identifier (soft delete)
        /// </summary>
        /// <param name="id">The unique identifier of the call to delete</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the call was successfully deleted, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Call ID cannot be empty", nameof(id));

            // Use the BaseRepository soft delete pattern
            return await base.DeleteAsync(id, cancellationToken);
        }

        /// <summary>
        /// Checks if a call with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a call with the specified ID exists, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Call ID cannot be empty", nameof(id));

            const string sql = "SELECT COUNT(1) FROM Call WHERE CallId = @CallId AND Active = 1";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(sql, new { CallId = id }),
                "ExistsAsync",
                new { CallId = id, EntityType = nameof(Call) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Gets all calls from the repository
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of all calls in the repository</returns>
        public override async Task<IEnumerable<Call>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1
                ORDER BY CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Call>(sql),
                "GetAllAsync",
                new { EntityType = nameof(Call) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a call by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the call</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The call if found, otherwise null</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<Call?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Call ID cannot be empty", nameof(id));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE CallId = @CallId AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QuerySingleOrDefaultAsync<Call>(sql, new { CallId = id }),
                "GetByIdAsync",
                new { CallId = id, EntityType = nameof(Call) },
                cancellationToken);
        }

        /// <summary>
        /// Gets calls by their type
        /// </summary>
        /// <param name="callTypeId">The call type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of calls with the specified type</returns>
        /// <exception cref="ArgumentException">Thrown when callTypeId is empty</exception>
        public async Task<IEnumerable<Call>> GetByCallTypeAsync(Guid callTypeId, CancellationToken cancellationToken = default)
        {
            if (callTypeId == Guid.Empty) throw new ArgumentException("The Call Type ID cannot be empty", nameof(callTypeId));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE TypeId = @TypeId AND Active = 1
                ORDER BY CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Call>(sql, new { TypeId = callTypeId }),
                "GetByCallTypeAsync",
                new { CallTypeId = callTypeId, EntityType = nameof(Call) },
                cancellationToken);
        }

        /// <summary>
        /// Gets calls for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of calls associated with the specified company</returns>
        /// <exception cref="ArgumentException">Thrown when companyId is empty</exception>
        public async Task<IEnumerable<Call>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            if (companyId == Guid.Empty) throw new ArgumentException("The Company ID cannot be empty", nameof(companyId));

            const string sql = @"
                SELECT c.CallId, c.TypeId, c.StatusId, c.DirectionId, c.Description, c.Duration,
                       c.CreatedBy, c.CreatedDate, c.ModifiedBy, c.ModifiedDate, c.Active
                FROM Call c
                INNER JOIN Company_Call cc ON c.CallId = cc.CallId
                WHERE cc.CompanyId = @CompanyId AND c.Active = 1
                ORDER BY c.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Call>(sql, new { CompanyId = companyId }),
                "GetByCompanyAsync",
                new { CompanyId = companyId, EntityType = nameof(Call) },
                cancellationToken);
        }

        /// <summary>
        /// Gets calls for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of calls associated with the specified person</returns>
        /// <exception cref="ArgumentException">Thrown when personId is empty</exception>
        public async Task<IEnumerable<Call>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            if (personId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(personId));

            const string sql = @"
                SELECT c.CallId, c.TypeId, c.StatusId, c.DirectionId, c.Description, c.Duration,
                       c.CreatedBy, c.CreatedDate, c.ModifiedBy, c.ModifiedDate, c.Active
                FROM Call c
                INNER JOIN Person_Call pc ON c.CallId = pc.CallId
                WHERE pc.PersonId = @PersonId AND c.Active = 1
                ORDER BY c.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Call>(sql, new { PersonId = personId }),
                "GetByPersonAsync",
                new { PersonId = personId, EntityType = nameof(Call) },
                cancellationToken);
        }

        /// <summary>
        /// Gets calls by duration range
        /// </summary>
        /// <param name="minDuration">The minimum duration in seconds</param>
        /// <param name="maxDuration">The maximum duration in seconds</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of calls with a duration within the specified range</returns>
        public async Task<IEnumerable<Call>> GetByDurationRangeAsync(int minDuration, int maxDuration, CancellationToken cancellationToken = default)
        {
            if (minDuration < 0) throw new ArgumentException("Minimum duration cannot be negative", nameof(minDuration));
            if (maxDuration < minDuration) throw new ArgumentException("Maximum duration must be greater than or equal to minimum duration", nameof(maxDuration));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Duration >= @MinDuration AND Duration <= @MaxDuration AND Active = 1
                ORDER BY Duration ASC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Call>(sql, new { MinDuration = minDuration, MaxDuration = maxDuration }),
                "GetByDurationRangeAsync",
                new { MinDuration = minDuration, MaxDuration = maxDuration, EntityType = nameof(Call) },
                cancellationToken);
        }

        /// <summary>
        /// Gets calls by schedule date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of calls scheduled within the specified date range</returns>
        public async Task<IEnumerable<Call>> GetByScheduleDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            if (endDate < startDate) throw new ArgumentException("End date must be greater than or equal to start date", nameof(endDate));

            const string sql = @"
                SELECT c.CallId, c.TypeId, c.StatusId, c.DirectionId, c.Description, c.Duration,
                       c.CreatedBy, c.CreatedDate, c.ModifiedBy, c.ModifiedDate, c.Active
                FROM Call c
                INNER JOIN Activity a ON c.CallId = a.CallId
                WHERE a.ScheduledStart >= @StartDate AND a.ScheduledStart <= @EndDate AND c.Active = 1
                ORDER BY a.ScheduledStart ASC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Call>(sql, new { StartDate = startDate, EndDate = endDate }),
                "GetByScheduleDateRangeAsync",
                new { StartDate = startDate, EndDate = endDate, EntityType = nameof(Call) },
                cancellationToken);
        }

        /// <summary>
        /// Gets calls by completion date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of calls completed within the specified date range</returns>
        public async Task<IEnumerable<Call>> GetByCompletionDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            if (endDate < startDate) throw new ArgumentException("End date must be greater than or equal to start date", nameof(endDate));

            const string sql = @"
                SELECT c.CallId, c.TypeId, c.StatusId, c.DirectionId, c.Description, c.Duration,
                       c.CreatedBy, c.CreatedDate, c.ModifiedBy, c.ModifiedDate, c.Active
                FROM Call c
                INNER JOIN Activity a ON c.CallId = a.CallId
                WHERE a.CompletedDate >= @StartDate AND a.CompletedDate <= @EndDate AND c.Active = 1
                ORDER BY a.CompletedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Call>(sql, new { StartDate = startDate, EndDate = endDate }),
                "GetByCompletionDateRangeAsync",
                new { StartDate = startDate, EndDate = endDate, EntityType = nameof(Call) },
                cancellationToken);
        }
    }
}