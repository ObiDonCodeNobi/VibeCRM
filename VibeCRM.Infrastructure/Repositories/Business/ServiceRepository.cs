using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.BusinessEntities;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Business
{
    /// <summary>
    /// Repository implementation for managing Service entities
    /// </summary>
    public class ServiceRepository : BaseRepository<Service, Guid>, IServiceRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Service";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "ServiceId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "ServiceId", "ServiceTypeId", "Name", "Description",
            "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Base select query used by multiple methods
        /// </summary>
        private const string BaseSelectQuery = @"
            SELECT
                s.ServiceId, s.ServiceTypeId, s.Name, s.Description,
                s.CreatedBy, s.CreatedDate, s.ModifiedBy, s.ModifiedDate, s.Active,
                st.ServiceTypeId, st.Type, st.Description, st.OrdinalPosition,
                st.CreatedBy, st.CreatedDate, st.ModifiedBy, st.ModifiedDate, st.Active
            FROM Service s
            LEFT JOIN ServiceType st ON s.ServiceTypeId = st.ServiceTypeId
            WHERE s.Active = 1";

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public ServiceRepository(ISQLConnectionFactory connectionFactory, ILogger<ServiceRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new service to the repository
        /// </summary>
        /// <param name="entity">The service to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added service with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when ServiceId is empty</exception>
        public override async Task<Service> AddAsync(Service entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Id == Guid.Empty) throw new ArgumentException("The Service ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO Service (
                    ServiceId, ServiceTypeId, Name, Description,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                )
                VALUES (
                    @Id, @ServiceTypeId, @Name, @Description,
                    @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active
                );";

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            new
                            {
                                entity.Id,
                                entity.ServiceTypeId,
                                entity.Name,
                                entity.Description,
                                entity.CreatedBy,
                                entity.CreatedDate,
                                entity.ModifiedBy,
                                entity.ModifiedDate,
                                entity.Active
                            },
                            cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding Service with ID {entity.Id}", ServiceId = entity.Id, EntityType = nameof(Service) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing service in the repository
        /// </summary>
        /// <param name="entity">The service to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated service</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        /// <exception cref="ArgumentException">Thrown when ServiceId is empty</exception>
        public override async Task<Service> UpdateAsync(Service entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Id == Guid.Empty) throw new ArgumentException("The Service ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE Service
                SET
                    ServiceTypeId = @ServiceTypeId,
                    Name = @Name,
                    Description = @Description,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate,
                    Active = @Active
                WHERE ServiceId = @Id
                AND Active = 1";

            var affectedRows = await ExecuteWithResilienceAndLoggingAsync<int>(
                async (connection) =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            new
                            {
                                entity.Id,
                                entity.ServiceTypeId,
                                entity.Name,
                                entity.Description,
                                entity.ModifiedBy,
                                entity.ModifiedDate,
                                entity.Active
                            },
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating Service with ID {entity.Id}", ServiceId = entity.Id, EntityType = nameof(Service) },
                cancellationToken);

            if (affectedRows == 0)
            {
                _logger.LogWarning("No Service was updated for ID {ServiceId}", entity.Id);
            }

            return entity;
        }

        /// <summary>
        /// Gets all services from the repository
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of all services in the repository</returns>
        public override async Task<IEnumerable<Service>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Service>>(
                async (connection) =>
                {
                    var result = await connection.QueryAsync<Service, Domain.Entities.TypeStatusEntities.ServiceType, Service>(
                        new CommandDefinition(
                            BaseSelectQuery,
                            cancellationToken: cancellationToken),
                        (service, serviceType) =>
                        {
                            service.ServiceType = serviceType;
                            return service;
                        },
                        splitOn: "ServiceTypeId");

                    return result ?? Enumerable.Empty<Service>();
                },
                "GetAllAsync",
                new { ErrorMessage = "Error getting all Services", EntityType = nameof(Service) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a service by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the service</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The service if found, otherwise null</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Service ID cannot be empty", nameof(id));

            var sql = $"{BaseSelectQuery} AND s.ServiceId = @ServiceId";

            return await ExecuteWithResilienceAndLoggingAsync<Service?>(
                async (connection) =>
                {
                    var result = await connection.QueryAsync<Service, Domain.Entities.TypeStatusEntities.ServiceType, Service>(
                        new CommandDefinition(
                            sql,
                            new { ServiceId = id },
                            cancellationToken: cancellationToken),
                        (service, serviceType) =>
                        {
                            service.ServiceType = serviceType;
                            return service;
                        },
                        splitOn: "ServiceTypeId");

                    return result.FirstOrDefault();
                },
                "GetByIdAsync",
                new { ErrorMessage = $"Error getting Service with ID {id}", ServiceId = id, EntityType = nameof(Service) },
                cancellationToken);
        }

        /// <summary>
        /// Gets services by their type
        /// </summary>
        /// <param name="serviceTypeId">The service type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of services with the specified type</returns>
        /// <exception cref="ArgumentException">Thrown when serviceTypeId is empty</exception>
        public async Task<IEnumerable<Service>> GetByServiceTypeAsync(Guid serviceTypeId, CancellationToken cancellationToken = default)
        {
            if (serviceTypeId == Guid.Empty) throw new ArgumentException("The Service Type ID cannot be empty", nameof(serviceTypeId));

            var sql = $"{BaseSelectQuery} AND s.ServiceTypeId = @ServiceTypeId";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Service>>(
                async (connection) =>
                {
                    var result = await connection.QueryAsync<Service, Domain.Entities.TypeStatusEntities.ServiceType, Service>(
                        new CommandDefinition(
                            sql,
                            new { ServiceTypeId = serviceTypeId },
                            cancellationToken: cancellationToken),
                        (service, serviceType) =>
                        {
                            service.ServiceType = serviceType;
                            return service;
                        },
                        splitOn: "ServiceTypeId");

                    return result ?? Enumerable.Empty<Service>();
                },
                "GetByServiceTypeAsync",
                new { ErrorMessage = $"Error getting Services with ServiceTypeId {serviceTypeId}", ServiceTypeId = serviceTypeId, EntityType = nameof(Service) },
                cancellationToken);
        }

        /// <summary>
        /// Gets services by name or partial name match
        /// </summary>
        /// <param name="name">The name or partial name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of services matching the specified name pattern</returns>
        /// <exception cref="ArgumentNullException">Thrown when name is null</exception>
        public async Task<IEnumerable<Service>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

            var sql = $"{BaseSelectQuery} AND s.Name LIKE @Name";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Service>>(
                async (connection) =>
                {
                    var result = await connection.QueryAsync<Service, Domain.Entities.TypeStatusEntities.ServiceType, Service>(
                        new CommandDefinition(
                            sql,
                            new { Name = $"%{name}%" },
                            cancellationToken: cancellationToken),
                        (service, serviceType) =>
                        {
                            service.ServiceType = serviceType;
                            return service;
                        },
                        splitOn: "ServiceTypeId");

                    return result ?? Enumerable.Empty<Service>();
                },
                "GetByNameAsync",
                new { ErrorMessage = $"Error getting Services with Name like {name}", Name = name, EntityType = nameof(Service) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if a service exists with the given name
        /// </summary>
        /// <param name="name">The service name to check</param>
        /// <param name="excludeId">Optional service ID to exclude from the check (for updates)</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a service with the name exists, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown when name is null</exception>
        public async Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

            string sql = @"
                SELECT COUNT(1)
                FROM Service
                WHERE Name = @Name
                AND Active = 1";

            if (excludeId.HasValue && excludeId != Guid.Empty)
            {
                sql += " AND ServiceId <> @ExcludeId";
            }

            return await ExecuteWithResilienceAndLoggingAsync<bool>(
                async (connection) =>
                {
                    var count = await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            new { Name = name, ExcludeId = excludeId },
                            cancellationToken: cancellationToken));

                    return count > 0;
                },
                "ExistsByNameAsync",
                new { ErrorMessage = $"Error checking if Service exists with Name {name}", Name = name, ExcludeId = excludeId, EntityType = nameof(Service) },
                cancellationToken);
        }

        /// <summary>
        /// Gets active services ordered by name
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of active services ordered by name</returns>
        public async Task<IEnumerable<Service>> GetActiveOrderedAsync(CancellationToken cancellationToken = default)
        {
            var sql = $"{BaseSelectQuery} ORDER BY s.Name";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Service>>(
                async (connection) =>
                {
                    var result = await connection.QueryAsync<Service, Domain.Entities.TypeStatusEntities.ServiceType, Service>(
                        new CommandDefinition(
                            sql,
                            cancellationToken: cancellationToken),
                        (service, serviceType) =>
                        {
                            service.ServiceType = serviceType;
                            return service;
                        },
                        splitOn: "ServiceTypeId");

                    return result ?? Enumerable.Empty<Service>();
                },
                "GetActiveOrderedAsync",
                new { ErrorMessage = "Error getting active Services ordered by name", EntityType = nameof(Service) },
                cancellationToken);
        }

        /// <summary>
        /// Checks if an entity with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if an entity with the specified ID exists, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Service ID cannot be empty", nameof(id));

            const string sql = @"
                SELECT COUNT(1)
                FROM Service
                WHERE ServiceId = @ServiceId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync<bool>(
                async (connection) =>
                {
                    var count = await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            new { ServiceId = id },
                            cancellationToken: cancellationToken));

                    return count > 0;
                },
                "ExistsAsync",
                new { ErrorMessage = $"Error checking if Service exists with ID {id}", ServiceId = id, EntityType = nameof(Service) },
                cancellationToken);
        }

        /// <summary>
        /// Loads quote line items associated with a service
        /// </summary>
        /// <param name="service">The service to load quote line items for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when service is null</exception>
        /// <exception cref="ArgumentException">Thrown when service ID is empty</exception>
        public async Task LoadQuoteLineItemsAsync(Service service, CancellationToken cancellationToken = default)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (service.Id == Guid.Empty) throw new ArgumentException("The Service ID cannot be empty", nameof(service));

            const string sql = @"
                SELECT
                    qli.QuoteLineItemId, qli.QuoteId, qli.ProductId, qli.ServiceId,
                    qli.Description, qli.Quantity, qli.UnitPrice, qli.DiscountPercentage,
                    qli.DiscountAmount, qli.TaxPercentage, qli.LineNumber, qli.Notes,
                    qli.CreatedBy, qli.CreatedDate, qli.ModifiedBy, qli.ModifiedDate, qli.Active
                FROM QuoteLineItem qli
                WHERE qli.ServiceId = @ServiceId
                AND qli.Active = 1";

            await ExecuteWithResilienceAndLoggingAsync<IEnumerable<QuoteLineItem>>(
                async (connection) =>
                {
                    var quoteLineItems = await connection.QueryAsync<QuoteLineItem>(
                        new CommandDefinition(
                            sql,
                            new { ServiceId = service.Id },
                            cancellationToken: cancellationToken));

                    if (quoteLineItems != null && quoteLineItems.Any())
                    {
                        service.QuoteLineItems = quoteLineItems.ToList();
                    }
                    else
                    {
                        service.QuoteLineItems = new List<QuoteLineItem>();
                    }

                    return quoteLineItems;
                },
                "LoadQuoteLineItemsAsync",
                new { ErrorMessage = $"Error loading QuoteLineItems for Service with ID {service.Id}", ServiceId = service.Id, EntityType = nameof(Service) },
                cancellationToken);
        }

        /// <summary>
        /// Loads invoice line items associated with a service
        /// </summary>
        /// <param name="service">The service to load invoice line items for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when service is null</exception>
        /// <exception cref="ArgumentException">Thrown when service ID is empty</exception>
        public async Task LoadInvoiceLineItemsAsync(Service service, CancellationToken cancellationToken = default)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (service.Id == Guid.Empty) throw new ArgumentException("The Service ID cannot be empty", nameof(service));

            const string sql = @"
                SELECT
                    ili.InvoiceLineItemId, ili.InvoiceId, ili.ProductId, ili.ServiceId,
                    ili.SalesOrderLineItemId, ili.Description, ili.Quantity, ili.UnitPrice,
                    ili.DiscountPercentage, ili.DiscountAmount, ili.TaxPercentage,
                    ili.LineNumber, ili.Notes,
                    ili.CreatedBy, ili.CreatedDate, ili.ModifiedBy, ili.ModifiedDate, ili.Active
                FROM InvoiceLineItem ili
                WHERE ili.ServiceId = @ServiceId
                AND ili.Active = 1";

            await ExecuteWithResilienceAndLoggingAsync<IEnumerable<InvoiceLineItem>>(
                async (connection) =>
                {
                    var invoiceLineItems = await connection.QueryAsync<InvoiceLineItem>(
                        new CommandDefinition(
                            sql,
                            new { ServiceId = service.Id },
                            cancellationToken: cancellationToken));

                    if (invoiceLineItems != null && invoiceLineItems.Any())
                    {
                        service.InvoiceLineItems = invoiceLineItems.ToList();
                    }
                    else
                    {
                        service.InvoiceLineItems = new List<InvoiceLineItem>();
                    }

                    return invoiceLineItems;
                },
                "LoadInvoiceLineItemsAsync",
                new { ErrorMessage = $"Error loading InvoiceLineItems for Service with ID {service.Id}", ServiceId = service.Id, EntityType = nameof(Service) },
                cancellationToken);
        }

        /// <summary>
        /// Loads sales order line item service relationships associated with a service
        /// </summary>
        /// <param name="service">The service to load sales order line item service relationships for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when service is null</exception>
        /// <exception cref="ArgumentException">Thrown when service ID is empty</exception>
        public async Task LoadSalesOrderLineItemServicesAsync(Service service, CancellationToken cancellationToken = default)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (service.Id == Guid.Empty) throw new ArgumentException("The Service ID cannot be empty", nameof(service));

            const string sql = @"
                SELECT
                    solis.SalesOrderLineItemId, solis.ServiceId, solis.Active, solis.ModifiedDate
                FROM SalesOrderLineItem_Service solis
                WHERE solis.ServiceId = @ServiceId
                AND solis.Active = 1";

            await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrderLineItem_Service>>(
                async (connection) =>
                {
                    var salesOrderLineItemServices = await connection.QueryAsync<SalesOrderLineItem_Service>(
                        new CommandDefinition(
                            sql,
                            new { ServiceId = service.Id },
                            cancellationToken: cancellationToken));

                    if (salesOrderLineItemServices != null && salesOrderLineItemServices.Any())
                    {
                        service.SalesOrderLineItemServices = salesOrderLineItemServices.ToList();
                    }
                    else
                    {
                        service.SalesOrderLineItemServices = new List<SalesOrderLineItem_Service>();
                    }

                    return salesOrderLineItemServices;
                },
                "LoadSalesOrderLineItemServicesAsync",
                new { ErrorMessage = $"Error loading SalesOrderLineItemServices for Service with ID {service.Id}", ServiceId = service.Id, EntityType = nameof(Service) },
                cancellationToken);
        }

        /// <summary>
        /// Loads sales order line items associated with a service
        /// </summary>
        /// <param name="service">The service to load sales order line items for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when service is null</exception>
        /// <exception cref="ArgumentException">Thrown when service ID is empty</exception>
        public async Task LoadSalesOrderLineItemsAsync(Service service, CancellationToken cancellationToken = default)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (service.Id == Guid.Empty) throw new ArgumentException("The Service ID cannot be empty", nameof(service));

            const string sql = @"
                SELECT
                    soli.SalesOrderLineItemId, soli.SalesOrderId, soli.ProductId, soli.ServiceId,
                    soli.QuoteLineItemId, soli.Description, soli.Quantity, soli.UnitPrice,
                    soli.DiscountPercentage, soli.DiscountAmount, soli.TaxPercentage,
                    soli.LineNumber, soli.Notes,
                    soli.CreatedBy, soli.CreatedDate, soli.ModifiedBy, soli.ModifiedDate, soli.Active
                FROM SalesOrderLineItem soli
                WHERE soli.ServiceId = @ServiceId
                AND soli.Active = 1";

            await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrderLineItem>>(
                async (connection) =>
                {
                    var salesOrderLineItems = await connection.QueryAsync<SalesOrderLineItem>(
                        new CommandDefinition(
                            sql,
                            new { ServiceId = service.Id },
                            cancellationToken: cancellationToken));

                    if (salesOrderLineItems != null && salesOrderLineItems.Any())
                    {
                        service.SalesOrderLineItems = salesOrderLineItems.ToList();
                    }
                    else
                    {
                        service.SalesOrderLineItems = new List<SalesOrderLineItem>();
                    }

                    return salesOrderLineItems;
                },
                "LoadSalesOrderLineItemsAsync",
                new { ErrorMessage = $"Error loading SalesOrderLineItems for Service with ID {service.Id}", ServiceId = service.Id, EntityType = nameof(Service) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a service by ID with all related entities loaded
        /// </summary>
        /// <param name="id">The unique identifier of the service to retrieve</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The service with all related entities loaded if found, otherwise null</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public async Task<Service?> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Service ID cannot be empty", nameof(id));

            _logger.LogInformation("Getting Service with ID {ServiceId} and all related entities", id);

            try
            {
                // Get the service with its service type
                var service = await GetByIdAsync(id, cancellationToken);
                if (service == null)
                {
                    _logger.LogWarning("Service with ID {ServiceId} not found", id);
                    return null;
                }

                // Load all related entities
                await LoadQuoteLineItemsAsync(service, cancellationToken);
                await LoadInvoiceLineItemsAsync(service, cancellationToken);
                await LoadSalesOrderLineItemServicesAsync(service, cancellationToken);
                await LoadSalesOrderLineItemsAsync(service, cancellationToken);

                _logger.LogInformation("Successfully loaded Service with ID {ServiceId} and all related entities", id);
                return service;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Service with ID {ServiceId} and related entities", id);
                throw;
            }
        }

        /// <summary>
        /// Gets all active services with related entities loaded
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of active services with all related entities loaded</returns>
        public async Task<IEnumerable<Service>> GetAllWithRelatedEntitiesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                // Get all active services
                var services = await GetActiveOrderedAsync(cancellationToken);
                var servicesList = services.ToList();

                // Load related entities for each service
                foreach (var service in servicesList)
                {
                    await LoadQuoteLineItemsAsync(service, cancellationToken);
                    await LoadInvoiceLineItemsAsync(service, cancellationToken);
                    await LoadSalesOrderLineItemServicesAsync(service, cancellationToken);
                    await LoadSalesOrderLineItemsAsync(service, cancellationToken);
                }

                return servicesList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all services with related entities");
                throw;
            }
        }
    }
}