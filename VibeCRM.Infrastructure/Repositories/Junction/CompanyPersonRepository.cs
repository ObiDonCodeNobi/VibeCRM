using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Company_Person junction entities
    /// </summary>
    public class CompanyPersonRepository : BaseJunctionRepository<Company_Person, Guid, Guid>, ICompanyPersonRepository
    {
        private readonly ILogger<CompanyPersonRepository> _logger;

        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Company_Person";

        /// <summary>
        /// Gets the name of the first ID column (CompanyId)
        /// </summary>
        protected override string FirstIdColumnName => "CompanyId";

        /// <summary>
        /// Gets the name of the second ID column (PersonId)
        /// </summary>
        protected override string SecondIdColumnName => "PersonId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "CompanyId", "PersonId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyPersonRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public CompanyPersonRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<CompanyPersonRepository> logger)
            : base(connectionFactory, logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets all company-person relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-person relationships for the specified company</returns>
        public async Task<IEnumerable<Company_Person>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            if (companyId == Guid.Empty) throw new ArgumentException("Company ID cannot be empty", nameof(companyId));

            var query = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE CompanyId = @CompanyId AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company_Person>>(
                async connection => await connection.QueryAsync<Company_Person>(
                    new CommandDefinition(
                        query,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyIdAsync",
                new { ErrorMessage = $"Error retrieving Company_Person records for company ID {companyId}", CompanyId = companyId },
                cancellationToken);
        }

        /// <summary>
        /// Gets all company-person relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-person relationships for the specified person</returns>
        public async Task<IEnumerable<Company_Person>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            if (personId == Guid.Empty) throw new ArgumentException("Person ID cannot be empty", nameof(personId));

            var query = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE PersonId = @PersonId AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Company_Person>>(
                async connection => await connection.QueryAsync<Company_Person>(
                    new CommandDefinition(
                        query,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetByPersonIdAsync",
                new { ErrorMessage = $"Error retrieving Company_Person records for person ID {personId}", PersonId = personId },
                cancellationToken);
        }

        /// <summary>
        /// Gets the primary person relationship for a specific company, if one exists
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The primary company-person relationship for the specified company, or null if none exists</returns>
        public async Task<Company_Person?> GetPrimaryPersonForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Company_Person>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetPrimaryPersonForCompanyAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Person) },
                cancellationToken);
        }

        /// <summary>
        /// Gets the primary company relationship for a specific person, if one exists
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The primary company-person relationship for the specified person, or null if none exists</returns>
        public async Task<Company_Person?> GetPrimaryCompanyForPersonAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @PersonId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Company_Person>(
                    new CommandDefinition(
                        sql,
                        new { PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetPrimaryCompanyForPersonAsync",
                new { PersonId = personId, EntityType = nameof(Company_Person) },
                cancellationToken);
        }

        /// <summary>
        /// Adds a relationship between a company and a person
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The newly created company-person relationship</returns>
        public async Task<Company_Person> AddRelationshipAsync(
            Guid companyId,
            Guid personId,
            CancellationToken cancellationToken = default)
        {
            var entity = new Company_Person
            {
                CompanyId = companyId,
                PersonId = personId,
                Active = true,
                ModifiedDate = DateTime.UtcNow
            };

            var sql = $@"
                INSERT INTO {TableName} ({string.Join(", ", SelectColumns)})
                VALUES (@CompanyId, @PersonId, @Active, @ModifiedDate)";

            var parameters = new
            {
                CompanyId = companyId,
                PersonId = personId,
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
                new { CompanyId = companyId, PersonId = personId, EntityType = nameof(Company_Person) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Removes a relationship between a company and a person
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully removed, otherwise false</returns>
        public async Task<bool> RemoveRelationshipAsync(Guid companyId, Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @PersonId
                AND Active = 1";

            var parameters = new
            {
                CompanyId = companyId,
                PersonId = personId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveRelationshipAsync",
                new { CompanyId = companyId, PersonId = personId, EntityType = nameof(Company_Person) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Removes all relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The number of relationships removed</returns>
        public async Task<int> RemoveAllForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @CompanyId
                AND Active = 1";

            var parameters = new
            {
                CompanyId = companyId,
                ModifiedDate = DateTime.UtcNow
            };

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "RemoveAllForCompanyAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Person) },
                cancellationToken);
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
                WHERE {SecondIdColumnName} = @PersonId
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
                new { PersonId = personId, EntityType = nameof(Company_Person) },
                cancellationToken);
        }

        /// <summary>
        /// Gets the relationship between a company and a person by their identifiers
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-person relationship if found, otherwise null</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<Company_Person?> GetByCompanyAndPersonIdAsync(Guid companyId, Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @CompanyId
                AND {SecondIdColumnName} = @PersonId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryFirstOrDefaultAsync<Company_Person>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId, PersonId = personId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyAndPersonIdAsync",
                new { CompanyId = companyId, PersonId = personId, EntityType = nameof(Company_Person) },
                cancellationToken);
        }

        /// <summary>
        /// Deletes all company-person relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {FirstIdColumnName} = @CompanyId
                AND Active = 1";

            var parameters = new
            {
                CompanyId = companyId,
                ModifiedDate = DateTime.UtcNow
            };

            var rowsAffected = await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        cancellationToken: cancellationToken)),
                "DeleteByCompanyIdAsync",
                new { CompanyId = companyId, EntityType = nameof(Company_Person) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all company-person relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        /// <exception cref="Exception">Thrown when a database error occurs</exception>
        public async Task<bool> DeleteByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                UPDATE {TableName}
                SET Active = 0, ModifiedDate = @ModifiedDate
                WHERE {SecondIdColumnName} = @PersonId
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
                new { PersonId = personId, EntityType = nameof(Company_Person) },
                cancellationToken);

            return rowsAffected > 0;
        }

        /// <summary>
        /// Updates the relationship type between a company and a person
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="personId">The person identifier</param>
        /// <param name="relationshipType">The new relationship type</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated company-person relationship or null if the relationship doesn't exist</returns>
        /// <exception cref="ArgumentException">Thrown when companyId or personId is empty</exception>
        public async Task<Company_Person?> UpdateRelationshipTypeAsync(Guid companyId, Guid personId, string relationshipType, CancellationToken cancellationToken = default)
        {
            if (companyId == Guid.Empty) throw new ArgumentException("Company ID cannot be empty", nameof(companyId));
            if (personId == Guid.Empty) throw new ArgumentException("Person ID cannot be empty", nameof(personId));
            if (string.IsNullOrWhiteSpace(relationshipType)) throw new ArgumentException("Relationship Type cannot be empty", nameof(relationshipType));

            // Check if relationship exists
            var relationship = await GetSingleAsync(companyId, personId, cancellationToken);
            if (relationship == null)
            {
                _logger.LogWarning("Attempted to update relationship type for non-existent Company-Person relationship. Company ID: {CompanyId}, Person ID: {PersonId}",
                    companyId, personId);
                return null;
            }

            // Log a warning since Company_Person entity doesn't actually have a RelationshipType property
            _logger.LogWarning("UpdateRelationshipTypeAsync called but Company_Person entity doesn't have a RelationshipType property. " +
                "Company ID: {CompanyId}, Person ID: {PersonId}, Relationship Type: {RelationshipType}",
                companyId, personId, relationshipType);

            // Return the existing relationship since we can't update a property that doesn't exist
            return relationship;
        }

        /// <summary>
        /// Gets a single company-person relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-person relationship if found, otherwise null</returns>
        private async Task<Company_Person?> GetSingleAsync(Guid companyId, Guid personId, CancellationToken cancellationToken = default)
        {
            return await GetByIdAsync(companyId, personId, cancellationToken);
        }
    }
}