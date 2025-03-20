using MediatR;

namespace VibeCRM.Application.Features.Invoice.Commands.DeleteInvoice
{
    /// <summary>
    /// Command for soft-deleting an existing invoice
    /// </summary>
    public class DeleteInvoiceCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the invoice to delete
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who is deleting the invoice
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date when the invoice is being deleted
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}