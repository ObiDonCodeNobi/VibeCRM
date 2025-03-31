using MediatR;
using VibeCRM.Shared.DTOs.InvoiceStatus;

namespace VibeCRM.Application.Features.InvoiceStatus.Queries.GetInvoiceStatusById
{
    /// <summary>
    /// Query to retrieve an invoice status by its ID
    /// </summary>
    public class GetInvoiceStatusByIdQuery : IRequest<InvoiceStatusDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the invoice status to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}