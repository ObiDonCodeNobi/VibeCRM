using VibeCRM.Domain.Common.Base;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents an invoice in the system.
    /// </summary>
    public class Invoice : BaseAuditableEntity<Guid>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the invoice.
        /// </summary>
        public Guid InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the associated sales order identifier, if applicable.
        /// </summary>
        public Guid? SalesOrderId { get; set; }

        /// <summary>
        /// Gets or sets the invoice number.
        /// </summary>
        public string Number { get; set; } = string.Empty;
    }
}