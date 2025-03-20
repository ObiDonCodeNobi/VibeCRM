using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a sales order in the CRM system
    /// </summary>
    public class SalesOrder : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SalesOrder"/> class.
        /// </summary>
        public SalesOrder()
        {
            Companies = new HashSet<Company_SalesOrder>();
            Activities = new HashSet<SalesOrder_Activity>();
            LineItems = new HashSet<SalesOrderLineItem>();
            Id = Guid.NewGuid();
            Number = string.Empty;
        }

        /// <summary>
        /// Gets or sets the sales order identifier that directly maps to the SalesOrderId database column
        /// </summary>
        public Guid SalesOrderId { get => Id; set => Id = value; }

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
        public Guid? TaxCodeId { get; set; }

        /// <summary>
        /// Gets or sets the related quote identifier, if this sales order was created from a quote
        /// </summary>
        public Guid? QuoteId { get; set; }

        /// <summary>
        /// Gets or sets the number of the sales order
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the order date
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the due date
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the ship date
        /// </summary>
        public DateTime? ShipDate { get; set; }

        /// <summary>
        /// Gets or sets the subtotal amount
        /// </summary>
        public decimal Subtotal { get; set; }

        /// <summary>
        /// Gets or sets the tax amount
        /// </summary>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the total discount amount
        /// </summary>
        public decimal TotalDiscount { get; set; }

        /// <summary>
        /// Gets or sets the due amount
        /// </summary>
        public decimal DueAmount { get; set; }

        /// <summary>
        /// Gets or sets the collection of companies associated with this sales order
        /// </summary>
        public ICollection<Company_SalesOrder> Companies { get; set; }

        /// <summary>
        /// Gets or sets the collection of activities associated with this sales order
        /// </summary>
        public ICollection<SalesOrder_Activity> Activities { get; set; }

        /// <summary>
        /// Gets or sets the quote associated with this sales order
        /// May be null if the sales order was not created from a quote
        /// </summary>
        public Quote? Quote { get; set; }

        /// <summary>
        /// Gets or sets the sales order status associated with this sales order
        /// </summary>
        public SalesOrderStatus? SalesOrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the ship method associated with this sales order
        /// </summary>
        public ShipMethod? ShipMethod { get; set; }

        /// <summary>
        /// Gets or sets the bill to address associated with this sales order
        /// </summary>
        public Address? BillToAddress { get; set; }

        /// <summary>
        /// Gets or sets the ship to address associated with this sales order
        /// </summary>
        public Address? ShipToAddress { get; set; }

        /// <summary>
        /// Gets or sets the collection of line items associated with this sales order
        /// </summary>
        public ICollection<SalesOrderLineItem> LineItems { get; set; }

        /// <summary>
        /// Gets the total amount of the sales order.
        /// This is a computed property calculated from Subtotal, TaxAmount, and TotalDiscount.
        /// </summary>
        public decimal TotalAmount => Subtotal + TaxAmount - TotalDiscount;
    }
}