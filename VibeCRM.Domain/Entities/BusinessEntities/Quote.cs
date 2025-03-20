using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.JunctionEntities;
using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a quote in the CRM system
    /// </summary>
    public class Quote : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Quote"/> class.
        /// </summary>
        public Quote() { Companies = new HashSet<Company_Quote>(); Activities = new HashSet<Quote_Activity>(); LineItems = new HashSet<QuoteLineItem>(); SalesOrders = new HashSet<SalesOrder>(); Id = Guid.NewGuid(); Number = string.Empty; }

        /// <summary>
        /// Gets or sets the quote identifier that directly maps to the QuoteId database column
        /// </summary>
        public Guid QuoteId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the quote status identifier
        /// </summary>
        public Guid? QuoteStatusId { get; set; }

        /// <summary>
        /// Gets or sets the quote number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the collection of companies associated with this quote
        /// </summary>
        public ICollection<Company_Quote> Companies { get; set; }

        /// <summary>
        /// Gets or sets the collection of activities associated with this quote
        /// </summary>
        public ICollection<Quote_Activity> Activities { get; set; }

        /// <summary>
        /// Gets or sets the quote status associated with this quote
        /// </summary>
        public QuoteStatus? QuoteStatus { get; set; }

        /// <summary>
        /// Gets or sets the collection of line items associated with this quote
        /// </summary>
        public ICollection<QuoteLineItem> LineItems { get; set; }

        /// <summary>
        /// Gets or sets the collection of sales orders created from this quote
        /// </summary>
        public ICollection<SalesOrder> SalesOrders { get; set; }
    }
}