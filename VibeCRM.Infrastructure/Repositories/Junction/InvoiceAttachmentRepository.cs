using Dapper;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Interfaces.Repositories.Junction;
using VibeCRM.Infrastructure.Persistence.Database;

namespace VibeCRM.Infrastructure.Repositories.Junction
{
    /// <summary>
    /// Repository implementation for managing Invoice_Attachment junction entities
    /// </summary>
    public class InvoiceAttachmentRepository : BaseJunctionRepository<Invoice_Attachment, Guid, Guid>, IInvoiceAttachmentRepository
    {
        /// <summary>
        /// Gets the table name for the entity
        /// </summary>
        protected override string TableName => "Invoice_Attachment";

        /// <summary>
        /// Gets the name of the first ID column (InvoiceId)
        /// </summary>
        protected override string FirstIdColumnName => "InvoiceId";

        /// <summary>
        /// Gets the name of the second ID column (AttachmentId)
        /// </summary>
        protected override string SecondIdColumnName => "AttachmentId";

        /// <summary>
        /// Gets a list of columns to select for basic queries
        /// </summary>
        protected override string[] SelectColumns => new[] { "InvoiceId", "AttachmentId", "Active", "ModifiedDate" };

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceAttachmentRepository"/> class
        /// </summary>
        /// <param name="connectionFactory">The SQL connection factory used to create database connections</param>
        /// <param name="logger">The logger for logging events and errors</param>
        /// <exception cref="ArgumentNullException">Thrown when connectionFactory or logger is null</exception>
        public InvoiceAttachmentRepository(
            ISQLConnectionFactory connectionFactory,
            ILogger<InvoiceAttachmentRepository> logger)
            : base(connectionFactory, logger)
        {
        }

        /// <summary>
        /// Gets all invoice-attachment relationships for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice-attachment relationships for the specified invoice</returns>
        public async Task<IEnumerable<Invoice_Attachment>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {FirstIdColumnName} = @InvoiceId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Invoice_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { InvoiceId = invoiceId },
                        cancellationToken: cancellationToken)),
                "GetByInvoiceIdAsync",
                new { InvoiceId = invoiceId, EntityType = nameof(Invoice_Attachment) },
                cancellationToken);
        }

        /// <summary>
        /// Gets all invoice-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice-attachment relationships for the specified attachment</returns>
        public async Task<IEnumerable<Invoice_Attachment>> GetByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default)
        {
            var sql = $@"
                SELECT {string.Join(", ", SelectColumns)}
                FROM {TableName}
                WHERE {SecondIdColumnName} = @AttachmentId
                AND Active = 1";

            return await ExecuteWithResilienceAndLoggingAsync(
                async connection => await connection.QueryAsync<Invoice_Attachment>(
                    new CommandDefinition(
                        sql,
                        new { AttachmentId = attachmentId },
                        cancellationToken: cancellationToken)),
                "GetByAttachmentIdAsync",
                new { AttachmentId = attachmentId, EntityType = nameof(Invoice_Attachment) },
                cancellationToken);
        }
    }
}