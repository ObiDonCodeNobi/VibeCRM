using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Person_Address junction entities
    /// </summary>
    public class PersonAddressRepository : BaseJunctionRepository<Person_Address, Guid, Guid>, IPersonAddressRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Person_Address";

        /// <summary>
        /// Gets the name of the first ID column (PersonId)
        /// </summary>
        protected override string FirstIdColumnName => "PersonId";

        /// <summary>
        /// Gets the name of the second ID column (AddressId)
        /// </summary>
        protected override string SecondIdColumnName => "AddressId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "PersonId", "AddressId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonAddressRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public PersonAddressRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PersonAddressRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all person-address relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-address relationships for the specified person</returns>
        public async Task<IEnumerable<Person_Address>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Address>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetByPersonIdAsync",
                new { PersonId = personId, EntityType = nameof(Person_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all person-address relationships for a specific address
        /// </summary>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-address relationships for the specified address</returns>
        public async Task<IEnumerable<Person_Address>> GetByAddressIdAsync(Guid addressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @AddressId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Address>(
                    new CommandDefinition(
                        sql,
                        new { AddressId = addressId },
                        cancellationToken: cancellationToken)),
                "GetByAddressIdAsync",
                new { AddressId = addressId, EntityType = nameof(Person_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a specific person-address relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person-address relationship if found, otherwise null</returns>
        public async Task<Person_Address?> GetByPersonAndAddressIdAsync(Guid personId, Guid addressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @AddressId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Person_Address>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, AddressId = addressId },
                        cancellationToken: cancellationToken)),
                "GetByPersonAndAddressIdAsync",
                new { PersonId = personId, AddressId = addressId, EntityType = nameof(Person_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets person-address relationships for a specified person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The collection of relationships</returns>
        public async Task<IEnumerable<Person_Address>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Address>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetByPersonAsync",
                new { PersonId = personId, EntityType = nameof(Person_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Adds a relationship between a person and an address
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created person-address relationship</returns>
        public async Task<Person_Address> AddRelationshipAsync(
            Guid personId,
            Guid addressId,
            CancellationToken cancellationToken = default)
        {
            var entity = new Person_Address
            {
                PersonId = personId,
                AddressId = addressId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            var sql = $@"
                INSERT INTO {TableName} ({string.Join(", ", SelectColumns)})
                VALUES (@PersonId, @AddressId, @Active, @ModifiedDate)";

            var parameters = new
            {
                PersonId = personId,
                AddressId = addressId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "AddRelationshipAsync",
                new { PersonId = personId, AddressId = addressId, EntityType = nameof(Person_Address) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates the record with a timestamp to indicate when it was last modified
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if successful, otherwise false</returns>
        public async Task<bool> SetAsPrimaryAsync(Guid personId, Guid addressId, CancellationToken cancellationToken = default)
        {
            // Update timestamp on the specified address
            var sql = $@"
                UPDATE {TableName}
                SET ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @AddressId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                AddressId = addressId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "SetAsPrimaryAsync",
                new { PersonId = personId, AddressId = addressId, EntityType = nameof(Person_Address) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Gets a person-address relationship by composite ID
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated person-address relationship or null if the operation fails</returns>
        public async Task<Person_Address?> UpdateTimestampAsync(Guid personId, Guid addressId, CancellationToken cancellationToken = default)
        {
            // Update timestamp using existing method
            var success = await SetAsPrimaryAsync(personId, addressId, cancellationToken);

            if (!success)
            {
                return null;
            }

            // Return the updated relationship
            return await GetByPersonAndAddressIdAsync(personId, addressId, cancellationToken);
        }

        /// <summary>
        /// Removes a relationship between a person and an address
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid personId, Guid addressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @AddressId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                AddressId = addressId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveRelationshipAsync",
                new { PersonId = personId, AddressId = addressId, EntityType = nameof(Person_Address) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-address relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPersonIdAsync",
                new { PersonId = personId, EntityType = nameof(Person_Address) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-address relationships for a specific address
        /// </summary>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByAddressIdAsync(Guid addressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @AddressId
                AND Active = 1";

            var parameters = new
            {
                AddressId = addressId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByAddressIdAsync",
                new { AddressId = addressId, EntityType = nameof(Person_Address) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Removes all relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                ModifiedDate = DateTime.UtcNow
            };

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveAllForPersonAsync",
                new { PersonId = personId, EntityType = nameof(Person_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Removes all relationships for a specific address
        /// </summary>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForAddressAsync(Guid addressId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @AddressId
                AND Active = 1";

            var parameters = new
            {
                AddressId = addressId,
                ModifiedDate = DateTime.UtcNow
            };

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveAllForAddressAsync",
                new { AddressId = addressId, EntityType = nameof(Person_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets the primary address for a person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The primary person-address relationship if found, otherwise null</returns>
        public async Task<Person_Address?> GetPrimaryAddressForPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT TOP 1 {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Person_Address>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetPrimaryAddressForPersonAsync",
                new { PersonId = personId, EntityType = nameof(Person_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Sets an address as the primary address for a person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="addressId">The address identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated person-address relationship if successful, otherwise null</returns>
        public async Task<Person_Address?> SetPrimaryAddressAsync(Guid personId, Guid addressId, CancellationToken cancellationToken = default)
        {
            // Update timestamp to indicate this record was modified
            const string sql = @"
                UPDATE Person_Address
                SET ModifiedDate = @ModifiedDate
                WHERE PersonId = @PersonId AND AddressId = @AddressId AND Active = 1;

                SELECT pa.PersonId, pa.AddressId, pa.CreatedBy, pa.CreatedDate, pa.ModifiedBy, pa.ModifiedDate, pa.Active
                FROM Person_Address pa
                WHERE pa.PersonId = @PersonId AND pa.AddressId = @AddressId AND pa.Active = 1;";

            var parameters = new
            {
                PersonId = personId,
                AddressId = addressId,
                ModifiedDate = DateTime.UtcNow
            };

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Person_Address>(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "SetPrimaryAddressAsync",
                new { PersonId = personId, AddressId = addressId, EntityType = nameof(Person_Address) },
                cancellationToken);
        }

        /// <summary>
        /// Gets person-address relationships by address type
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="addressTypeId">The address type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>Person-address relationships for the specified person</returns>
        public async Task<IEnumerable<Person_Address>> GetByAddressTypeAsync(Guid personId, Guid addressTypeId, CancellationToken cancellationToken = default)
        {
            // Since Person_Address doesn't have address type, return all addresses for the person
            return await GetByPersonIdAsync(personId, cancellationToken);
        }
    }
}