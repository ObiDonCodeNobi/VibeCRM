using VibeCRM.Shared.DTOs.Activity;
using VibeCRM.Shared.DTOs.Address;
using VibeCRM.Shared.DTOs.Company;
using VibeCRM.Shared.DTOs.Quote;
using VibeCRM.Shared.DTOs.SalesOrderLineItem;

namespace VibeCRM.Shared.DTOs.SalesOrder
{
    /// <summary>
    /// Detailed Data Transfer Object for a sales order with related entities
    /// </summary>
    public class SalesOrderDetailsDto : SalesOrderDto
    {
        /// <summary>
        /// Gets or sets the sales order status name
        /// </summary>
        public string SalesOrderStatusName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ship method name
        /// </summary>
        public string ShipMethodName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the bill to address details
        /// </summary>
        public AddressDto BillToAddress { get; set; } = new AddressDto();

        /// <summary>
        /// Gets or sets the ship to address details
        /// </summary>
        public AddressDto ShipToAddress { get; set; } = new AddressDto();

        /// <summary>
        /// Gets or sets the tax code name
        /// </summary>
        public string TaxCodeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the related quote details
        /// </summary>
        public QuoteDto Quote { get; set; } = new QuoteDto();

        /// <summary>
        /// Gets or sets the companies associated with this sales order
        /// </summary>
        public ICollection<CompanyListDto> Companies { get; set; } = new List<CompanyListDto>();

        /// <summary>
        /// Gets or sets the activities associated with this sales order
        /// </summary>
        public ICollection<ActivityListDto> Activities { get; set; } = new List<ActivityListDto>();

        /// <summary>
        /// Gets or sets the total amount of the sales order
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the created by user name
        /// </summary>
        public string CreatedByName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the modified by user name
        /// </summary>
        public string ModifiedByName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the line items associated with this sales order
        /// </summary>
        public ICollection<SalesOrderLineItemDto> LineItems { get; set; } = new List<SalesOrderLineItemDto>();
    }
}