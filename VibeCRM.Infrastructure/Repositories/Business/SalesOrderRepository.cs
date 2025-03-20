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
    /// Repository implementation for managing SalesOrder entities
    /// </summary>
    public class SalesOrderRepository : BaseRepository<SalesOrder, Guid>, ISalesOrderRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "SalesOrder";

        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "SalesOrderId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "SalesOrderId", "Number", "SalesOrderStatusId", "ShipMethodId", "BillToAddressId",
            "ShipToAddressId", "TaxCodeId", "OrderDate", "DueDate", "ShipDate",
            "Subtotal", "TaxAmount", "TotalDiscount", "TotalAmount", "CreatedBy",
            "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrderRepository"/> class.
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public SalesOrderRepository(ISQLConnectionFactory connectionFactory, ILogger<SalesOrderRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Adds a new sales order to the repository
        /// </summary>
        /// <param name="entity">The sales order to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added sales order with any system-generated values populated</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<SalesOrder> AddAsync(SalesOrder entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.SalesOrderId == Guid.Empty) throw new ArgumentException("The SalesOrder ID cannot be empty", nameof(entity));

            const string sql = @"
                INSERT INTO SalesOrder (
                    SalesOrderId, Number, SalesOrderStatusId, ShipMethodId, BillToAddressId,
                    ShipToAddressId, TaxCodeId, OrderDate, DueDate, ShipDate,
                    Subtotal, TaxAmount, TotalDiscount, TotalAmount, CreatedBy,
                    CreatedDate, ModifiedBy, ModifiedDate, Active
                ) VALUES (
                    @SalesOrderId, @Number, @SalesOrderStatusId, @ShipMethodId, @BillToAddressId,
                    @ShipToAddressId, @TaxCodeId, @OrderDate, @DueDate, @ShipDate,
                    @Subtotal, @TaxAmount, @TotalDiscount, @TotalAmount, @CreatedBy,
                    @CreatedDate, @ModifiedBy, @ModifiedDate, @Active
                );";

            await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding SalesOrder with ID {entity.SalesOrderId}", SalesOrderId = entity.SalesOrderId, EntityType = nameof(SalesOrder) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing sales order in the repository
        /// </summary>
        /// <param name="entity">The sales order to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated sales order</returns>
        /// <exception cref="ArgumentNullException">Thrown when entity is null</exception>
        public override async Task<SalesOrder> UpdateAsync(SalesOrder entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.SalesOrderId == Guid.Empty) throw new ArgumentException("The SalesOrder ID cannot be empty", nameof(entity));

            const string sql = @"
                UPDATE SalesOrder
                SET
                    Number = @Number,
                    SalesOrderStatusId = @SalesOrderStatusId,
                    ShipMethodId = @ShipMethodId,
                    BillToAddressId = @BillToAddressId,
                    ShipToAddressId = @ShipToAddressId,
                    TaxCodeId = @TaxCodeId,
                    OrderDate = @OrderDate,
                    DueDate = @DueDate,
                    ShipDate = @ShipDate,
                    Subtotal = @Subtotal,
                    TaxAmount = @TaxAmount,
                    TotalDiscount = @TotalDiscount,
                    TotalAmount = @TotalAmount,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate,
                    Active = @Active
                WHERE SalesOrderId = @SalesOrderId";

            var affectedRows = await ExecuteWithResilienceAndLoggingAsync<int>(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating Sales Order with ID {entity.SalesOrderId}", SalesOrderId = entity.SalesOrderId, EntityType = nameof(SalesOrder) },
                cancellationToken);

            if (affectedRows == 0)
            {
                _logger.LogWarning("No SalesOrder was updated for ID {SalesOrderId}", entity.SalesOrderId);
            }

            return entity;
        }

        /// <summary>
        /// Gets sales orders by number
        /// </summary>
        /// <param name="number">The sales order number</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales orders with the specified number</returns>
        /// <exception cref="ArgumentException">Thrown when number is null or empty</exception>
        public async Task<IEnumerable<SalesOrder>> GetByNumberAsync(string number, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(number)) throw new ArgumentException("Sales order number cannot be null or empty", nameof(number));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE Number = @Number AND Active = 1
                ORDER BY CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrder>>(
                async connection =>
                    await connection.QueryAsync<SalesOrder>(
                        new CommandDefinition(
                            sql,
                            new { Number = number },
                            cancellationToken: cancellationToken)),
                "GetByNumberAsync",
                new { ErrorMessage = $"Error retrieving Sales Orders with number {number}", Number = number, EntityType = nameof(SalesOrder) },
                cancellationToken);
        }

        /// <summary>
        /// Gets sales orders for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales orders associated with the specified company</returns>
        /// <exception cref="ArgumentException">Thrown when companyId is empty</exception>
        public async Task<IEnumerable<SalesOrder>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            if (companyId == Guid.Empty) throw new ArgumentException("The Company ID cannot be empty", nameof(companyId));

            const string sql = @"
                SELECT
                    s.SalesOrderId, s.Number, s.SalesOrderStatusId, s.ShipMethodId, s.BillToAddressId,
                    s.ShipToAddressId, s.TaxCodeId, s.OrderDate, s.DueDate, s.ShipDate,
                    s.Subtotal, s.TaxAmount, s.TotalDiscount, s.TotalAmount, s.CreatedBy,
                    s.CreatedDate, s.ModifiedBy, s.ModifiedDate, s.Active
                FROM SalesOrder s
                INNER JOIN Company_SalesOrder cs ON s.SalesOrderId = cs.SalesOrderId
                WHERE cs.CompanyId = @CompanyId AND s.Active = 1
                ORDER BY s.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrder>>(
                async connection => await connection.QueryAsync<SalesOrder>(
                    new CommandDefinition(
                        sql,
                        new { CompanyId = companyId },
                        cancellationToken: cancellationToken)),
                "GetByCompanyIdAsync",
                new { CompanyId = companyId, EntityType = nameof(SalesOrder) },
                cancellationToken);
        }

        /// <summary>
        /// Gets sales orders associated with a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales orders associated with the specified activity</returns>
        /// <exception cref="ArgumentException">Thrown when activityId is empty</exception>
        public async Task<IEnumerable<SalesOrder>> GetByActivityAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            if (activityId == Guid.Empty) throw new ArgumentException("The Activity ID cannot be empty", nameof(activityId));

            const string sql = @"
                SELECT
                    s.SalesOrderId, s.Number, s.SalesOrderStatusId, s.ShipMethodId, s.BillToAddressId,
                    s.ShipToAddressId, s.TaxCodeId, s.OrderDate, s.DueDate, s.ShipDate,
                    s.Subtotal, s.TaxAmount, s.TotalDiscount, s.TotalAmount, s.CreatedBy,
                    s.CreatedDate, s.ModifiedBy, s.ModifiedDate, s.Active
                FROM SalesOrder s
                INNER JOIN SalesOrder_Activity sa ON s.SalesOrderId = sa.SalesOrderId
                WHERE sa.ActivityId = @ActivityId AND s.Active = 1
                ORDER BY s.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrder>>(
                async connection =>
                    await connection.QueryAsync<SalesOrder>(
                        new CommandDefinition(
                            sql,
                            new { ActivityId = activityId },
                            cancellationToken: cancellationToken)),
                "GetByActivityAsync",
                new { ErrorMessage = $"Error retrieving Sales Orders for activity with ID {activityId}", ActivityId = activityId, EntityType = nameof(SalesOrder) },
                cancellationToken);
        }

        /// <summary>
        /// Gets sales orders associated with a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales orders associated with the specified quote</returns>
        /// <exception cref="ArgumentException">Thrown when quoteId is empty</exception>
        public async Task<IEnumerable<SalesOrder>> GetByQuoteAsync(Guid quoteId, CancellationToken cancellationToken = default)
        {
            if (quoteId == Guid.Empty) throw new ArgumentException("The Quote ID cannot be empty", nameof(quoteId));

            const string sql = @"
                SELECT
                    s.SalesOrderId, s.Number, s.SalesOrderStatusId, s.ShipMethodId, s.BillToAddressId,
                    s.ShipToAddressId, s.TaxCodeId, s.OrderDate, s.DueDate, s.ShipDate,
                    s.Subtotal, s.TaxAmount, s.TotalDiscount, s.TotalAmount, s.CreatedBy,
                    s.CreatedDate, s.ModifiedBy, s.ModifiedDate, s.Active
                FROM SalesOrder s
                INNER JOIN Quote_SalesOrder qs ON s.SalesOrderId = qs.SalesOrderId
                WHERE qs.QuoteId = @QuoteId AND s.Active = 1
                ORDER BY s.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrder>>(
                async connection =>
                    await connection.QueryAsync<SalesOrder>(
                        new CommandDefinition(
                            sql,
                            new { QuoteId = quoteId },
                            cancellationToken: cancellationToken)),
                "GetByQuoteAsync",
                new { ErrorMessage = $"Error retrieving Sales Orders for quote with ID {quoteId}", QuoteId = quoteId, EntityType = nameof(SalesOrder) },
                cancellationToken);
        }

        /// <summary>
        /// Gets sales orders by status
        /// </summary>
        /// <param name="salesOrderStatusId">The sales order status identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales orders with the specified status</returns>
        /// <exception cref="ArgumentException">Thrown when salesOrderStatusId is empty</exception>
        public async Task<IEnumerable<SalesOrder>> GetBySalesOrderStatusAsync(Guid salesOrderStatusId, CancellationToken cancellationToken = default)
        {
            if (salesOrderStatusId == Guid.Empty) throw new ArgumentException("The SalesOrderStatus ID cannot be empty", nameof(salesOrderStatusId));

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE SalesOrderStatusId = @StatusId AND Active = 1
                ORDER BY CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrder>>(
                async connection =>
                    await connection.QueryAsync<SalesOrder>(
                        new CommandDefinition(
                            sql,
                            new { StatusId = salesOrderStatusId },
                            cancellationToken: cancellationToken)),
                "GetBySalesOrderStatusAsync",
                new { ErrorMessage = $"Error retrieving Sales Orders with status ID {salesOrderStatusId}", StatusId = salesOrderStatusId, EntityType = nameof(SalesOrder) },
                cancellationToken);
        }

        /// <summary>
        /// Gets sales orders by order date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales orders with order dates within the specified range</returns>
        /// <exception cref="ArgumentException">Thrown when startDate is later than endDate</exception>
        public async Task<IEnumerable<SalesOrder>> GetByOrderDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date cannot be later than end date", nameof(startDate));
            }

            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE OrderDate >= @StartDate AND OrderDate <= @EndDate AND Active = 1
                ORDER BY OrderDate ASC";

            var parameters = new
            {
                StartDate = startDate,
                EndDate = endDate
            };

            return await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrder>>(
                async connection =>
                    await connection.QueryAsync<SalesOrder>(
                        new CommandDefinition(
                            sql,
                            parameters,
                            cancellationToken: cancellationToken)),
                "GetByOrderDateRangeAsync",
                new { ErrorMessage = $"Error retrieving Sales Orders within date range from {startDate} to {endDate}", StartDate = startDate, EndDate = endDate, EntityType = nameof(SalesOrder) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a sales order by its unique identifier with related entities
        /// </summary>
        /// <param name="id">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The sales order with the specified identifier, or null if not found</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public async Task<SalesOrder> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Sales Order ID cannot be empty", nameof(id));

            var salesOrder = await GetByIdAsync(id, cancellationToken);
            if (salesOrder == null) return new SalesOrder();

            // Load related entities
            await LoadSalesOrderStatusAsync(salesOrder, cancellationToken);
            await LoadShipMethodAsync(salesOrder, cancellationToken);
            await LoadTaxCodeAsync(salesOrder, cancellationToken);
            await LoadAddressesAsync(salesOrder, cancellationToken);
            await LoadLineItemsAsync(salesOrder, cancellationToken);
            await LoadCompaniesAsync(salesOrder, cancellationToken);
            await LoadActivitiesAsync(salesOrder, cancellationToken);
            await LoadQuoteAsync(salesOrder, cancellationToken);

            return salesOrder;
        }

        /// <summary>
        /// Loads the sales order status for a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the status for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        public async Task LoadSalesOrderStatusAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default)
        {
            if (salesOrder == null) throw new ArgumentNullException(nameof(salesOrder));
            if (salesOrder.SalesOrderStatusId == Guid.Empty) return;

            const string sql = @"
                SELECT
                    SalesOrderStatusId, Status, Description, OrdinalPosition,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                FROM SalesOrderStatus
                WHERE SalesOrderStatusId = @StatusId AND Active = 1";

            var salesOrderStatus = await ExecuteWithResilienceAndLoggingAsync<SalesOrderStatus>(
                async connection =>
                    await connection.QuerySingleOrDefaultAsync<SalesOrderStatus>(
                        new CommandDefinition(
                            sql,
                            new { StatusId = salesOrder.SalesOrderStatusId },
                            cancellationToken: cancellationToken)),
                "LoadSalesOrderStatusAsync",
                new { SalesOrderId = salesOrder.SalesOrderId, StatusId = salesOrder.SalesOrderStatusId },
                cancellationToken);

            salesOrder.SalesOrderStatus = salesOrderStatus;
        }

        /// <summary>
        /// Loads the ship method for a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the ship method for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        public async Task LoadShipMethodAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default)
        {
            if (salesOrder == null) throw new ArgumentNullException(nameof(salesOrder));
            if (salesOrder.ShipMethodId == Guid.Empty) return;

            const string sql = @"
                SELECT
                    ShipMethodId, Method, Description, OrdinalPosition,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                FROM ShipMethod
                WHERE ShipMethodId = @ShipMethodId AND Active = 1";

            var shipMethod = await ExecuteWithResilienceAndLoggingAsync<ShipMethod>(
                async connection =>
                    await connection.QuerySingleOrDefaultAsync<ShipMethod>(
                        new CommandDefinition(
                            sql,
                            new { ShipMethodId = salesOrder.ShipMethodId },
                            cancellationToken: cancellationToken)),
                "LoadShipMethodAsync",
                new { SalesOrderId = salesOrder.SalesOrderId, ShipMethodId = salesOrder.ShipMethodId },
                cancellationToken);

            salesOrder.ShipMethod = shipMethod;
        }

        /// <summary>
        /// Loads the bill-to and ship-to addresses for a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the addresses for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        public async Task LoadAddressesAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default)
        {
            if (salesOrder == null) throw new ArgumentNullException(nameof(salesOrder));
            if (salesOrder.BillToAddressId == Guid.Empty && salesOrder.ShipToAddressId == Guid.Empty) return;

            const string sql = @"
                SELECT
                    AddressId, AddressLine1, AddressLine2, City, StateProvince,
                    PostalCode, Country, AddressType, CreatedBy, CreatedDate,
                    ModifiedBy, ModifiedDate, Active
                FROM Address
                WHERE (AddressId = @BillToAddressId OR AddressId = @ShipToAddressId) AND Active = 1";

            var addresses = await ExecuteWithResilienceAndLoggingAsync<IEnumerable<Address>>(
                async connection =>
                    await connection.QueryAsync<Address>(
                        new CommandDefinition(
                            sql,
                            new
                            {
                                BillToAddressId = salesOrder.BillToAddressId,
                                ShipToAddressId = salesOrder.ShipToAddressId
                            },
                            cancellationToken: cancellationToken)),
                "LoadAddressesAsync",
                new { SalesOrderId = salesOrder.SalesOrderId },
                cancellationToken);

            foreach (var address in addresses)
            {
                if (address.AddressId == salesOrder.BillToAddressId)
                {
                    salesOrder.BillToAddress = address;
                }
                if (address.AddressId == salesOrder.ShipToAddressId)
                {
                    salesOrder.ShipToAddress = address;
                }
            }
        }

        /// <summary>
        /// Loads the line items for a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the line items for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        public async Task LoadLineItemsAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default)
        {
            if (salesOrder == null) throw new ArgumentNullException(nameof(salesOrder));

            const string sql = @"
                SELECT
                    SalesOrderLineItemId, SalesOrderId, ProductId, ServiceId, Description,
                    Quantity, UnitPrice, DiscountPercentage, DiscountAmount, IsTaxable,
                    TaxRate, Notes, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                FROM SalesOrderLineItem
                WHERE SalesOrderId = @SalesOrderId AND Active = 1
                ORDER BY CreatedDate";

            var lineItems = await ExecuteWithResilienceAndLoggingAsync<IEnumerable<SalesOrderLineItem>>(
                async connection =>
                    await connection.QueryAsync<SalesOrderLineItem>(
                        new CommandDefinition(
                            sql,
                            new { SalesOrderId = salesOrder.SalesOrderId },
                            cancellationToken: cancellationToken)),
                "LoadLineItemsAsync",
                new { SalesOrderId = salesOrder.SalesOrderId },
                cancellationToken);

            salesOrder.LineItems = lineItems.ToList();

            // Set the parent reference in each line item
            foreach (var lineItem in salesOrder.LineItems)
            {
                lineItem.SalesOrder = salesOrder;
            }
        }

        /// <summary>
        /// Loads the tax code for a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the tax code for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        public async Task LoadTaxCodeAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default)
        {
            if (salesOrder == null) throw new ArgumentNullException(nameof(salesOrder));

            if (salesOrder.TaxCodeId.HasValue)
            {
                const string sql = @"
                    SELECT *
                    FROM TaxCode
                    WHERE TaxCodeId = @TaxCodeId AND Active = 1";

                // Since TaxCode entity doesn't exist, we'll use dynamic to retrieve the data
                // and just set the TaxCodeId property on the SalesOrder
                await ExecuteWithResilienceAndLoggingAsync(
                    async connection =>
                    {
                        await connection.QuerySingleOrDefaultAsync(
                            new CommandDefinition(
                                sql,
                                new { TaxCodeId = salesOrder.TaxCodeId },
                                cancellationToken: cancellationToken));
                        return true;
                    },
                    "LoadTaxCodeAsync",
                    new { SalesOrderId = salesOrder.SalesOrderId, TaxCodeId = salesOrder.TaxCodeId },
                    cancellationToken);

                // We don't set salesOrder.TaxCode as it doesn't have this navigation property
            }
        }

        /// <summary>
        /// Loads the companies for a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the companies for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        public async Task LoadCompaniesAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default)
        {
            if (salesOrder == null) throw new ArgumentNullException(nameof(salesOrder));

            const string sql = @"
                SELECT cso.*, c.*
                FROM Company_SalesOrder cso
                INNER JOIN Company c ON cso.CompanyId = c.CompanyId
                WHERE cso.SalesOrderId = @SalesOrderId AND cso.Active = 1 AND c.Active = 1";

            var companySalesOrders = await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<Company_SalesOrder, Company, Company_SalesOrder>(
                        new CommandDefinition(
                            sql,
                            new { SalesOrderId = salesOrder.SalesOrderId },
                            cancellationToken: cancellationToken),
                        (companySalesOrder, company) =>
                        {
                            companySalesOrder.Company = company;
                            companySalesOrder.SalesOrder = salesOrder;
                            return companySalesOrder;
                        },
                        splitOn: "CompanyId"),
                "LoadCompaniesAsync",
                new { SalesOrderId = salesOrder.SalesOrderId },
                cancellationToken);

            // Clear existing items and add the retrieved ones
            salesOrder.Companies.Clear();
            foreach (var companySalesOrder in companySalesOrders)
            {
                salesOrder.Companies.Add(companySalesOrder);
            }
        }

        /// <summary>
        /// Loads the activities for a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the activities for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        public async Task LoadActivitiesAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default)
        {
            if (salesOrder == null) throw new ArgumentNullException(nameof(salesOrder));

            const string sql = @"
                SELECT soa.*, a.*
                FROM SalesOrder_Activity soa
                INNER JOIN Activity a ON soa.ActivityId = a.ActivityId
                WHERE soa.SalesOrderId = @SalesOrderId AND soa.Active = 1 AND a.Active = 1";

            var salesOrderActivities = await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<SalesOrder_Activity, Activity, SalesOrder_Activity>(
                        new CommandDefinition(
                            sql,
                            new { SalesOrderId = salesOrder.SalesOrderId },
                            cancellationToken: cancellationToken),
                        (salesOrderActivity, activity) =>
                        {
                            salesOrderActivity.Activity = activity;
                            salesOrderActivity.SalesOrder = salesOrder;
                            return salesOrderActivity;
                        },
                        splitOn: "ActivityId"),
                "LoadActivitiesAsync",
                new { SalesOrderId = salesOrder.SalesOrderId },
                cancellationToken);

            // Clear existing items and add the retrieved ones
            salesOrder.Activities.Clear();
            foreach (var salesOrderActivity in salesOrderActivities)
            {
                salesOrder.Activities.Add(salesOrderActivity);
            }
        }

        /// <summary>
        /// Loads the quote for a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the quote for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        public async Task LoadQuoteAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default)
        {
            if (salesOrder == null) throw new ArgumentNullException(nameof(salesOrder));

            const string sql = @"
                SELECT
                    q.QuoteId, q.Number, q.QuoteDate, q.ExpirationDate, q.TotalAmount, q.CreatedBy, q.CreatedDate, q.ModifiedBy, q.ModifiedDate, q.Active
                FROM Quote q
                INNER JOIN Quote_SalesOrder qs ON q.QuoteId = qs.QuoteId
                WHERE qs.SalesOrderId = @SalesOrderId AND q.Active = 1";

            var quote = await ExecuteWithResilienceAndLoggingAsync<Quote>(
                async connection =>
                    await connection.QuerySingleOrDefaultAsync<Quote>(
                        new CommandDefinition(
                            sql,
                            new { SalesOrderId = salesOrder.SalesOrderId },
                            cancellationToken: cancellationToken)),
                "LoadQuoteAsync",
                new { SalesOrderId = salesOrder.SalesOrderId },
                cancellationToken);

            salesOrder.Quote = quote;
        }
    }
}