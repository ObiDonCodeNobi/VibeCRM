using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing Address entities
    /// </summary>
    public class AddressRepository : BaseRepository<Address, Guid>, IAddressRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Address";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "AddressId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "AddressId", "AddressTypeId", "Line1", "Line2", "City", "StateId", "Zip",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public AddressRepository(ISQLConnectionFactory connectionFactory, ILogger<AddressRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new address to the repository
        /// </summary>
        /// <param name="entity">The address to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added address with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when AddressId is empty</exception>
        public override async Task<Address> AddAsync(Address entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AddressId == Guid.Empty) throw new ArgumentException("The Address ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO Address (
                    AddressId, AddressTypeId, Line1, Line2, City, StateId, Zip,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @AddressId, @AddressTypeId, @Line1, @Line2, @City, @StateId, @Zip,
                    @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active
                );";

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding Address with ID {entity.AddressId}", AddressId = entity.AddressId, EntityType = nameof(Address) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing address in the repository
        /// </summary>
        /// <param name="entity">The address to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated address</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when AddressId is empty</exception>
        public override async Task<Address> UpdateAsync(Address entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AddressId == Guid.Empty) throw new ArgumentException("The Address ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE Address
                SET
                    AddressTypeId = @AddressTypeId,
                    Line1 = @Line1,
                    Line2 = @Line2,
                    City = @City,
                    StateId = @StateId,
                    Zip = @Zip,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate,
                    Active = @Active
                WHERE AddressId = @AddressId";

            var affectedRows = await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating Address with ID {entity.AddressId}", AddressId = entity.AddressId, EntityType = nameof(Address) },
                cancellationToken);

            if (affectedRows == 0)
            {
                _logger.LogWarning("No Address was updated for ID {AddressId}", entity.AddressId);
            }

            return entity;
        }

        /// <summary>
        /// Gets addresses by address type
        /// </summary>
        /// <param name="addressTypeId">The address type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses with the specified address type</returns>
        /// <exception cref="ArgumentException">Thrown when addressTypeId is empty</exception>
        public async Task<IEnumerable<Address>> GetByAddressTypeAsync(Guid addressTypeId, CancellationToken cancellationToken = default)
        {
            if (addressTypeId == Guid.Empty) throw new ArgumentException("The Address Type ID cannot be empty", nameof(addressTypeId));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE AddressTypeId = @AddressTypeId AND Active = 1
                ORDER BY CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Address>>(
                async (connection) =>
                    await connection.QueryAsync<Address>(
                        new CommandDefinition(
                            sql,
                            new { AddressTypeId = addressTypeId },
                            cancellationToken: cancellationToken)),
                "GetByAddressTypeAsync",
                new { ErrorMessage = $"Error retrieving Addresses with Type ID {addressTypeId}", AddressTypeId = addressTypeId, EntityType = nameof(Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets addresses by city
        /// </summary>
        /// <param name="city">The city name</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses in the specified city</returns>
        /// <exception cref="ArgumentException">Thrown when city is null or empty</exception>
        public async Task<IEnumerable<Address>> GetByCityAsync(string city, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be null or whitespace", nameof(city));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE City = @City AND Active = 1
                ORDER BY Line1, Line2";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Address>>(
                async (connection) =>
                    await connection.QueryAsync<Address>(
                        new CommandDefinition(
                            sql,
                            new { City = city },
                            cancellationToken: cancellationToken)),
                "GetByCityAsync",
                new { ErrorMessage = $"Error retrieving Addresses in City {city}", City = city, EntityType = nameof(Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets addresses associated with a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses associated with the specified company</returns>
        /// <exception cref="ArgumentException">Thrown when companyId is empty</exception>
        public async Task<IEnumerable<Address>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            if (companyId == Guid.Empty) throw new ArgumentException("The Company ID cannot be empty", nameof(companyId));

            const string sql = @"
                SELECT a.AddressId, a.AddressTypeId, a.Line1, a.Line2, a.City, a.StateId, a.Zip,
                       a.CreatedBy, a.CreatedDate, a.ModifiedBy, a.ModifiedDate, a.Active
                FROM Address a
                INNER JOIN Company_Address ca ON a.AddressId = ca.AddressId
                WHERE ca.CompanyId = @CompanyId AND a.Active = 1
                ORDER BY a.AddressTypeId, a.Line1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Address>>(
                async (connection) =>
                    await connection.QueryAsync<Address>(
                        new CommandDefinition(
                            sql,
                            new { CompanyId = companyId },
                            cancellationToken: cancellationToken)),
                "GetByCompanyAsync",
                new { ErrorMessage = $"Error retrieving Addresses associated with Company ID {companyId}", CompanyId = companyId, EntityType = nameof(Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets addresses by country
        /// </summary>
        /// <param name="country">The country name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses in the specified country</returns>
        /// <exception cref="ArgumentException">Thrown when country is null or empty</exception>
        public async Task<IEnumerable<Address>> GetByCountryAsync(string country, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country cannot be null or whitespace", nameof(country));

            const string sql = @"
                SELECT a.AddressId, a.AddressTypeId, a.Line1, a.Line2, a.City, a.StateId, a.Zip,
                       a.CreatedBy, a.CreatedDate, a.ModifiedBy, a.ModifiedDate, a.Active
                FROM Address a
                INNER JOIN StateProvince s ON a.StateId = s.StateProvinceId
                INNER JOIN Country c ON s.CountryId = c.CountryId
                WHERE c.Name = @Country AND a.Active = 1
                ORDER BY a.City, a.Line1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Address>>(
                async (connection) =>
                    await connection.QueryAsync<Address>(
                        new CommandDefinition(
                            sql,
                            new { Country = country },
                            cancellationToken: cancellationToken)),
                "GetByCountryAsync",
                new { ErrorMessage = $"Error retrieving Addresses in country '{country}'", Country = country, EntityType = nameof(Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets addresses associated with a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses associated with the specified person</returns>
        /// <exception cref="ArgumentException">Thrown when personId is empty</exception>
        public async Task<IEnumerable<Address>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            if (personId == Guid.Empty) throw new ArgumentException("The Person ID cannot be empty", nameof(personId));

            const string sql = @"
                SELECT a.AddressId, a.AddressTypeId, a.Line1, a.Line2, a.City, a.StateId, a.Zip,
                       a.CreatedBy, a.CreatedDate, a.ModifiedBy, a.ModifiedDate, a.Active
                FROM Address a
                INNER JOIN Person_Address pa ON a.AddressId = pa.AddressId
                WHERE pa.PersonId = @PersonId AND a.Active = 1
                ORDER BY a.AddressTypeId, a.Line1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Address>>(
                async (connection) =>
                    await connection.QueryAsync<Address>(
                        new CommandDefinition(
                            sql,
                            new { PersonId = personId },
                            cancellationToken: cancellationToken)),
                "GetByPersonAsync",
                new { ErrorMessage = $"Error retrieving Addresses associated with Person ID {personId}", PersonId = personId, EntityType = nameof(Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets addresses by state or province
        /// </summary>
        /// <param name="stateOrProvince">The state or province to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses in the specified state or province</returns>
        /// <exception cref="ArgumentException">Thrown when stateOrProvince is null or empty</exception>
        public async Task<IEnumerable<Address>> GetByStateOrProvinceAsync(string stateOrProvince, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(stateOrProvince))
                throw new ArgumentException("State or province cannot be null or whitespace", nameof(stateOrProvince));

            const string sql = @"
                SELECT a.AddressId, a.AddressTypeId, a.Line1, a.Line2, a.City, a.StateId, a.Zip,
                       a.CreatedBy, a.CreatedDate, a.ModifiedBy, a.ModifiedDate, a.Active
                FROM Address a
                INNER JOIN StateProvince s ON a.StateId = s.StateProvinceId
                WHERE s.Name = @StateOrProvince AND a.Active = 1
                ORDER BY a.City, a.Line1, a.Line2";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Address>>(
                async (connection) =>
                    await connection.QueryAsync<Address>(
                        new CommandDefinition(
                            sql,
                            new { StateOrProvince = stateOrProvince },
                            cancellationToken: cancellationToken)),
                "GetByStateOrProvinceAsync",
                new { ErrorMessage = $"Error retrieving Addresses in state or province '{stateOrProvince}'", StateOrProvince = stateOrProvince, EntityType = nameof(Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets addresses by postal code
        /// </summary>
        /// <param name="postalCode">The postal code to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses with the specified postal code</returns>
        /// <exception cref="ArgumentException">Thrown when postalCode is null or empty</exception>
        public async Task<IEnumerable<Address>> GetByPostalCodeAsync(string postalCode, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
                throw new ArgumentException("Postal code cannot be null or whitespace", nameof(postalCode));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Zip = @PostalCode AND Active = 1
                ORDER BY City, Line1, Line2";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Address>>(
                async (connection) =>
                    await connection.QueryAsync<Address>(
                        new CommandDefinition(
                            sql,
                            new { PostalCode = postalCode },
                            cancellationToken: cancellationToken)),
                "GetByPostalCodeAsync",
                new { ErrorMessage = $"Error retrieving Addresses with postal code '{postalCode}'", PostalCode = postalCode, EntityType = nameof(Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets addresses by state ID
        /// </summary>
        /// <param name="stateId">The state identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of addresses in the specified state</returns>
        /// <exception cref="ArgumentException">Thrown when stateId is empty</exception>
        public async Task<IEnumerable<Address>> GetByStateIdAsync(Guid stateId, CancellationToken cancellationToken = default)
        {
            if (stateId == Guid.Empty) throw new ArgumentException("The State ID cannot be empty", nameof(stateId));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE StateId = @StateId AND Active = 1
                ORDER BY City, Line1, Line2";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Address>>(
                async (connection) =>
                    await connection.QueryAsync<Address>(
                        new CommandDefinition(
                            sql,
                            new { StateId = stateId },
                            cancellationToken: cancellationToken)),
                "GetByStateIdAsync",
                new { ErrorMessage = $"Error retrieving Addresses with State ID {stateId}", StateId = stateId, EntityType = nameof(Address) },
                cancellationToken);
        }

        /// <summary>
        /// Loads the state entity for an address
        /// </summary>
        /// <param name="address">The address for which to load the state</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when address is null</exception>
        public async Task LoadStateAsync(Address address, CancellationToken cancellationToken = default)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            if (address.StateId == Guid.Empty) return;

            const string sql = @"
                SELECT s.StateId, s.Name, s.Abbreviation, s.CountryCode, s.OrdinalPosition,
                       s.CreatedBy, s.CreatedDate, s.ModifiedBy, s.ModifiedDate, s.Active
                FROM State s
                WHERE s.StateId = @StateId AND s.Active = 1";

            var state = await ExecuteWithResilienceAndLoggingAsync<State>(
                async (connection) =>
                    await connection.QuerySingleOrDefaultAsync<State>(
                        new CommandDefinition(
                            sql,
                            new { StateId = address.StateId },
                            cancellationToken: cancellationToken)),
                "LoadStateAsync",
                new { ErrorMessage = $"Error loading State for Address ID {address.AddressId}", AddressId = address.AddressId, StateId = address.StateId, EntityType = nameof(Address) },
                cancellationToken);

            address.State = state;
        }

        /// <summary>
        /// Loads the address type entity for an address
        /// </summary>
        /// <param name="address">The address for which to load the address type</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when address is null</exception>
        public async Task LoadAddressTypeAsync(Address address, CancellationToken cancellationToken = default)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            if (address.AddressTypeId == Guid.Empty) return;

            const string sql = @"
                SELECT at.AddressTypeId, at.Name, at.Description, at.OrdinalPosition,
                       at.CreatedBy, at.CreatedDate, at.ModifiedBy, at.ModifiedDate, at.Active
                FROM AddressType at
                WHERE at.AddressTypeId = @AddressTypeId AND at.Active = 1";

            var addressType = await ExecuteWithResilienceAndLoggingAsync<AddressType>(
                async (connection) =>
                    await connection.QuerySingleOrDefaultAsync<AddressType>(
                        new CommandDefinition(
                            sql,
                            new { AddressTypeId = address.AddressTypeId },
                            cancellationToken: cancellationToken)),
                "LoadAddressTypeAsync",
                new { ErrorMessage = $"Error loading AddressType for Address ID {address.AddressId}", AddressId = address.AddressId, AddressTypeId = address.AddressTypeId, EntityType = nameof(Address) },
                cancellationToken);

            address.AddressType = addressType;
        }

        /// <summary>
        /// Gets an address by ID with all related entities loaded
        /// </summary>
        /// <param name="id">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The address with all related entities loaded, or null if not found</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public async Task<Address?> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The ID cannot be empty", nameof(id));

            var address = await GetByIdAsync(id, cancellationToken);
            if (address == null) return null;

            await LoadStateAsync(address, cancellationToken);
            await LoadAddressTypeAsync(address, cancellationToken);

            return address;
        }

        /// <summary>
        /// Deletes an address by its unique identifier (soft delete)
        /// </summary>
        /// <param name="id">The unique identifier of the address to delete</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the address was successfully deleted, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The ID cannot be empty", nameof(id));

            // Use the BaseRepository soft delete pattern
            return await base.DeleteAsync(id, cancellationToken);
        }
    }
}