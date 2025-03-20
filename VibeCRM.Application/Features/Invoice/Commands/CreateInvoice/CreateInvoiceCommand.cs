using MediatR;

namespace VibeCRM.Application.Features.Invoice.Commands.CreateInvoice
{
    /// <summary>
    /// Command for creating a new invoice
    /// </summary>
    public class CreateInvoiceCommand : IRequest<Guid>
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
        /// Gets or sets the identifier of the user who created the invoice
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date when the invoice was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}