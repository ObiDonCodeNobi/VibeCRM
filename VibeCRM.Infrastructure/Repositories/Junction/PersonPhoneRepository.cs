using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Person_Phone junction entities
    /// </summary>
    public class PersonPhoneRepository : BaseJunctionRepository<Person_Phone, Guid, Guid>, IPersonPhoneRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Person_Phone";

        /// <summary>
        /// Gets the name of the first ID column (PersonId)
        /// </summary>
        protected override string FirstIdColumnName => "PersonId";

        /// <summary>
        /// Gets the name of the second ID column (PhoneId)
        /// </summary>
        protected override string SecondIdColumnName => "PhoneId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "PersonId", "PhoneId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonPhoneRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public PersonPhoneRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<PersonPhoneRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all person-phone relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-phone relationships for the specified person</returns>
        public async Task<IEnumerable<Person_Phone>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Phone>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetByPersonIdAsync",
                new { PersonId = personId, EntityType = nameof(Person_Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all person-phone relationships for a specific phone
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-phone relationships for the specified phone</returns>
        public async Task<IEnumerable<Person_Phone>> GetByPhoneIdAsync(Guid phoneId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @PhoneId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Phone>(
                    new CommandDefinition(
                        sql,
                        new { PhoneId = phoneId },
                        cancellationToken: cancellationToken)),
                "GetByPhoneIdAsync",
                new { PhoneId = phoneId, EntityType = nameof(Person_Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Gets person-phone relationships for a specified person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The collection of relationships</returns>
        public async Task<IEnumerable<Person_Phone>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Person_Phone>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetByPersonAsync",
                new { PersonId = personId, EntityType = nameof(Person_Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a relationship between the specified person and phone exists
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a relationship exists, otherwise false</returns>
        public async Task<bool> ExistsByPersonAndPhoneAsync(Guid personId, Guid phoneId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM {TableName}
                    WHERE {FirstIdColumnName} = @PersonId
                      AND {SecondIdColumnName} = @PhoneId
                      AND Active = 1
                ) THEN 1 ELSE 0 END";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, PhoneId = phoneId },
                        cancellationToken: cancellationToken)),
                "ExistsByPersonAndPhoneAsync",
                new { PersonId = personId, PhoneId = phoneId, EntityType = nameof(Person_Phone) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Gets the primary phone relationship for a specific person, if one exists
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The primary person-phone relationship for the specified person, or null if none exists</returns>
        public async Task<Person_Phone?> GetPrimaryPhoneForPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND IsPrimary = 1
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Person_Phone>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetPrimaryPhoneForPersonAsync",
                new { PersonId = personId, EntityType = nameof(Person_Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Adds a relationship between a person and a phone
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created person-phone relationship</returns>
        public async Task<Person_Phone> AddRelationshipAsync(
            Guid personId,
            Guid phoneId,
            CancellationToken cancellationToken = default)
        {
            var entity = new Person_Phone
            {
                PersonId = personId,
                PhoneId = phoneId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            var sql = $@"
                INSERT INTO {TableName} ({string.Join(", ", SelectColumns)})
                VALUES (@PersonId, @PhoneId, @Active, @ModifiedDate)";

            var parameters = new
            {
                PersonId = personId,
                PhoneId = phoneId,
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
                new { PersonId = personId, PhoneId = phoneId, EntityType = nameof(Person_Phone) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates the record with a timestamp to indicate when it was last modified
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if successful, otherwise false</returns>
        public async Task<bool> SetAsPrimaryAsync(Guid personId, Guid phoneId, CancellationToken cancellationToken = default)
        {
            // Set the specified phone as primary
            var sql = $@"
                UPDATE {TableName}
                SET ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @PhoneId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                PhoneId = phoneId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "SetAsPrimaryAsync",
                new { PersonId = personId, PhoneId = phoneId, EntityType = nameof(Person_Phone) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Sets a phone as the primary phone for a person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated person-phone relationship if successful, otherwise null</returns>
        public async Task<Person_Phone?> SetPrimaryPhoneAsync(Guid personId, Guid phoneId, CancellationToken cancellationToken = default)
        {
            await SetAsPrimaryAsync(personId, phoneId, cancellationToken);

            // Return the updated entity
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @PhoneId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Person_Phone>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId, PhoneId = phoneId },
                        cancellationToken: cancellationToken)),
                "SetPrimaryPhoneAsync_GetEntity",
                new { PersonId = personId, PhoneId = phoneId, EntityType = nameof(Person_Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Removes a relationship between a person and a phone
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid personId, Guid phoneId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @PhoneId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                PhoneId = phoneId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveRelationshipAsync",
                new { PersonId = personId, PhoneId = phoneId, EntityType = nameof(Person_Phone) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-phone relationships for a specific person
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
                new { PersonId = personId, EntityType = nameof(Person_Phone) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all person-phone relationships for a specific phone
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        public async Task<bool> DeleteByPhoneIdAsync(Guid phoneId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @PhoneId
                AND Active = 1";

            var parameters = new
            {
                PhoneId = phoneId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPhoneIdAsync",
                new { PhoneId = phoneId, EntityType = nameof(Person_Phone) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes a specific relationship between a person and a phone
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was deleted, otherwise false</returns>
        public async Task<bool> DeleteByPersonAndPhoneAsync(Guid personId, Guid phoneId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @PhoneId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                PhoneId = phoneId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByPersonAndPhoneAsync",
                new { PersonId = personId, PhoneId = phoneId, EntityType = nameof(Person_Phone) },
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
                new { PersonId = personId, EntityType = nameof(Person_Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Removes all relationships for a specific phone
        /// </summary>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForPhoneAsync(Guid phoneId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @PhoneId
                AND Active = 1";

            var parameters = new
            {
                PhoneId = phoneId,
                ModifiedDate = DateTime.UtcNow
            };

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveAllForPhoneAsync",
                new { PhoneId = phoneId, EntityType = nameof(Person_Phone) },
                cancellationToken);
        }

        /// <summary>
        /// Updates the phone type for a person-phone relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="phoneId">The phone identifier</param>
        /// <param name="phoneTypeId">The new phone type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the update was successful, otherwise false</returns>
        public async Task<bool> UpdatePhoneTypeAsync(Guid personId, Guid phoneId, Guid? phoneTypeId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET PhoneTypeId = @PhoneTypeId,
                    ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @PersonId
                AND {SecondIdColumnName} = @PhoneId
                AND Active = 1";

            var parameters = new
            {
                PersonId = personId,
                PhoneId = phoneId,
                PhoneTypeId = phoneTypeId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "UpdatePhoneTypeAsync",
                new { PersonId = personId, PhoneId = phoneId, PhoneTypeId = phoneTypeId, EntityType = nameof(Person_Phone) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Gets person-phone relationships by phone type, implemented to maintain interface compatibility
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="phoneTypeId">The phone type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>Person-phone relationships for the specified person</returns>
        /// <remarks>
        /// This method is required by the interface but the Person_Phone entity doesn't have a
        /// concept of "phone type", so it returns all phone relationships for the person
        /// </remarks>
        public async Task<IEnumerable<Person_Phone>> GetByPhoneTypeAsync(Guid personId, Guid phoneTypeId, CancellationToken cancellationToken = default)
        {
            // Since Person_Phone doesn't have phone type, return all phones for the person
            return await GetByPersonIdAsync(personId, cancellationToken);
        }
    }
}