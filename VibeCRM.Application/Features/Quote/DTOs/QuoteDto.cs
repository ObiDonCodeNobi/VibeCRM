namespace VibeCRM.Application.Features.Quote.DTOs
{
    /// <summary>
    /// Data Transfer Object for Quote information
    /// </summary>
    public class QuoteDto
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
        /// Gets or sets a value indicating whether the quote is active
        /// </summary>
        public bool Active { get; set; }
    }
}