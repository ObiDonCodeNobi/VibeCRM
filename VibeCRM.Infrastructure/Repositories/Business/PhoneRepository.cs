using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing Phone entities
    /// </summary>
    public class PhoneRepository : BaseRepository<Phone, Guid>, IPhoneRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Phone";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "PhoneId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "PhoneId", "AreaCode", "Prefix", "LineNumber", "Extension", "PhoneTypeId",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public PhoneRepository(ISQLConnectionFactory connectionFactory, ILogger<PhoneRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new phone to the repository
        /// </summary>
        /// <param name="entity">The phone entity to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added phone with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<Phone> AddAsync(Phone entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            const string sql = @"
                INSERT INTO Phone (
                    PhoneId, AreaCode, Prefix, LineNumber, Extension, PhoneTypeId,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @PhoneId, @AreaCode, @Prefix, @LineNumber, @Extension, @PhoneTypeId,
                    @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active
                );";

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        entity,
                        cancellationToken: cancellationToken)),
                "AddAsync",
                new { PhoneId = entity.PhoneId, EntityType = nameof(Phone) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing phone in the repository
        /// </summary>
        /// <param name="entity">The phone to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated phone</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<Phone> UpdateAsync(Phone entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            const string sql = @"
                UPDATE Phone
                SET AreaCode = @AreaCode,
                    Prefix = @Prefix,
                    LineNumber = @LineNumber,
                    Extension = @Extension,
                    PhoneTypeId = @PhoneTypeId,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate,
                    Active = @Active
                WHERE PhoneId = @PhoneId";

            int affectedRows = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        entity,
                        cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { PhoneId = entity.PhoneId, EntityType = nameof(Phone) },
                cancellationToken);

            if (affectedRows == 0)
            {
                _logger.LogWarning("No Phone was updated for ID {PhoneId}", entity.PhoneId);
            }

            return entity;
        }

        /// <summary>
        /// Gets phones by their type
        /// </summary>
        /// <param name="phoneTypeId">The phone type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phones with the specified type</returns>
        /// <exception cref="ArgumentException">Thrown when phoneTypeId is empty</exception>
        public async Task<IEnumerable<Phone>> GetByPhoneTypeAsync(Guid phoneTypeId, CancellationToken cancellationToken = default)
        {
            if (phoneTypeId == Guid.Empty) throw new ArgumentException("The Phone Type ID cannot be empty", nameof(phoneTypeId));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE PhoneTypeId = @PhoneTypeId AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Phone>(
                    new CommandDefinition(
                        sql,
                        new { PhoneTypeId = phoneTypeId },
                        cancellationToken: cancellationToken)),
                "GetByPhoneTypeAsync",
                new { ErrorMessage = $"Error retrieving Phones with Type ID {phoneTypeId}", PhoneTypeId = phoneTypeId, EntityType = nameof(Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Gets phones for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phones associated with the specified company</returns>
        /// <exception cref="ArgumentException">Thrown when companyId is empty</exception>
        public async Task<IEnumerable<Phone>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            if (companyId == Guid.Empty) throw new ArgumentException("The Company ID cannot be empty", nameof(companyId));

            const string sql = @"
                SELECT p.PhoneId, p.AreaCode, p.Prefix, p.LineNumber, p.Extension, p.PhoneTypeId,
                       p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Phone p
                JOIN Company_Phone cp ON p.PhoneId = cp.PhoneId
                WHERE cp.CompanyId = @CompanyId AND p.Active = 1 AND cp.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Phone>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyAsync",
                new { ErrorMessage = $"Error retrieving Phones associated with Company ID {companyId}", CompanyId = companyId },
                cancellationToken);
        }

        /// <summary>
        /// Gets phones for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phones associated with the specified person</returns>
        /// <exception cref="ArgumentException">Thrown when personId is empty</exception>
        public async Task<IEnumerable<Phone>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            if (personId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(personId));

            const string sql = @"
                SELECT p.PhoneId, p.AreaCode, p.Prefix, p.LineNumber, p.Extension, p.PhoneTypeId,
                       p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate, p.Active
                FROM Phone p
                JOIN Person_Phone pp ON p.PhoneId = pp.PhoneId
                WHERE pp.PersonId = @PersonId AND p.Active = 1 AND pp.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Phone>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetByPersonAsync",
                new { ErrorMessage = $"Error retrieving Phones associated with Person ID {personId}", PersonId = personId },
                cancellationToken);
        }

        /// <summary>
        /// Gets a phone by ID with all related details (phone type, companies, persons)
        /// </summary>
        /// <param name="id">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The phone with all related details if found, otherwise null</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public async Task<Phone> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Phone ID cannot be empty", nameof(id));

            // First, get the phone with its basic properties
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE PhoneId = @PhoneId AND Active = 1";

            var phone = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Phone>(
                    new CommandDefinition(
                        sql,
                        new { PhoneId = id },
                        cancellationToken: cancellationToken)),
                "GetByIdWithDetailsAsync_Phone",
                new { ErrorMessage = $"Error retrieving Phone with ID {id}", PhoneId = id },
                cancellationToken);

            if (phone == null)
            {
                return new Phone();
            }

            // Get the phone type
            const string phoneTypeSql = @"
                SELECT pt.PhoneTypeId, pt.Type, pt.Description, pt.CreatedBy, pt.CreatedDate, pt.ModifiedBy, pt.ModifiedDate, pt.Active
                FROM PhoneType pt
                WHERE pt.PhoneTypeId = @PhoneTypeId AND pt.Active = 1";

            var phoneType = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<PhoneType>(
                    new CommandDefinition(
                        phoneTypeSql,
                        new { PhoneTypeId = phone.PhoneTypeId },
                        cancellationToken: cancellationToken)),
                "GetByIdWithDetailsAsync_PhoneType",
                new { ErrorMessage = $"Error retrieving PhoneType for Phone ID {id}", PhoneId = id, PhoneTypeId = phone.PhoneTypeId },
                cancellationToken);

            phone.PhoneType = phoneType;

            // Get associated companies
            const string companySql = @"
                SELECT cp.CompanyId, cp.PhoneId, cp.Active, cp.ModifiedDate,
                       c.CompanyId, c.Name, c.Active
                FROM Company_Phone cp
                JOIN Company c ON cp.CompanyId = c.CompanyId AND c.Active = 1
                WHERE cp.PhoneId = @PhoneId AND cp.Active = 1";

            var companyPhones = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Company_Phone, Company, Company_Phone>(
                    new CommandDefinition(
                        companySql,
                        new { PhoneId = id },
                        cancellationToken: cancellationToken),
                    (cp, c) =>
                    {
                        cp.Company = c;
                        return cp;
                    },
                    splitOn: "CompanyId"),
                "GetByIdWithDetailsAsync_Companies",
                new { ErrorMessage = $"Error retrieving associated companies for Phone ID {id}", PhoneId = id },
                cancellationToken);

            phone.Companies = companyPhones.ToList();

            // Get associated persons
            const string personSql = @"
                SELECT pp.PersonId, pp.PhoneId, pp.Active, pp.ModifiedDate,
                       p.PersonId, p.Firstname, p.Lastname, p.Active
                FROM Person_Phone pp
                JOIN Person p ON pp.PersonId = p.PersonId AND p.Active = 1
                WHERE pp.PhoneId = @PhoneId AND pp.Active = 1";

            var personPhones = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Phone, Person, Person_Phone>(
                    new CommandDefinition(
                        personSql,
                        new { PhoneId = id },
                        cancellationToken: cancellationToken),
                    (pp, p) =>
                    {
                        pp.Person = p;
                        return pp;
                    },
                    splitOn: "PersonId"),
                "GetByIdWithDetailsAsync_Persons",
                new { ErrorMessage = $"Error retrieving associated persons for Phone ID {id}", PhoneId = id },
                cancellationToken);

            phone.Persons = personPhones.ToList();

            return phone;
        }

        /// <summary>
        /// Checks if a phone is associated with a company
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the phone is associated with the company, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when phoneId or companyId is empty</exception>
        public async Task<bool> IsPhoneAssociatedWithCompanyAsync(Guid phoneId, Guid companyId, CancellationToken cancellationToken = default)
        {
            if (phoneId == Guid.Empty) throw new ArgumentException("The Phone ID cannot be empty", nameof(phoneId));
            if (companyId == Guid.Empty) throw new ArgumentException("The Company ID cannot be empty", nameof(companyId));

            const string sql = @"
                SELECT COUNT(1)
                FROM Company_Phone
                WHERE PhoneId = @PhoneId AND CompanyId = @CompanyId AND Active = 1";

            int count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { PhoneId = phoneId, CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "IsPhoneAssociatedWithCompanyAsync",
                new { ErrorMessage = $"Error checking if Phone {phoneId} is associated with Company {companyId}", PhoneId = phoneId, CompanyId = companyId },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Checks if a phone is associated with a person
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the phone is associated with the person, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when phoneId or personId is empty</exception>
        public async Task<bool> IsPhoneAssociatedWithPersonAsync(Guid phoneId, Guid personId, CancellationToken cancellationToken = default)
        {
            if (phoneId == Guid.Empty) throw new ArgumentException("The Phone ID cannot be empty", nameof(phoneId));
            if (personId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(personId));

            const string sql = @"
                SELECT COUNT(1)
                FROM Person_Phone
                WHERE PhoneId = @PhoneId AND PersonId = @PersonId AND Active = 1";

            int count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { PhoneId = phoneId, PersonId = personId },
                        cancellationToken: cancellationToken)),
                "IsPhoneAssociatedWithPersonAsync",
                new { ErrorMessage = $"Error checking if Phone {phoneId} is associated with Person {personId}", PhoneId = phoneId, PersonId = personId },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Adds a phone to a company by creating an association
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the association was successfully created, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when phoneId or companyId is empty</exception>
        public async Task<bool> AddPhoneToCompanyAsync(Guid phoneId, Guid companyId, CancellationToken cancellationToken = default)
        {
            if (phoneId == Guid.Empty) throw new ArgumentException("The Phone ID cannot be empty", nameof(phoneId));
            if (companyId == Guid.Empty) throw new ArgumentException("The Company ID cannot be empty", nameof(companyId));

            // Check if association already exists
            if (await IsPhoneAssociatedWithCompanyAsync(phoneId, companyId, cancellationToken))
            {
                _logger.LogInformation("Phone {PhoneId} is already associated with Company {CompanyId}", phoneId, companyId);
                return true;
            }

            // Create the association
            const string sql = @"
                INSERT INTO Company_Phone (CompanyId, PhoneId, Active, ModifiedDate)
                VALUES (@CompanyId, @PhoneId, 1, @ModifiedDate)";

            var parameters = new
            {
                CompanyId = companyId,
                PhoneId = phoneId,
                ModifiedDate = DateTime.UtcNow
            };

            int affectedRows = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddPhoneToCompanyAsync",
                new { ErrorMessage = $"Error adding Phone {phoneId} to Company {companyId}", PhoneId = phoneId, CompanyId = companyId },
                cancellationToken);

            return affectedRows > 0;
        }

        /// <summary>
        /// Adds a phone to a person by creating an association
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the association was successfully created, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when phoneId or personId is empty</exception>
        public async Task<bool> AddPhoneToPersonAsync(Guid phoneId, Guid personId, CancellationToken cancellationToken = default)
        {
            if (phoneId == Guid.Empty) throw new ArgumentException("The Phone ID cannot be empty", nameof(phoneId));
            if (personId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(personId));

            // Check if association already exists
            if (await IsPhoneAssociatedWithPersonAsync(phoneId, personId, cancellationToken))
            {
                _logger.LogInformation("Phone {PhoneId} is already associated with Person {PersonId}", phoneId, personId);
                return true;
            }

            // Create the association
            const string sql = @"
                INSERT INTO Person_Phone (PersonId, PhoneId, Active, ModifiedDate)
                VALUES (@PersonId, @PhoneId, 1, @ModifiedDate)";

            var parameters = new
            {
                PersonId = personId,
                PhoneId = phoneId,
                ModifiedDate = DateTime.UtcNow
            };

            int affectedRows = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddPhoneToPersonAsync",
                new { ErrorMessage = $"Error adding Phone {phoneId} to Person {personId}", PhoneId = phoneId, PersonId = personId },
                cancellationToken);

            return affectedRows > 0;
        }

        /// <summary>
        /// Removes a phone from a company by soft-deleting the association
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the association was successfully removed, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when phoneId or companyId is empty</exception>
        public async Task<bool> RemovePhoneFromCompanyAsync(Guid phoneId, Guid companyId, CancellationToken cancellationToken = default)
        {
            if (phoneId == Guid.Empty) throw new ArgumentException("The Phone ID cannot be empty", nameof(phoneId));
            if (companyId == Guid.Empty) throw new ArgumentException("The Company ID cannot be empty", nameof(companyId));

            // Soft delete the association (set Active = 0)
            const string sql = @"
                UPDATE Company_Phone
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE PhoneId = @PhoneId AND CompanyId = @CompanyId AND Active = 1";

            var parameters = new
            {
                PhoneId = phoneId,
                CompanyId = companyId,
                ModifiedDate = DateTime.UtcNow
            };

            int affectedRows = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemovePhoneFromCompanyAsync",
                new { ErrorMessage = $"Error removing Phone {phoneId} from Company {companyId}", PhoneId = phoneId, CompanyId = companyId },
                cancellationToken);

            return affectedRows > 0;
        }

        /// <summary>
        /// Removes a phone from a person by soft-deleting the association
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the association was successfully removed, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when phoneId or personId is empty</exception>
        public async Task<bool> RemovePhoneFromPersonAsync(Guid phoneId, Guid personId, CancellationToken cancellationToken = default)
        {
            if (phoneId == Guid.Empty) throw new ArgumentException("The Phone ID cannot be empty", nameof(phoneId));
            if (personId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(personId));

            // Soft delete the association (set Active = 0)
            const string sql = @"
                UPDATE Person_Phone
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE PhoneId = @PhoneId AND PersonId = @PersonId AND Active = 1";

            var parameters = new
            {
                PhoneId = phoneId,
                PersonId = personId,
                ModifiedDate = DateTime.UtcNow
            };

            int affectedRows = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemovePhoneFromPersonAsync",
                new { ErrorMessage = $"Error removing Phone {phoneId} from Person {personId}", PhoneId = phoneId, PersonId = personId },
                cancellationToken);

            return affectedRows > 0;
        }

        /// <summary>
        /// Searches for phones by phone number (area code, prefix, line number)
        /// </summary>
        /// <param name="phoneNumber">The phone number to search for (can be partial)</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phones matching the search criteria</returns>
        public async Task<IEnumerable<Phone>> SearchByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return new List<Phone>();
            }

            // Remove non-numeric characters from the search string
            string cleanNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(cleanNumber))
            {
                return new List<Phone>();
            }

            string sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Active = 1 AND (
                    CAST(AreaCode AS NVARCHAR(10)) + CAST(Prefix AS NVARCHAR(10)) + CAST(LineNumber AS NVARCHAR(10)) LIKE @SearchPattern
                    OR CAST(Prefix AS NVARCHAR(10)) + CAST(LineNumber AS NVARCHAR(10)) LIKE @SearchPattern
                    OR CAST(LineNumber AS NVARCHAR(10)) LIKE @SearchPattern
                )";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Phone>(
                    new CommandDefinition(
                        sql,
                        new { SearchPattern = $"%{cleanNumber}%" },
                        cancellationToken: cancellationToken)),
                "SearchByPhoneNumberAsync",
                new { ErrorMessage = $"Error searching for phones with number '{phoneNumber}'", SearchTerm = phoneNumber },
                cancellationToken);
        }

        /// <summary>
        /// Gets a phone by the phone number
        /// </summary>
        /// <param name="phoneNumber">The phone number to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The phone entity with the specified phone number if found, otherwise null</returns>
        /// <exception cref="ArgumentException">Thrown when the phone number is null, empty, or doesn't contain at least 7 digits</exception>
        public async Task<Phone> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number cannot be null or empty", nameof(phoneNumber));

            // Normalize the phone number by removing non-numeric characters
            string normalizedPhoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            // If the normalized phone number is too short, throw an exception
            if (normalizedPhoneNumber.Length < 7)
                throw new ArgumentException("Phone number must contain at least 7 digits", nameof(phoneNumber));

            // Check for the phone with the full number (area code + prefix + line number combined)
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE
                    CAST(AreaCode AS NVARCHAR(10)) + CAST(Prefix AS NVARCHAR(10)) + CAST(LineNumber AS NVARCHAR(10)) = @NormalizedNumber
                    AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Phone>(
                    new CommandDefinition(
                        sql,
                        new { NormalizedNumber = normalizedPhoneNumber },
                        cancellationToken: cancellationToken)),
                "GetByPhoneNumberAsync",
                new { ErrorMessage = $"Error retrieving Phone with Phone Number {phoneNumber}", PhoneNumber = phoneNumber, NormalizedNumber = normalizedPhoneNumber },
                cancellationToken);
        }

        /// <summary>
        /// Gets phones by country code
        /// </summary>
        /// <param name="countryCode">The country code to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phones with the specified country code</returns>
        /// <exception cref="ArgumentException">Thrown when countryCode is null or empty</exception>
        public async Task<IEnumerable<Phone>> GetByCountryCodeAsync(string countryCode, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                throw new ArgumentException("Country code cannot be null or empty", nameof(countryCode));

            // Normalize the country code by removing non-numeric characters and leading plus sign
            string normalizedCountryCode = countryCode.TrimStart('+');
            normalizedCountryCode = new string(normalizedCountryCode.Where(char.IsDigit).ToArray());

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE CountryCode = @CountryCode
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Phone>(
                    new CommandDefinition(
                        sql,
                        new { CountryCode = normalizedCountryCode },
                        cancellationToken: cancellationToken)),
                "GetByCountryCodeAsync",
                new { ErrorMessage = $"Error retrieving Phones with Country Code {countryCode}", CountryCode = countryCode, NormalizedCountryCode = normalizedCountryCode },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a phone number is unique
        /// </summary>
        /// <param name="phoneNumber">The phone number to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the phone number is unique, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when phoneNumber is null or empty</exception>
        public async Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number cannot be null or empty", nameof(phoneNumber));

            // Normalize the phone number by removing non-numeric characters
            string normalizedPhoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            // If the normalized phone number is too short, throw an exception
            if (normalizedPhoneNumber.Length < 7)
                throw new ArgumentException("Phone number must contain at least 7 digits", nameof(phoneNumber));

            var sql = $@"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM {TableName}
                    WHERE
                        CAST(AreaCode AS NVARCHAR(10)) + CAST(Prefix AS NVARCHAR(10)) + CAST(LineNumber AS NVARCHAR(10)) = @NormalizedNumber
                        AND Active = 1
                ) THEN 0 ELSE 1 END";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<bool>(
                    new CommandDefinition(
                        sql,
                        new { NormalizedNumber = normalizedPhoneNumber },
                        cancellationToken: cancellationToken)),
                "IsPhoneNumberUniqueAsync",
                new { ErrorMessage = $"Error checking Phone Number {phoneNumber} for uniqueness", PhoneNumber = phoneNumber, NormalizedNumber = normalizedPhoneNumber },
                cancellationToken);
        }
    }
}