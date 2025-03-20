using VibeCRM.Application.Features.Company.DTOs;

namespace VibeCRM.Application.Features.SalesOrder.DTOs
{
    /// <summary>
    /// Data Transfer Object for listing sales orders with minimal information
    /// </summary>
    public class SalesOrderListDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the sales order
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the sales order number
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the sales order status name
        /// </summary>
        public string SalesOrderStatusName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the order date
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the ship date
        /// </summary>
        public DateTime? ShipDate { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the sales order
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the companies associated with this sales order
        /// </summary>
        public ICollection<CompanyListDto> Companies { get; set; } = new List<CompanyListDto>();

        /// <summary>
        /// Gets or sets the created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the created by user name
        /// </summary>
        public string CreatedByName { get; set; } = string.Empty;
    }
}