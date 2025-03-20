namespace VibeCRM.Application.Features.Quote.DTOs
{
    /// <summary>
    /// Data Transfer Object for listing Quotes in UI components
    /// </summary>
    public class QuoteListDto
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
        /// Gets or sets the count of line items in this quote
        /// </summary>
        public int LineItemCount { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the quote
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the quote was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the quote was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}