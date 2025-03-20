using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a service offering in the system
    /// </summary>
    public class Service : BaseAuditableEntity<Guid>
    {
        /// <summary>
        /// Gets or sets the type of service
        /// </summary>
        public Guid ServiceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the service
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the service
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the related service type entity
        /// </summary>
        public ServiceType? ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the collection of quote line items associated with this service
        /// </summary>
        public ICollection<QuoteLineItem>? QuoteLineItems { get; set; }

        /// <summary>
        /// Gets or sets the collection of invoice line items associated with this service
        /// </summary>
        public ICollection<InvoiceLineItem>? InvoiceLineItems { get; set; }

        /// <summary>
        /// Gets or sets the collection of sales order line item service relationships
        /// </summary>
        public ICollection<SalesOrderLineItem_Service>? SalesOrderLineItemServices { get; set; }

        /// <summary>
        /// Gets or sets the collection of sales order line items associated with this service
        /// </summary>
        public ICollection<SalesOrderLineItem>? SalesOrderLineItems { get; set; }
    }
}