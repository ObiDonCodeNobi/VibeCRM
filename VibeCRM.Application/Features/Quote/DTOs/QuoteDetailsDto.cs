using VibeCRM.Application.Features.QuoteLineItem.DTOs;
using VibeCRM.Application.Features.SalesOrder.DTOs;
using System.Collections.Generic;

namespace VibeCRM.Application.Features.Quote.DTOs
{
    /// <summary>
    /// Detailed Data Transfer Object for Quote information including audit details
    /// </summary>
    public class QuoteDetailsDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quote
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the quote number
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quote status identifier
        /// </summary>
        public Guid? QuoteStatusId { get; set; }

        /// <summary>
        /// Gets or sets the name of the quote status
        /// </summary>
        public string? QuoteStatusName { get; set; }

        /// <summary>
        /// Gets or sets the collection of line items associated with this quote
        /// </summary>
        public ICollection<QuoteLineItemDto> LineItems { get; set; } = new List<QuoteLineItemDto>();

        /// <summary>
        /// Gets or sets the collection of sales orders created from this quote
        /// </summary>
        public ICollection<SalesOrderListDto> SalesOrders { get; set; } = new List<SalesOrderListDto>();

        /// <summary>
        /// Gets or sets a value indicating whether the quote is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the quote
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the quote was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the quote
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the quote was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}