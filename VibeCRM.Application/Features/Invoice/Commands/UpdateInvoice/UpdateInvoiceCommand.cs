using MediatR;

namespace VibeCRM.Application.Features.Invoice.Commands.UpdateInvoice
{
    /// <summary>
    /// Command for updating an existing invoice
    /// </summary>
    public class UpdateInvoiceCommand : IRequest<bool>
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

        /// <summary>
        /// Gets or sets the identifier of the user who modified the invoice
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date when the invoice was modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}