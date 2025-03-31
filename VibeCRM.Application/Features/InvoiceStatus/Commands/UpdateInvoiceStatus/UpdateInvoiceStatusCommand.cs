using MediatR;
using VibeCRM.Shared.DTOs.InvoiceStatus;

namespace VibeCRM.Application.Features.InvoiceStatus.Commands.UpdateInvoiceStatus
{
    /// <summary>
    /// Command to update an existing invoice status
    /// </summary>
    public class UpdateInvoiceStatusCommand : IRequest<InvoiceStatusDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the invoice status
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the invoice status (e.g., "Draft", "Sent", "Paid", "Overdue")
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a detailed description of the invoice status
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting invoice statuses in listings
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}