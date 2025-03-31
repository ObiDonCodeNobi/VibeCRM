using MediatR;
using VibeCRM.Shared.DTOs.SalesOrder;

namespace VibeCRM.Application.Features.SalesOrder.Commands.CreateSalesOrder
{
    /// <summary>
    /// Command for creating a new sales order
    /// </summary>
    public class CreateSalesOrderCommand : IRequest<SalesOrderDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the sales order
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the sales order number
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the sales order status identifier
        /// </summary>
        public Guid SalesOrderStatusId { get; set; }

        /// <summary>
        /// Gets or sets the ship method identifier
        /// </summary>
        public Guid ShipMethodId { get; set; }

        /// <summary>
        /// Gets or sets the bill to address identifier
        /// </summary>
        public Guid BillToAddressId { get; set; }

        /// <summary>
        /// Gets or sets the ship to address identifier
        /// </summary>
        public Guid ShipToAddressId { get; set; }

        /// <summary>
        /// Gets or sets the tax code identifier
        /// </summary>
        public Guid TaxCodeId { get; set; }

        /// <summary>
        /// Gets or sets the related quote identifier, if this sales order was created from a quote
        /// </summary>
        public Guid? QuoteId { get; set; }

        /// <summary>
        /// Gets or sets the order date
        /// </summary>
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the ship date
        /// </summary>
        public DateTime? ShipDate { get; set; }

        /// <summary>
        /// Gets or sets the subtotal amount
        /// </summary>
        public decimal SubTotal { get; set; }

        /// <summary>
        /// Gets or sets the tax amount
        /// </summary>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the due amount
        /// </summary>
        public decimal DueAmount { get; set; }

        /// <summary>
        /// Gets or sets the user identifier who is creating this sales order
        /// </summary>
        public Guid CreatedBy { get; set; }
    }
}