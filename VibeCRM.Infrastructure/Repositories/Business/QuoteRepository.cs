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
    /// Repository implementation for managing Quote entities
    /// </summary>
    public class QuoteRepository : BaseRepository<Quote, Guid>, IQuoteRepository
    {
        /// <summary>
        /// Gets the ID column name for the entity
        /// </summary>
        protected override string IdColumnName => "QuoteId";

        /// <summary>
        /// Gets the database table name for the entity
        /// </summary>
        protected override string TableName => "Quote";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[]
        {
            "QuoteId", "QuoteStatusId", "Number", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Active"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteRepository"/> class.
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for capturing errors and information</param>
        public QuoteRepository(ISQLConnectionFactory connectionFactory, ILogger<QuoteRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all quotes from the repository
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of all quotes in the repository</returns>
        public override async Task<IEnumerable<Quote>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT q.QuoteId, q.QuoteStatusId, q.Number, q.CreatedBy, q.CreatedDate, q.ModifiedBy, q.ModifiedDate, q.Active
                FROM Quote q
                WHERE q.Active = 1
                ORDER BY q.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<Quote>(
                        new CommandDefinition(
                            sql,
                            cancellationToken: cancellationToken)),
                "GetAllAsync",
                new { ErrorMessage = "Error retrieving all Quotes", EntityType = nameof(Quote) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a quote by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the quote</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The quote if found, otherwise null</returns>
        public override async Task<Quote?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT q.QuoteId, q.QuoteStatusId, q.Number, q.CreatedBy, q.CreatedDate, q.ModifiedBy, q.ModifiedDate, q.Active
                FROM Quote q
                WHERE q.QuoteId = @Id AND q.Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryFirstOrDefaultAsync<Quote>(
                        new CommandDefinition(
                            sql,
                            new { Id = id },
                            cancellationToken: cancellationToken)),
                "GetByIdAsync",
                new { ErrorMessage = $"Error retrieving Quote with ID {id}", QuoteId = id, EntityType = nameof(Quote) },
                cancellationToken);
        }

        /// <summary>
        /// Gets a quote by its unique identifier with all related entities loaded
        /// </summary>
        /// <param name="id">The unique identifier of the quote</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The quote with all related entities if found, otherwise null</returns>
        public async Task<Quote?> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var quote = await GetByIdAsync(id, cancellationToken);
            if (quote == null) return null;

            // Load all related entities
            await LoadQuoteStatusAsync(quote, cancellationToken);
            await LoadLineItemsAsync(quote, cancellationToken);
            await LoadSalesOrdersAsync(quote, cancellationToken);
            await LoadCompaniesAsync(quote, cancellationToken);
            await LoadActivitiesAsync(quote, cancellationToken);

            return quote;
        }

        /// <summary>
        /// Loads the quote status for a quote
        /// </summary>
        /// <param name="quote">The quote to load the status for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task LoadQuoteStatusAsync(Quote quote, CancellationToken cancellationToken = default)
        {
            if (quote.QuoteStatusId == null || quote.QuoteStatusId == Guid.Empty) return;

            const string sql = @"
                SELECT
                    QuoteStatusId, Status, Description, OrdinalPosition, 
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                FROM QuoteStatus
                WHERE QuoteStatusId = @StatusId AND Active = 1";

            var quoteStatus = await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QuerySingleOrDefaultAsync<QuoteStatus>(
                        new CommandDefinition(
                            sql,
                            new { StatusId = quote.QuoteStatusId },
                            cancellationToken: cancellationToken)),
                "LoadQuoteStatusAsync",
                new { QuoteId = quote.QuoteId, StatusId = quote.QuoteStatusId },
                cancellationToken);

            quote.QuoteStatus = quoteStatus;
        }

        /// <summary>
        /// Loads the line items for a quote
        /// </summary>
        /// <param name="quote">The quote to load the line items for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task LoadLineItemsAsync(Quote quote, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT
                    QuoteLineItemId, QuoteId, ProductId, ServiceId, Description,
                    Quantity, UnitPrice, DiscountPercentage, DiscountAmount, TaxPercentage,
                    LineNumber, Notes, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                FROM QuoteLineItem
                WHERE QuoteId = @QuoteId AND Active = 1
                ORDER BY LineNumber";

            var lineItems = await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<QuoteLineItem>(
                        new CommandDefinition(
                            sql,
                            new { QuoteId = quote.QuoteId },
                            cancellationToken: cancellationToken)),
                "LoadLineItemsAsync",
                new { QuoteId = quote.QuoteId },
                cancellationToken);

            foreach (var lineItem in lineItems)
            {
                lineItem.Quote = quote;
                quote.LineItems.Add(lineItem);
            }
        }

        /// <summary>
        /// Loads the sales orders for a quote
        /// </summary>
        /// <param name="quote">The quote to load the sales orders for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task LoadSalesOrdersAsync(Quote quote, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT
                    SalesOrderId, SalesOrderStatusId, ShipMethodId, BillToAddressId, ShipToAddressId,
                    TaxCodeId, QuoteId, Number, OrderDate, ShipDate, SubTotal, TaxAmount, DueAmount,
                    CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
                FROM SalesOrder
                WHERE QuoteId = @QuoteId AND Active = 1
                ORDER BY OrderDate DESC";

            var salesOrders = await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<SalesOrder>(
                        new CommandDefinition(
                            sql,
                            new { QuoteId = quote.QuoteId },
                            cancellationToken: cancellationToken)),
                "LoadSalesOrdersAsync",
                new { QuoteId = quote.QuoteId },
                cancellationToken);

            foreach (var salesOrder in salesOrders)
            {
                salesOrder.Quote = quote;
                quote.SalesOrders.Add(salesOrder);
            }
        }

        /// <summary>
        /// Loads the companies associated with a quote
        /// </summary>
        /// <param name="quote">The quote for which to load the companies</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task LoadCompaniesAsync(Quote quote, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT cq.CompanyId, cq.QuoteId, cq.Active, cq.ModifiedDate,
                       c.CompanyId, c.CompanyTypeId, c.CompanyStatusId, c.Name, c.Website, c.Phone, c.Email,
                       c.Description, c.Notes, c.CreatedBy, c.CreatedDate, c.ModifiedBy, c.ModifiedDate, c.Active
                FROM Company_Quote cq
                INNER JOIN Company c ON cq.CompanyId = c.CompanyId
                WHERE cq.QuoteId = @QuoteId AND cq.Active = 1 AND c.Active = 1";

            var companyQuotes = await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<Company_Quote, Company, Company_Quote>(
                        new CommandDefinition(
                            sql,
                            new { QuoteId = quote.QuoteId },
                            cancellationToken: cancellationToken),
                        (companyQuote, company) =>
                        {
                            companyQuote.Company = company;
                            companyQuote.Quote = quote;
                            return companyQuote;
                        },
                        splitOn: "CompanyId"),
                "LoadCompaniesAsync",
                new { QuoteId = quote.QuoteId },
                cancellationToken);

            foreach (var companyQuote in companyQuotes)
            {
                quote.Companies.Add(companyQuote);
            }
        }

        /// <summary>
        /// Loads the activities associated with a quote
        /// </summary>
        /// <param name="quote">The quote for which to load the activities</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task LoadActivitiesAsync(Quote quote, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT qa.QuoteId, qa.ActivityId, qa.Active, qa.ModifiedDate,
                       a.ActivityId, a.ActivityTypeId, a.ActivityStatusId, a.AssignedUserId, a.AssignedTeamId,
                       a.Subject, a.Description, a.DueDate, a.StartDate, a.CompletedDate, a.CompletedBy,
                       a.CreatedBy, a.CreatedDate, a.ModifiedBy, a.ModifiedDate, a.Active
                FROM Quote_Activity qa
                INNER JOIN Activity a ON qa.ActivityId = a.ActivityId
                WHERE qa.QuoteId = @QuoteId AND qa.Active = 1 AND a.Active = 1";

            var quoteActivities = await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<Quote_Activity, Activity, Quote_Activity>(
                        new CommandDefinition(
                            sql,
                            new { QuoteId = quote.QuoteId },
                            cancellationToken: cancellationToken),
                        (quoteActivity, activity) =>
                        {
                            quoteActivity.Activity = activity;
                            quoteActivity.Quote = quote;
                            return quoteActivity;
                        },
                        splitOn: "ActivityId"),
                "LoadActivitiesAsync",
                new { QuoteId = quote.QuoteId },
                cancellationToken);

            foreach (var quoteActivity in quoteActivities)
            {
                quote.Activities.Add(quoteActivity);
            }
        }

        /// <summary>
        /// Adds a new quote to the repository
        /// </summary>
        /// <param name="entity">The quote to add</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The added quote with any system-generated values populated</returns>
        public override async Task<Quote> AddAsync(Quote entity, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                INSERT INTO Quote (QuoteId, QuoteStatusId, Number, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active)
                VALUES (@QuoteId, @QuoteStatusId, @Number, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active);";

            await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "AddAsync",
                new { ErrorMessage = $"Error adding Quote with ID {entity.QuoteId}", QuoteId = entity.QuoteId, EntityType = nameof(Quote) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Updates an existing quote in the repository
        /// </summary>
        /// <param name="entity">The quote to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated quote</returns>
        public override async Task<Quote> UpdateAsync(Quote entity, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                UPDATE Quote
                SET QuoteStatusId = @QuoteStatusId,
                    Number = @Number,
                    ModifiedBy = @ModifiedBy,
                    ModifiedDate = @ModifiedDate,
                    Active = @Active
                WHERE QuoteId = @QuoteId;";

            var affectedRows = await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            entity,
                            cancellationToken: cancellationToken)),
                "UpdateAsync",
                new { ErrorMessage = $"Error updating Quote with ID {entity.QuoteId}", QuoteId = entity.QuoteId, EntityType = nameof(Quote) },
                cancellationToken);

            return entity;
        }

        /// <summary>
        /// Deletes a quote from the repository
        /// </summary>
        /// <param name="id">The unique identifier of the quote to delete</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the quote was successfully deleted, otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        public override async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("The Quote ID cannot be empty", nameof(id));

            const string sql = @"
                UPDATE Quote
                SET Active = 0,
                    ModifiedDate = @ModifiedDate
                WHERE QuoteId = @Id
                AND Active = 1";

            var parameters = new
            {
                Id = id,
                ModifiedDate = DateTime.UtcNow
            };

            var success = await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.ExecuteAsync(
                        new CommandDefinition(
                            sql,
                            parameters,
                            cancellationToken: cancellationToken)) > 0,
                "DeleteAsync",
                new { ErrorMessage = $"Error soft-deleting Quote with ID {id}", QuoteId = id, EntityType = nameof(Quote) },
                cancellationToken);

            return success;
        }

        /// <summary>
        /// Checks if a quote with the specified identifier exists
        /// </summary>
        /// <param name="id">The unique identifier to check</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a quote with the specified ID exists, otherwise false</returns>
        public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM Quote
                    WHERE QuoteId = @Id AND Active = 1
                ) THEN 1 ELSE 0 END";

            var count = await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.ExecuteScalarAsync<int>(
                        new CommandDefinition(
                            sql,
                            new { Id = id },
                            cancellationToken: cancellationToken)),
                "ExistsAsync",
                new { ErrorMessage = $"Error checking if Quote with ID {id} exists", QuoteId = id, EntityType = nameof(Quote) },
                cancellationToken);

            return count > 0;
        }

        /// <summary>
        /// Gets quotes by number
        /// </summary>
        /// <param name="number">The quote number</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quotes with the specified number</returns>
        public async Task<IEnumerable<Quote>> GetByNumberAsync(string number, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT q.QuoteId, q.QuoteStatusId, q.Number, q.CreatedBy, q.CreatedDate, q.ModifiedBy, q.ModifiedDate, q.Active
                FROM Quote q
                WHERE q.Number = @Number AND q.Active = 1
                ORDER BY q.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<Quote>(
                        new CommandDefinition(
                            sql,
                            new { Number = number },
                            cancellationToken: cancellationToken)),
                "GetByNumberAsync",
                new { ErrorMessage = $"Error retrieving Quote with number {number}", Number = number, EntityType = nameof(Quote) },
                cancellationToken);
        }

        /// <summary>
        /// Gets quotes for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quotes associated with the specified company</returns>
        public async Task<IEnumerable<Quote>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT q.QuoteId, q.QuoteStatusId, q.Number, q.CreatedBy, q.CreatedDate, q.ModifiedBy, q.ModifiedDate, q.Active
                FROM Quote q
                INNER JOIN Company_Quote cq ON q.QuoteId = cq.QuoteId
                WHERE cq.CompanyId = @CompanyId AND q.Active = 1
                ORDER BY q.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<Quote>(
                        new CommandDefinition(
                            sql,
                            new { CompanyId = companyId },
                            cancellationToken: cancellationToken)),
                "GetByCompanyAsync",
                new { ErrorMessage = $"Error retrieving Quotes for company with ID {companyId}", CompanyId = companyId, EntityType = nameof(Quote) },
                cancellationToken);
        }

        /// <summary>
        /// Gets quotes associated with a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quotes associated with the specified activity</returns>
        public async Task<IEnumerable<Quote>> GetByActivityAsync(Guid activityId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT q.QuoteId, q.QuoteStatusId, q.Number, q.CreatedBy, q.CreatedDate, q.ModifiedBy, q.ModifiedDate, q.Active
                FROM Quote q
                INNER JOIN Activity_Quote aq ON q.QuoteId = aq.QuoteId
                WHERE aq.ActivityId = @ActivityId AND q.Active = 1
                ORDER BY q.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<Quote>(
                        new CommandDefinition(
                            sql,
                            new { ActivityId = activityId },
                            cancellationToken: cancellationToken)),
                "GetByActivityAsync",
                new { ErrorMessage = $"Error retrieving Quotes for activity with ID {activityId}", ActivityId = activityId, EntityType = nameof(Quote) },
                cancellationToken);
        }

        /// <summary>
        /// Gets quotes by quote status identifier
        /// </summary>
        /// <param name="quoteStatusId">The quote status identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quotes with the specified quote status</returns>
        public async Task<IEnumerable<Quote>> GetByQuoteStatusIdAsync(Guid quoteStatusId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT q.QuoteId, q.QuoteStatusId, q.Number, q.CreatedBy, q.CreatedDate, q.ModifiedBy, q.ModifiedDate, q.Active
                FROM Quote q
                WHERE q.QuoteStatusId = @QuoteStatusId AND q.Active = 1
                ORDER BY q.CreatedDate DESC";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection =>
                    await connection.QueryAsync<Quote>(
                        new CommandDefinition(
                            sql,
                            new { QuoteStatusId = quoteStatusId },
                            cancellationToken: cancellationToken)),
                "GetByQuoteStatusIdAsync",
                new { ErrorMessage = $"Error retrieving Quotes for quote status with ID {quoteStatusId}", QuoteStatusId = quoteStatusId, EntityType = nameof(Quote) },
                cancellationToken);
        }
    }
}