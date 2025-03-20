namespace VibeCRM.Application.Features.SalesOrderLineItem_Service.DTOs
{
    /// <summary>
    /// Data transfer object for SalesOrderLineItem_Service junction entity
    /// </summary>
    public class SalesOrderLineItem_ServiceDto
    {
        /// <summary>
        /// Gets or sets the sales order line item identifier
        /// </summary>
        public Guid SalesOrderLineItemId { get; set; }

        /// <summary>
        /// Gets or sets the service identifier
        /// </summary>
        public Guid ServiceId { get; set; }

        /// <summary>
        /// Gets or sets whether this entity is active (not soft-deleted)
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the date this entity was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}