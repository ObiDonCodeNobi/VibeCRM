using VibeCRM.Shared.DTOs.InvoiceLineItem;
using VibeCRM.Shared.DTOs.QuoteLineItem;
using VibeCRM.Shared.DTOs.SalesOrderLineItem;
using VibeCRM.Shared.DTOs.SalesOrderLineItem_Service;

namespace VibeCRM.Application.Features.Service.DTOs
{
    /// <summary>
    /// Data transfer object for detailed service information
    /// </summary>
    public class ServiceDetailsDto : ServiceDto
    {
        /// <summary>
        /// Gets or sets the name of the service type
        /// </summary>
        public string ServiceTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the user who created this service
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this service was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who last modified this service
        /// </summary>
        public Guid? ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this service was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the collection of quote line items associated with this service
        /// </summary>
        public ICollection<QuoteLineItemDto>? QuoteLineItems { get; set; }

        /// <summary>
        /// Gets or sets the collection of invoice line items associated with this service
        /// </summary>
        public ICollection<InvoiceLineItemDto>? InvoiceLineItems { get; set; }

        /// <summary>
        /// Gets or sets the collection of sales order line item service relationships
        /// </summary>
        public ICollection<SalesOrderLineItem_ServiceDto>? SalesOrderLineItemServices { get; set; }

        /// <summary>
        /// Gets or sets the collection of sales order line items associated with this service
        /// </summary>
        public ICollection<SalesOrderLineItemDto>? SalesOrderLineItems { get; set; }
    }
}