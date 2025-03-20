using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing Company entities
    /// </summary>
    public class CompanyRepository : BaseRepository<Company, Guid>, ICompanyRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Company";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "CompanyId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "CompanyId", "ParentCompanyId", "Name", "Description", "AccountTypeId", "AccountStatusId",
            "PrimaryContactId", "PrimaryPhoneId", "PrimaryAddressId", "Website",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public CompanyRepository(ISQLConnectionFactory connectionFactory, ILogger<CompanyRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new company to the repository
        /// </summary>
        /// <param name="entity">The company entity to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added company with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<Company> AddAsync(Company entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.CompanyId == Guid.Empty) throw new ArgumentException("The Company ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO Company (
                    CompanyId, ParentCompanyId, Name, Description, AccountTypeId, AccountStatusId,
                    PrimaryContactId, PrimaryPhoneId, PrimaryAddressId, Website,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @CompanyId, @ParentCompanyId, @Name, @Description, @AccountTypeId, @AccountStatusId,
                    @PrimaryContactId, @PrimaryPhoneId, @PrimaryAddressId, @Website,
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
                new { ErrorMessage = $"Error adding Company with ID {entity.CompanyId} and Name {entity.Name}", CompanyId = entity.CompanyId, EntityType = nameof(Company) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing company in the repository
        /// </summary>
        /// <param name="entity">The company entity to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated company entity</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when CompanyId is empty</exception>
        public override async Task<Company> UpdateAsync(Company entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.CompanyId == Guid.Empty) throw new ArgumentException("The Company ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE Company
                SET ParentCompanyId = @ParentCompanyId,
                    Name = @Name,
                    Description = @Description,
                    AccountTypeId = @AccountTypeId,
                    AccountStatusId = @AccountStatusId,
                    PrimaryContactId = @PrimaryContactId,
                    PrimaryPhoneId = @PrimaryPhoneId,
                    PrimaryAddressId = @PrimaryAddressId,
                    Website = @Website,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate,
                    Active = @Active
                WHERE CompanyId = @CompanyId;";

            var affectedRows = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating Company with ID {entity.CompanyId} and Name {entity.Name}", CompanyId = entity.CompanyId, EntityType = nameof(Company) },
                cancellationToken);

            if (affectedRows == 0)
            {
                _logger.LogWarning("No Company was updated for ID {CompanyId} and Name {Name}", entity.CompanyId, entity.Name);
            }

            return entity;
        }

        /// <summary>
        /// Gets companies by their type
        /// </summary>
        /// <param name="accountTypeId">The account type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies with the specified type</returns>
        /// <exception cref="ArgumentException">Thrown when accountTypeId is empty</exception>
        public async Task<IEnumerable<Company>> GetByAccountTypeAsync(Guid accountTypeId, CancellationToken cancellationToken = default)
        {
            if (accountTypeId == Guid.Empty) throw new ArgumentException("The Account Type ID cannot be empty", nameof(accountTypeId));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE AccountTypeId = @AccountTypeId AND Active = 1
                ORDER BY Name";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company>>(
                async connection =>
                    await connection.QueryAsync<Company>(
                        new CommandDefinition(
                            sql,
                            new { AccountTypeId = accountTypeId },
                            cancellationToken: cancellationToken)),
                "GetByAccountTypeAsync",
                new { ErrorMessage = $"Error retrieving Companies with Account Type ID {accountTypeId}", AccountTypeId = accountTypeId, EntityType = nameof(Company) },
                cancellationToken);
        }

        /// <summary>
        /// Gets companies by their status
        /// </summary>
        /// <param name="accountStatusId">The account status identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies with the specified status</returns>
        /// <exception cref="ArgumentException">Thrown when accountStatusId is empty</exception>
        public async Task<IEnumerable<Company>> GetByAccountStatusAsync(Guid accountStatusId, CancellationToken cancellationToken = default)
        {
            if (accountStatusId == Guid.Empty) throw new ArgumentException("The Account Status ID cannot be empty", nameof(accountStatusId));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE AccountStatusId = @AccountStatusId AND Active = 1
                ORDER BY Name";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company>>(
                async connection =>
                    await connection.QueryAsync<Company>(
                        new CommandDefinition(
                            sql,
                            new { AccountStatusId = accountStatusId },
                            cancellationToken: cancellationToken)),
                "GetByAccountStatusAsync",
                new { ErrorMessage = $"Error retrieving Companies with Account Status ID {accountStatusId}", AccountStatusId = accountStatusId, EntityType = nameof(Company) },
                cancellationToken);
        }

        /// <summary>
        /// Gets companies that have a specific person associated with them
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies associated with the specified person</returns>
        /// <exception cref="ArgumentException">Thrown when personId is empty</exception>
        public async Task<IEnumerable<Company>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            if (personId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(personId));

            var sql = $@"
                SELECT c.{string.Join(", ", SelectColumns)}
                FROM {TableName} c
                INNER JOIN Company_Person cp ON c.CompanyId = cp.CompanyId
                WHERE cp.PersonId = @PersonId AND c.Active = 1
                ORDER BY c.Name";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company>>(
                async connection =>
                    await connection.QueryAsync<Company>(
                        new CommandDefinition(
                            sql,
                            new { PersonId = personId },
                            cancellationToken: cancellationToken)),
                "GetByPersonAsync",
                new { ErrorMessage = $"Error retrieving Companies associated with Person ID {personId}", PersonId = personId, EntityType = nameof(Company) },
                cancellationToken);
        }

        /// <summary>
        /// Searches companies by name
        /// </summary>
        /// <param name="searchName">The name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies matching the search criteria</returns>
        public async Task<IEnumerable<Company>> SearchByNameAsync(string searchName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(searchName))
                return await GetAllAsync(cancellationToken);

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Name LIKE @SearchName AND Active = 1
                ORDER BY Name";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company>>(
                async connection =>
                    await connection.QueryAsync<Company>(
                        new CommandDefinition(
                            sql,
                            new { SearchName = $"%{searchName}%" },
                            cancellationToken: cancellationToken)),
                "SearchByNameAsync",
                new { ErrorMessage = $"Error searching Companies with name containing '{searchName}'", SearchName = searchName, EntityType = nameof(Company) },
                cancellationToken);
        }

        /// <summary>
        /// Gets companies by website
        /// </summary>
        /// <param name="website">The website to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies with the specified website</returns>
        /// <exception cref="ArgumentException">Thrown when website is null or empty</exception>
        public async Task<IEnumerable<Company>> GetByWebsiteAsync(string website, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(website))
                throw new ArgumentException("Website cannot be null or empty", nameof(website));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Website LIKE @Website AND Active = 1
                ORDER BY Name";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company>>(
                async connection =>
                    await connection.QueryAsync<Company>(
                        new CommandDefinition(
                            sql,
                            new { Website = $"%{website}%" },
                            cancellationToken: cancellationToken)),
                "GetByWebsiteAsync",
                new { ErrorMessage = $"Error retrieving Companies with website containing '{website}'", Website = website, EntityType = nameof(Company) },
                cancellationToken);
        }

        /// <summary>
        /// Gets companies by industry
        /// </summary>
        /// <param name="industry">The industry to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies in the specified industry</returns>
        /// <exception cref="ArgumentException">Thrown when industry is null or empty</exception>
        public async Task<IEnumerable<Company>> GetByIndustryAsync(string industry, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(industry))
                throw new ArgumentException("Industry cannot be null or empty", nameof(industry));

            // Assuming industry is stored in the Description field or a custom property
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Description LIKE @Industry AND Active = 1
                ORDER BY Name";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company>>(
                async connection =>
                    await connection.QueryAsync<Company>(
                        new CommandDefinition(
                            sql,
                            new { Industry = $"%{industry}%" },
                            cancellationToken: cancellationToken)),
                "GetByIndustryAsync",
                new { ErrorMessage = $"Error retrieving Companies in industry containing '{industry}'", Industry = industry, EntityType = nameof(Company) },
                cancellationToken);
        }

        /// <summary>
        /// Gets companies created within a date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies created within the specified date range</returns>
        /// <exception cref="ArgumentException">Thrown when startDate is later than endDate</exception>
        public async Task<IEnumerable<Company>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date cannot be later than end date", nameof(startDate));
            }

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE CreatedDate BETWEEN @StartDate AND @EndDate AND Active = 1
                ORDER BY CreatedDate";

            var parameters = new
            {
                StartDate = startDate,
                EndDate = endDate
            };

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company>>(
                async connection =>
                    await connection.QueryAsync<Company>(
                        new CommandDefinition(
                            sql,
                            parameters,
                            cancellationToken: cancellationToken)),
                "GetByCreatedDateRangeAsync",
                new { ErrorMessage = $"Error retrieving Companies created between {startDate} and {endDate}", StartDate = startDate, EndDate = endDate, EntityType = nameof(Company) },
                cancellationToken);
        }

        /// <summary>
        /// Gets companies by annual revenue range
        /// </summary>
        /// <param name="minRevenue">The minimum annual revenue</param>
        /// <param name="maxRevenue">The maximum annual revenue</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of companies with annual revenue in the specified range</returns>
        /// <exception cref="ArgumentException">Thrown when minRevenue is greater than maxRevenue</exception>
        public async Task<IEnumerable<Company>> GetByAnnualRevenueRangeAsync(decimal minRevenue, decimal maxRevenue, CancellationToken cancellationToken = default)
        {
            if (minRevenue > maxRevenue)
            {
                throw new ArgumentException("Minimum revenue cannot be greater than maximum revenue", nameof(minRevenue));
            }

            // Assuming annual revenue is stored in a custom property table or a specific field
            var sql = $@"
                SELECT c.{string.Join(", ", SelectColumns)}
                FROM {TableName} c
                INNER JOIN CompanyProperty cp ON c.CompanyId = cp.CompanyId
                WHERE cp.PropertyName = 'AnnualRevenue'
                AND CAST(cp.PropertyValue AS DECIMAL) BETWEEN @MinRevenue AND @MaxRevenue
                AND c.Active = 1
                ORDER BY c.Name";

            var parameters = new
            {
                MinRevenue = minRevenue,
                MaxRevenue = maxRevenue
            };

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company>>(
                async connection =>
                    await connection.QueryAsync<Company>(
                        new CommandDefinition(
                            sql,
                            parameters,
                            cancellationToken: cancellationToken)),
                "GetByAnnualRevenueRangeAsync",
                new { ErrorMessage = $"Error retrieving Companies with annual revenue between {minRevenue} and {maxRevenue}", MinRevenue = minRevenue, MaxRevenue = maxRevenue, EntityType = nameof(Company) },
                cancellationToken);
        }
    }
}