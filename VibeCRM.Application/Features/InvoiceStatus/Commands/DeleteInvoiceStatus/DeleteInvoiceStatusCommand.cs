using MediatR;

namespace VibeCRM.Application.Features.InvoiceStatus.Commands.DeleteInvoiceStatus
{
    /// <summary>
    /// Command to delete (soft delete) an invoice status
    /// </summary>
    public class DeleteInvoiceStatusCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the invoice status to delete
        /// </summary>
        public Guid Id { get; set; }
    }
}
