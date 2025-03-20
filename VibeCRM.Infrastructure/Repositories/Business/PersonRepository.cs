using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing Person entities
    /// </summary>
    public class PersonRepository : BaseRepository<Person, Guid>, IPersonRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Person";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "PersonId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "PersonId", "Firstname", "MiddleInitial", "Lastname", "Title",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        private readonly string[] _columnNames = {
            "PersonId", "Firstname", "MiddleInitial", "Lastname", "Title",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        private new readonly ILogger<PersonRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepository"/> class.
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public PersonRepository(ISQLConnectionFactory connectionFactory, ILogger<PersonRepository> logger)
            : base(connectionFactory, logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets all persons from the repository
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of all persons in the repository</returns>
        public override async Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT
                    p.PersonId, p.Firstname, p.MiddleInitial, p.Lastname, p.Title,
                    p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Person p
                WHERE p.Active = 1
                ORDER BY p.Lastname, p.Firstname";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Person>>(
                async connection =>
                    await connection.QueryAsync<Person>(
                        new CommandDefinition(
                            sql,
                            cancellationToken: cancellationToken)),
                "GetAllAsync",
                new { ErrorMessage = "Error retrieving all Persons", EntityType = nameof(Person) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a person by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the person</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person if found, otherwise null</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(id));

            const string sql = @"
                SELECT
                    p.PersonId, p.Firstname, p.MiddleInitial, p.Lastname, p.Title,
                    p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Person p
                WHERE p.PersonId = @Id AND p.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<Person?>(
                async connection =>
                    await connection.QueryFirstOrDefaultAsync<Person>(
                        new CommandDefinition(
                            sql,
                            new { Id = id },
                            cancellationToken: cancellationToken)),
                "GetByIdAsync",
                new { Id = id, ErrorMessage = $"Error retrieving Person with ID {id}", PersonId = id, EntityType = nameof(Person) },
                cancellationToken);
        }

        /// <summary>
        /// Adds a new person to the repository
        /// </summary>
        /// <param name="entity">The person to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added person with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when PersonId is empty</exception>
        public override async Task<Person> AddAsync(Person entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.PersonId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(entity));

            try
            {
                _logger.LogInformation("Adding person with ID {PersonId} to database", entity.PersonId);

                const string query = @"
                    INSERT INTO Person (PersonId, Firstname, MiddleInitial, Lastname, Title,
                                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active)
                    VALUES (@PersonId, @Firstname, @MiddleInitial, @Lastname, @Title,
                            @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active);
                    SELECT
                        PersonId, Firstname, MiddleInitial, Lastname, Title,
                        CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                    FROM Person
                    WHERE PersonId = @PersonId";

                return await ExecuteWithResilienceAndLoggingAsync<Person>(
                    async connection =>
                        await connection.QuerySingleAsync<Person>(
                            new CommandDefinition(
                                query,
                                entity,
                                cancellationToken: cancellationToken)),
                    "AddAsync",
                    new { entity.PersonId, ErrorMessage = "Error adding Person" },
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding person with ID {PersonId}", entity.PersonId);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing person in the repository
        /// </summary>
        /// <param name="entity">The person to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated person</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when PersonId is empty</exception>
        public override async Task<Person> UpdateAsync(Person entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.PersonId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(entity));

            try
            {
                _logger.LogInformation("Updating person with ID {PersonId} in database", entity.PersonId);

                const string query = @"
                    UPDATE Person
                    SET Firstname = @Firstname,
                        MiddleInitial = @MiddleInitial,
                        Lastname = @Lastname,
                        Title = @Title,
                        ModifiedBy = @ModifiedBy,
                        ModifiedDate = @ModifiedDate
                    WHERE PersonId = @PersonId;

                    SELECT
                        PersonId, Firstname, MiddleInitial, Lastname, Title,
                        CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                    FROM Person
                    WHERE PersonId = @PersonId";

                return await ExecuteWithResilienceAndLoggingAsync<Person>(
                    async connection =>
                        await connection.QuerySingleAsync<Person>(
                            new CommandDefinition(
                                query,
                                entity,
                                cancellationToken: cancellationToken)),
                    "UpdateAsync",
                    new { entity.PersonId, ErrorMessage = "Error updating Person" },
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating person with ID {PersonId}", entity.PersonId);
                throw;
            }
        }

        /// <summary>
        /// Deletes a person by their unique identifier (soft delete)
        /// </summary>
        /// <param name="id">The unique identifier of the person to delete</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the person was successfully deleted, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(id));

            try
            {
                const string sql = @"
                    UPDATE Person
                    SET Active = 0,
                        ModifiedBy = @ModifiedBy,
                        ModifiedDate = @ModifiedDate
                    WHERE PersonId = @Id;
                    SELECT @@ROWCOUNT";

                // Use the current username or system as the modifier
                string modifiedBy = "SYSTEM"; // Replace with actual user if available
                DateTime modifiedDate = DateTime.UtcNow;

                var parameters = new { Id = id, ModifiedBy = modifiedBy, ModifiedDate = modifiedDate };

                var rowsAffected = await ExecuteWithResilienceAndLoggingAsync<int>(
                    async connection =>
                        await connection.ExecuteScalarAsync<int>(
                            new CommandDefinition(
                                sql,
                                parameters,
                                cancellationToken: cancellationToken)),
                    "DeleteAsync",
                    new { Id = id, ErrorMessage = "Error deleting Person" },
                    cancellationToken);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting person with ID {PersonId}", id);
                throw;
            }
        }

        /// <summary>
        /// Checks if a person with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a person with the specified ID exists, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(id));

            const string sql = "SELECT COUNT(1) FROM Person WHERE PersonId = @Id AND Active = 1";
            var parameters = new { Id = id };

            var count = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            parameters,
                            cancellationToken: cancellationToken)),
                "ExistsAsync",
                new { Id = id, ErrorMessage = "Error checking Person existence" },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Gets persons by their type
        /// </summary>
        /// <param name="personTypeId">The person type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons with the specified type</returns>
        public async Task<IEnumerable<Person>> GetByPersonTypeAsync(Guid personTypeId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting persons by person type {PersonTypeId}", personTypeId);

            const string sql = @"
                SELECT
                    p.PersonId, p.Firstname, p.MiddleInitial, p.Lastname, p.Title,
                    p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Person p
                -- Join to a person type table would go here if it existed
                --INNER JOIN PersonType pt ON p.PersonTypeId = pt.PersonTypeId
                WHERE p.Active = 1
                -- AND p.PersonTypeId = @PersonTypeId  -- Uncomment when PersonTypeId is added to the Person entity
                ORDER BY p.Lastname, p.Firstname";

            var parameters = new { PersonTypeId = personTypeId };

            // For now, this just returns all persons since Person doesn't have a PersonTypeId property
            _logger.LogWarning("GetByPersonTypeAsync called but Person entity doesn't have a PersonTypeId property");

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Person>>(
                async connection => await connection.QueryAsync<Person>(sql, parameters),
                "GetByPersonTypeAsync",
                new { PersonTypeId = personTypeId, ErrorMessage = "Error getting Persons by type" },
                cancellationToken);
        }

        /// <summary>
        /// Gets persons by their status
        /// </summary>
        /// <param name="personStatusId">The person status identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons with the specified status</returns>
        public async Task<IEnumerable<Person>> GetByPersonStatusAsync(Guid personStatusId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT
                    p.PersonId, p.Firstname, p.MiddleInitial, p.Lastname, p.Title,
                    p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Person p
                INNER JOIN Person_PersonStatus pps ON p.PersonId = pps.PersonId
                WHERE pps.PersonStatusId = @PersonStatusId AND p.Active = 1 AND pps.Active = 1
                ORDER BY p.Lastname, p.Firstname";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Person>>(
                async connection =>
                    await connection.QueryAsync<Person>(
                        new CommandDefinition(
                            sql,
                            new { PersonStatusId = personStatusId },
                            cancellationToken: cancellationToken)),
                "GetByPersonStatusAsync",
                new { PersonStatusId = personStatusId, ErrorMessage = "Error getting Persons by status" },
                cancellationToken);
        }

        /// <summary>
        /// Gets persons associated with a company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons associated with the specified company</returns>
        public async Task<IEnumerable<Person>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT
                    p.PersonId, p.Firstname, p.MiddleInitial, p.Lastname, p.Title,
                    p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Person p
                INNER JOIN Company_Person cp ON p.PersonId = cp.PersonId
                WHERE cp.CompanyId = @CompanyId AND p.Active = 1 AND cp.Active = 1
                ORDER BY p.Lastname, p.Firstname";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Person>>(
                async connection =>
                    await connection.QueryAsync<Person>(
                        new CommandDefinition(
                            sql,
                            new { CompanyId = companyId },
                            cancellationToken: cancellationToken)),
                "GetByCompanyAsync",
                new { CompanyId = companyId, ErrorMessage = "Error getting Persons by company" },
                cancellationToken);
        }

        /// <summary>
        /// Gets persons by name (full or partial)
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons with names matching the search criteria</returns>
        public async Task<IEnumerable<Person>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty", nameof(name));

            const string sql = @"
                SELECT
                    p.PersonId, p.Firstname, p.MiddleInitial, p.Lastname, p.Title,
                    p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Person p
                WHERE (p.Firstname LIKE @SearchTerm OR p.Lastname LIKE @SearchTerm) AND p.Active = 1
                ORDER BY p.Lastname, p.Firstname";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Person>>(
                async connection =>
                    await connection.QueryAsync<Person>(
                        new CommandDefinition(
                            sql,
                            new { SearchTerm = $"%{name}%" },
                            cancellationToken: cancellationToken)),
                "GetByNameAsync",
                new { Name = name, ErrorMessage = "Error getting Persons by name" },
                cancellationToken);
        }

        /// <summary>
        /// Gets persons by email address
        /// </summary>
        /// <param name="email">The email address to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons with the specified email address</returns>
        public async Task<IEnumerable<Person>> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty", nameof(email));

            const string sql = @"
                SELECT
                    p.PersonId, p.Firstname, p.MiddleInitial, p.Lastname, p.Title,
                    p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Person p
                INNER JOIN Person_EmailAddress pe ON p.PersonId = pe.PersonId
                INNER JOIN EmailAddress e ON pe.EmailAddressId = e.EmailAddressId
                WHERE e.EmailAddress = @Email AND p.Active = 1 AND pe.Active = 1 AND e.Active = 1
                ORDER BY p.Lastname, p.Firstname";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Person>>(
                async connection =>
                    await connection.QueryAsync<Person>(
                        new CommandDefinition(
                            sql,
                            new { Email = email },
                            cancellationToken: cancellationToken)),
                "GetByEmailAsync",
                new { Email = email, ErrorMessage = "Error getting Persons by email" },
                cancellationToken);
        }

        /// <summary>
        /// Gets persons by phone number
        /// </summary>
        /// <param name="phoneNumber">The phone number to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons with the specified phone number</returns>
        public async Task<IEnumerable<Person>> GetByPhoneAsync(string phoneNumber, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number cannot be null or empty", nameof(phoneNumber));

            // Normalize the phone number for search
            phoneNumber = phoneNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

            if (phoneNumber.Length < 10)
                throw new ArgumentException("Phone number must be at least 10 digits", nameof(phoneNumber));

            int areaCode = int.Parse(phoneNumber.Substring(0, 3));
            int prefix = int.Parse(phoneNumber.Substring(3, 3));
            int lineNumber = int.Parse(phoneNumber.Substring(6, 4));

            const string sql = @"
                SELECT
                    p.PersonId, p.Firstname, p.MiddleInitial, p.Lastname, p.Title,
                    p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Person p
                INNER JOIN Person_Phone pp ON p.PersonId = pp.PersonId
                INNER JOIN Phone ph ON pp.PhoneId = ph.PhoneId
                WHERE ph.AreaCode = @AreaCode AND ph.Prefix = @Prefix AND ph.LineNumber = @LineNumber
                AND p.Active = 1 AND pp.Active = 1 AND ph.Active = 1
                ORDER BY p.Lastname, p.Firstname";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Person>>(
                async connection =>
                    await connection.QueryAsync<Person>(
                        new CommandDefinition(
                            sql,
                            new { AreaCode = areaCode, Prefix = prefix, LineNumber = lineNumber },
                            cancellationToken: cancellationToken)),
                "GetByPhoneAsync",
                new { PhoneNumber = phoneNumber, ErrorMessage = "Error getting Persons by phone" },
                cancellationToken);
        }

        /// <summary>
        /// Gets persons created after a specified date
        /// </summary>
        /// <param name="date">The date to filter by</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons created after the specified date</returns>
        public async Task<IEnumerable<Person>> GetByCreatedDateAfterAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT
                    p.PersonId, p.Firstname, p.MiddleInitial, p.Lastname, p.Title,
                    p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Person p
                WHERE p.CreatedDate > @CreatedDate AND p.Active = 1
                ORDER BY p.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Person>>(
                async connection =>
                    await connection.QueryAsync<Person>(
                        new CommandDefinition(
                            sql,
                            new { CreatedDate = date },
                            cancellationToken: cancellationToken)),
                "GetByCreatedDateAfterAsync",
                new { CreatedDate = date, ErrorMessage = "Error getting Persons by created date" },
                cancellationToken);
        }

        /// <summary>
        /// Gets persons with a specific job title
        /// </summary>
        /// <param name="jobTitle">The job title to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons with the specified job title</returns>
        public async Task<IEnumerable<Person>> GetByJobTitleAsync(string jobTitle, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(jobTitle))
                throw new ArgumentException("Job title cannot be null or empty", nameof(jobTitle));

            const string sql = @"
                SELECT
                    p.PersonId, p.Firstname, p.MiddleInitial, p.Lastname, p.Title,
                    p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Person p
                WHERE p.Title LIKE @JobTitle AND p.Active = 1
                ORDER BY p.Lastname, p.Firstname";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Person>>(
                async connection =>
                    await connection.QueryAsync<Person>(
                        new CommandDefinition(
                            sql,
                            new { JobTitle = $"%{jobTitle}%" },
                            cancellationToken: cancellationToken)),
                "GetByJobTitleAsync",
                new { JobTitle = jobTitle, ErrorMessage = "Error getting Persons by job title" },
                cancellationToken);
        }

        /// <summary>
        /// Searches persons by name
        /// </summary>
        /// <param name="searchName">The name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons matching the search criteria</returns>
        /// <exception cref="ArgumentException">Thrown when searchName is null or empty</exception>
        public async Task<IEnumerable<Person>> SearchByNameAsync(string searchName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(searchName))
                throw new ArgumentException("Search name cannot be null or empty", nameof(searchName));

            // Split the search term to handle searching by first name and/or last name
            string[] searchTerms = searchName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string sql;
            object parameters;

            if (searchTerms.Length == 1)
            {
                // Single search term - could be first name or last name
                sql = @"
                    SELECT
                        p.PersonId, p.Firstname, p.MiddleInitial, p.Lastname, p.Title,
                        p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                    FROM Person p
                    WHERE (p.Firstname LIKE @SearchTerm OR p.Lastname LIKE @SearchTerm)
                        AND p.Active = 1
                    ORDER BY p.Lastname, p.Firstname";

                parameters = new { SearchTerm = $"%{searchTerms[0]}%" };
            }
            else
            {
                // Multiple search terms - likely first name and last name
                sql = @"
                    SELECT
                        p.PersonId, p.Firstname, p.MiddleInitial, p.Lastname, p.Title,
                        p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                    FROM Person p
                    WHERE (
                        (p.Firstname LIKE @FirstTerm AND p.Lastname LIKE @LastTerm) OR
                        (p.Firstname LIKE @LastTerm AND p.Lastname LIKE @FirstTerm)
                    ) AND p.Active = 1
                    ORDER BY p.Lastname, p.Firstname";

                parameters = new
                {
                    FirstTerm = $"%{searchTerms[0]}%",
                    LastTerm = $"%{searchTerms[searchTerms.Length - 1]}%"
                };
            }

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Person>>(
                async connection => await connection.QueryAsync<Person>(sql, parameters),
                "SearchByNameAsync",
                new { ErrorMessage = $"Error searching persons by name {searchName}", SearchName = searchName, SearchTerms = searchTerms, EntityType = nameof(Person) },
                cancellationToken);
        }

        /// <summary>
        /// Gets persons by phone number
        /// </summary>
        /// <param name="phoneNumber">The phone number to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons with the specified phone number</returns>
        /// <exception cref="ArgumentException">Thrown when phoneNumber is null or empty</exception>
        public async Task<IEnumerable<Person>> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number cannot be null or empty", nameof(phoneNumber));

            // Normalize the phone number for search - remove all non-numeric characters
            string normalizedNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if (normalizedNumber.Length < 7)
                throw new ArgumentException("Phone number must contain at least 7 digits", nameof(phoneNumber));

            const string sql = @"
                SELECT
                    p.PersonId, p.Firstname, p.MiddleInitial, p.Lastname, p.Title,
                    p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Person p
                INNER JOIN Person_Phone pp ON p.PersonId = pp.PersonId
                INNER JOIN Phone ph ON pp.PhoneId = ph.PhoneId
                WHERE (ph.AreaCode + ph.Prefix + ph.LineNumber) LIKE @PhoneNumber
                AND p.Active = 1 AND pp.Active = 1 AND ph.Active = 1
                ORDER BY p.Lastname, p.Firstname";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Person>>(
                async connection => await connection.QueryAsync<Person>(
                    sql, new { PhoneNumber = $"%{normalizedNumber}%" }),
                "GetByPhoneNumberAsync",
                new { ErrorMessage = $"Error retrieving persons by phone {phoneNumber}", PhoneNumber = phoneNumber, NormalizedNumber = normalizedNumber, EntityType = nameof(Person) },
                cancellationToken);
        }

        /// <summary>
        /// Gets persons created within a date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of persons created within the specified date range</returns>
        /// <exception cref="ArgumentException">Thrown when endDate is earlier than startDate</exception>
        public async Task<IEnumerable<Person>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            if (endDate < startDate)
                throw new ArgumentException("End date cannot be earlier than start date", nameof(endDate));

            // Ensure the end date includes the entire day
            DateTime adjustedEndDate = endDate.Date.AddDays(1).AddSeconds(-1);

            const string sql = @"
                SELECT
                    p.PersonId, p.Firstname, p.MiddleInitial, p.Lastname, p.Title,
                    p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Person p
                WHERE p.CreatedDate >= @StartDate
                AND p.CreatedDate <= @EndDate
                AND p.Active = 1
                ORDER BY p.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Person>>(
                async connection => await connection.QueryAsync<Person>(
                    sql, new { StartDate = startDate, EndDate = adjustedEndDate }),
                "GetByCreatedDateRangeAsync",
                new { ErrorMessage = $"Error retrieving persons created within date range {startDate} - {endDate}", StartDate = startDate, EndDate = endDate, AdjustedEndDate = adjustedEndDate, EntityType = nameof(Person) },
                cancellationToken);
        }

        /// <summary>
        /// Loads the companies associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load companies</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when person is null</exception>
        /// <exception cref="ArgumentException">Thrown when person ID is empty</exception>
        public async Task LoadCompaniesAsync(Person person, CancellationToken cancellationToken = default)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            if (person.PersonId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(person));

            try
            {
                _logger.LogInformation("Loading companies for person with ID {PersonId}", person.PersonId);

                const string sql = @"
                    SELECT cp.*, c.*
                    FROM Company_Person cp
                    INNER JOIN Company c ON cp.CompanyId = c.CompanyId
                    WHERE cp.PersonId = @PersonId AND cp.Active = 1 AND c.Active = 1";

                var companyPersonDictionary = new Dictionary<Guid, Company_Person>();

                await ExecuteWithResilienceAndLoggingAsync(
                    async connection =>
                    {
                        await connection.QueryAsync<Company_Person, Company, Company_Person>(
                            new CommandDefinition(
                                sql,
                                new { PersonId = person.PersonId },
                                cancellationToken: cancellationToken),
                            (companyPerson, company) =>
                            {
                                if (!companyPersonDictionary.TryGetValue(companyPerson.CompanyId, out var companyPersonEntry))
                                {
                                    companyPersonEntry = companyPerson;
                                    companyPersonDictionary.Add(companyPersonEntry.CompanyId, companyPersonEntry);
                                }

                                companyPersonEntry.Company = company;
                                return companyPersonEntry;
                            },
                            splitOn: "CompanyId");

                        return true;
                    },
                    "LoadCompaniesAsync",
                    new { person.PersonId, ErrorMessage = "Error loading companies for person" },
                    cancellationToken);

                person.Companies = companyPersonDictionary.Values.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading companies for person with ID {PersonId}", person.PersonId);
                throw;
            }
        }

        /// <summary>
        /// Loads the addresses associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load addresses</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when person is null</exception>
        /// <exception cref="ArgumentException">Thrown when person ID is empty</exception>
        public async Task LoadAddressesAsync(Person person, CancellationToken cancellationToken = default)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            if (person.PersonId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(person));

            try
            {
                _logger.LogInformation("Loading addresses for person with ID {PersonId}", person.PersonId);

                const string sql = @"
                    SELECT pa.*, a.*, at.*, s.*
                    FROM Person_Address pa
                    INNER JOIN Address a ON pa.AddressId = a.AddressId
                    LEFT JOIN AddressType at ON a.AddressTypeId = at.AddressTypeId
                    LEFT JOIN State s ON a.StateId = s.StateId
                    WHERE pa.PersonId = @PersonId AND pa.Active = 1 AND a.Active = 1";

                var personAddressDictionary = new Dictionary<Guid, Person_Address>();

                await ExecuteWithResilienceAndLoggingAsync(
                    async connection =>
                    {
                        await connection.QueryAsync<Person_Address, Address, AddressType, State, Person_Address>(
                            new CommandDefinition(
                                sql,
                                new { PersonId = person.PersonId },
                                cancellationToken: cancellationToken),
                            (personAddress, address, addressType, state) =>
                            {
                                if (!personAddressDictionary.TryGetValue(personAddress.AddressId, out var personAddressEntry))
                                {
                                    personAddressEntry = personAddress;
                                    personAddressDictionary.Add(personAddressEntry.AddressId, personAddressEntry);
                                }

                                address.AddressType = addressType;
                                address.State = state;
                                personAddressEntry.Address = address;
                                return personAddressEntry;
                            },
                            splitOn: "AddressId,AddressTypeId,StateId");

                        return true;
                    },
                    "LoadAddressesAsync",
                    new { person.PersonId, ErrorMessage = "Error loading addresses for person" },
                    cancellationToken);

                person.Addresses = personAddressDictionary.Values.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading addresses for person with ID {PersonId}", person.PersonId);
                throw;
            }
        }

        /// <summary>
        /// Loads the phone numbers associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load phone numbers</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when person is null</exception>
        /// <exception cref="ArgumentException">Thrown when person ID is empty</exception>
        public async Task LoadPhoneNumbersAsync(Person person, CancellationToken cancellationToken = default)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            if (person.PersonId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(person));

            try
            {
                _logger.LogInformation("Loading phone numbers for person with ID {PersonId}", person.PersonId);

                const string sql = @"
                    SELECT pp.*, p.*, pt.*
                    FROM Person_Phone pp
                    INNER JOIN Phone p ON pp.PhoneId = p.PhoneId
                    LEFT JOIN PhoneType pt ON p.PhoneTypeId = pt.PhoneTypeId
                    WHERE pp.PersonId = @PersonId AND pp.Active = 1 AND p.Active = 1";

                var personPhoneDictionary = new Dictionary<Guid, Person_Phone>();

                await ExecuteWithResilienceAndLoggingAsync(
                    async connection =>
                    {
                        await connection.QueryAsync<Person_Phone, Phone, PhoneType, Person_Phone>(
                            new CommandDefinition(
                                sql,
                                new { PersonId = person.PersonId },
                                cancellationToken: cancellationToken),
                            (personPhone, phone, phoneType) =>
                            {
                                if (!personPhoneDictionary.TryGetValue(personPhone.PhoneId, out var personPhoneEntry))
                                {
                                    personPhoneEntry = personPhone;
                                    personPhoneDictionary.Add(personPhoneEntry.PhoneId, personPhoneEntry);
                                }

                                phone.PhoneType = phoneType;
                                personPhoneEntry.Phone = phone;
                                return personPhoneEntry;
                            },
                            splitOn: "PhoneId,PhoneTypeId");

                        return true;
                    },
                    "LoadPhoneNumbersAsync",
                    new { person.PersonId, ErrorMessage = "Error loading phone numbers for person" },
                    cancellationToken);

                person.PhoneNumbers = personPhoneDictionary.Values.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading phone numbers for person with ID {PersonId}", person.PersonId);
                throw;
            }
        }

        /// <summary>
        /// Loads the email addresses associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load email addresses</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when person is null</exception>
        /// <exception cref="ArgumentException">Thrown when person ID is empty</exception>
        public async Task LoadEmailAddressesAsync(Person person, CancellationToken cancellationToken = default)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            if (person.PersonId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(person));

            try
            {
                _logger.LogInformation("Loading email addresses for person with ID {PersonId}", person.PersonId);

                const string sql = @"
                    SELECT pe.*, e.*, eat.*
                    FROM Person_EmailAddress pe
                    INNER JOIN EmailAddress e ON pe.EmailAddressId = e.EmailAddressId
                    LEFT JOIN EmailAddressType eat ON e.EmailAddressTypeId = eat.EmailAddressTypeId
                    WHERE pe.PersonId = @PersonId AND pe.Active = 1 AND e.Active = 1";

                var personEmailDictionary = new Dictionary<Guid, Person_EmailAddress>();

                await ExecuteWithResilienceAndLoggingAsync(
                    async connection =>
                    {
                        await connection.QueryAsync<Person_EmailAddress, EmailAddress, EmailAddressType, Person_EmailAddress>(
                            new CommandDefinition(
                                sql,
                                new { PersonId = person.PersonId },
                                cancellationToken: cancellationToken),
                            (personEmail, email, emailAddressType) =>
                            {
                                if (!personEmailDictionary.TryGetValue(personEmail.EmailAddressId, out var personEmailEntry))
                                {
                                    personEmailEntry = personEmail;
                                    personEmailDictionary.Add(personEmailEntry.EmailAddressId, personEmailEntry);
                                }

                                email.EmailAddressType = emailAddressType;
                                personEmailEntry.EmailAddress = email;
                                return personEmailEntry;
                            },
                            splitOn: "EmailAddressId,EmailAddressTypeId");

                        return true;
                    },
                    "LoadEmailAddressesAsync",
                    new { person.PersonId, ErrorMessage = "Error loading email addresses for person" },
                    cancellationToken);

                person.EmailAddresses = personEmailDictionary.Values.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading email addresses for person with ID {PersonId}", person.PersonId);
                throw;
            }
        }

        /// <summary>
        /// Loads the activities associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load activities</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when person is null</exception>
        /// <exception cref="ArgumentException">Thrown when person ID is empty</exception>
        public async Task LoadActivitiesAsync(Person person, CancellationToken cancellationToken = default)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            if (person.PersonId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(person));

            try
            {
                _logger.LogInformation("Loading activities for person with ID {PersonId}", person.PersonId);

                const string sql = @"
                    SELECT pa.*, a.*, at.*
                    FROM Person_Activity pa
                    INNER JOIN Activity a ON pa.ActivityId = a.ActivityId
                    LEFT JOIN ActivityType at ON a.ActivityTypeId = at.ActivityTypeId
                    WHERE pa.PersonId = @PersonId AND pa.Active = 1 AND a.Active = 1";

                var personActivityDictionary = new Dictionary<Guid, Person_Activity>();

                await ExecuteWithResilienceAndLoggingAsync(
                    async connection =>
                    {
                        await connection.QueryAsync<Person_Activity, Activity, ActivityType, Person_Activity>(
                            new CommandDefinition(
                                sql,
                                new { PersonId = person.PersonId },
                                cancellationToken: cancellationToken),
                            (personActivity, activity, activityType) =>
                            {
                                if (!personActivityDictionary.TryGetValue(personActivity.ActivityId, out var personActivityEntry))
                                {
                                    personActivityEntry = personActivity;
                                    personActivityDictionary.Add(personActivityEntry.ActivityId, personActivityEntry);
                                }

                                activity.ActivityTypeId = activityType.ActivityTypeId;
                                personActivityEntry.Activity = activity;
                                return personActivityEntry;
                            },
                            splitOn: "ActivityId,ActivityTypeId");

                        return true;
                    },
                    "LoadActivitiesAsync",
                    new { person.PersonId, ErrorMessage = "Error loading activities for person" },
                    cancellationToken);

                person.Activities = personActivityDictionary.Values.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading activities for person with ID {PersonId}", person.PersonId);
                throw;
            }
        }

        /// <summary>
        /// Loads the attachments associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load attachments</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when person is null</exception>
        /// <exception cref="ArgumentException">Thrown when person ID is empty</exception>
        public async Task LoadAttachmentsAsync(Person person, CancellationToken cancellationToken = default)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            if (person.PersonId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(person));

            try
            {
                _logger.LogInformation("Loading attachments for person with ID {PersonId}", person.PersonId);

                const string sql = @"
                    SELECT pa.*, a.*, at.*
                    FROM Person_Attachment pa
                    INNER JOIN Attachment a ON pa.AttachmentId = a.AttachmentId
                    LEFT JOIN AttachmentType at ON a.AttachmentTypeId = at.AttachmentTypeId
                    WHERE pa.PersonId = @PersonId AND pa.Active = 1 AND a.Active = 1";

                var personAttachmentDictionary = new Dictionary<Guid, Person_Attachment>();

                await ExecuteWithResilienceAndLoggingAsync(
                    async connection =>
                    {
                        await connection.QueryAsync<Person_Attachment, Attachment, AttachmentType, Person_Attachment>(
                            new CommandDefinition(
                                sql,
                                new { PersonId = person.PersonId },
                                cancellationToken: cancellationToken),
                            (personAttachment, attachment, attachmentType) =>
                            {
                                if (!personAttachmentDictionary.TryGetValue(personAttachment.AttachmentId, out var personAttachmentEntry))
                                {
                                    personAttachmentEntry = personAttachment;
                                    personAttachmentDictionary.Add(personAttachmentEntry.AttachmentId, personAttachmentEntry);
                                }

                                attachment.AttachmentType = attachmentType;
                                personAttachmentEntry.Attachment = attachment;
                                return personAttachmentEntry;
                            },
                            splitOn: "AttachmentId,AttachmentTypeId");

                        return true;
                    },
                    "LoadAttachmentsAsync",
                    new { person.PersonId, ErrorMessage = "Error loading attachments for person" },
                    cancellationToken);

                person.Attachments = personAttachmentDictionary.Values.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading attachments for person with ID {PersonId}", person.PersonId);
                throw;
            }
        }

        /// <summary>
        /// Loads the notes associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load notes</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when person is null</exception>
        /// <exception cref="ArgumentException">Thrown when person ID is empty</exception>
        public async Task LoadNotesAsync(Person person, CancellationToken cancellationToken = default)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            if (person.PersonId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(person));

            try
            {
                _logger.LogInformation("Loading notes for person with ID {PersonId}", person.PersonId);

                const string sql = @"
                    SELECT pn.*, n.*, nt.*
                    FROM Person_Note pn
                    INNER JOIN Note n ON pn.NoteId = n.NoteId
                    LEFT JOIN NoteType nt ON n.NoteTypeId = nt.NoteTypeId
                    WHERE pn.PersonId = @PersonId AND pn.Active = 1 AND n.Active = 1";

                var personNoteDictionary = new Dictionary<Guid, Person_Note>();

                await ExecuteWithResilienceAndLoggingAsync(
                    async connection =>
                    {
                        await connection.QueryAsync<Person_Note, Note, NoteType, Person_Note>(
                            new CommandDefinition(
                                sql,
                                new { PersonId = person.PersonId },
                                cancellationToken: cancellationToken),
                            (personNote, note, noteType) =>
                            {
                                if (!personNoteDictionary.TryGetValue(personNote.NoteId, out var personNoteEntry))
                                {
                                    personNoteEntry = personNote;
                                    personNoteDictionary.Add(personNoteEntry.NoteId, personNoteEntry);
                                }

                                note.NoteType = noteType;
                                personNoteEntry.Note = note;
                                return personNoteEntry;
                            },
                            splitOn: "NoteId,NoteTypeId");

                        return true;
                    },
                    "LoadNotesAsync",
                    new { person.PersonId, ErrorMessage = "Error loading notes for person" },
                    cancellationToken);

                person.PersonNotes = personNoteDictionary.Values.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading notes for person with ID {PersonId}", person.PersonId);
                throw;
            }
        }

        /// <summary>
        /// Loads the calls associated with the specified person
        /// </summary>
        /// <param name="person">The person for which to load calls</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when person is null</exception>
        /// <exception cref="ArgumentException">Thrown when person ID is empty</exception>
        public async Task LoadCallsAsync(Person person, CancellationToken cancellationToken = default)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            if (person.PersonId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(person));

            try
            {
                _logger.LogInformation("Loading calls for person with ID {PersonId}", person.PersonId);

                const string sql = @"
                    SELECT pc.*, c.*, ct.*
                    FROM Person_Call pc
                    INNER JOIN Call c ON pc.CallId = c.CallId
                    LEFT JOIN CallType ct ON c.CallTypeId = ct.CallTypeId
                    WHERE pc.PersonId = @PersonId AND pc.Active = 1 AND c.Active = 1";

                var personCallDictionary = new Dictionary<Guid, Person_Call>();

                await ExecuteWithResilienceAndLoggingAsync(
                    async connection =>
                    {
                        await connection.QueryAsync<Person_Call, Call, CallType, Person_Call>(
                            new CommandDefinition(
                                sql,
                                new { PersonId = person.PersonId },
                                cancellationToken: cancellationToken),
                            (personCall, call, callType) =>
                            {
                                if (!personCallDictionary.TryGetValue(personCall.CallId, out var personCallEntry))
                                {
                                    personCallEntry = personCall;
                                    personCallDictionary.Add(personCallEntry.CallId, personCallEntry);
                                }

                                // Set the call type using the TypeId property of the Call entity
                                // instead of directly setting a CallType property that doesn't exist
                                call.TypeId = callType.CallTypeId;
                                personCallEntry.Call = call;
                                return personCallEntry;
                            },
                            splitOn: "CallId,CallTypeId");

                        return true;
                    },
                    "LoadCallsAsync",
                    new { person.PersonId, ErrorMessage = "Error loading calls for person" },
                    cancellationToken);

                person.Calls = personCallDictionary.Values.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading calls for person with ID {PersonId}", person.PersonId);
                throw;
            }
        }

        /// <summary>
        /// Gets a person by ID with all related entities loaded
        /// </summary>
        /// <param name="id">The unique identifier of the person</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person with all related entities loaded, or null if not found</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public async Task<Person?> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(id));

            try
            {
                _logger.LogInformation("Getting person with ID {PersonId} with all related entities", id);

                var person = await GetByIdAsync(id, cancellationToken);
                if (person == null) return null;

                await LoadCompaniesAsync(person, cancellationToken);
                await LoadAddressesAsync(person, cancellationToken);
                await LoadPhoneNumbersAsync(person, cancellationToken);
                await LoadEmailAddressesAsync(person, cancellationToken);
                await LoadActivitiesAsync(person, cancellationToken);
                await LoadAttachmentsAsync(person, cancellationToken);
                await LoadNotesAsync(person, cancellationToken);
                await LoadCallsAsync(person, cancellationToken);

                return person;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting person with ID {PersonId} with related entities", id);
                throw;
            }
        }
    }
}