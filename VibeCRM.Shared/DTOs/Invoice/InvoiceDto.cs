namespace VibeCRM.Shared.DTOs.Invoice
{
    /// <summary>
    /// Data Transfer Object for Invoice information
    /// </summary>
    public class InvoiceDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the invoice
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the associated sales order identifier, if applicable
        /// </summary>
        public Guid? SalesOrderId { get; set; }

        /// <summary>
        /// Gets or sets the invoice number
        /// </summary>
        public string Number { get; set; } = string.Empty;
    }
}